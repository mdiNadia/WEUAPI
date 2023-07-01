
using Application.Dtos.Account;
using Application.Dtos.Register;
using Application.Errors;
using Application.ExtensionMethods;
using Application.Features.Profile.Commands;
using Application.Features.Wallet.Commands;
using Application.Helpers;
using Application.Interfaces;
using Application.Services.Email;
using Application.Services.JWT;
using Application.Services.Sms;
using Application.Services.UserAccessor;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;

public class UserAccessor : IUserAccessor
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IApplicationDbContext _context;
    private readonly IEmailSender _emailSender;
    private readonly IJWTGenerator _jWTGenerator;
    private readonly IMediator _mediator;
    private readonly IConfiguration _configuration;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    //private readonly IUrlHelper _urlHelper;
    private readonly IPasswordHasher<ApplicationUser> _passwordHasher;
    private readonly ICurrencyRepository _currencyRepository;
    private readonly ISmsSender _smsSender;

    public UserAccessor(
        IHttpContextAccessor httpContextAccessor,
        IApplicationDbContext context,
        IEmailSender emailSender,
        IJWTGenerator jWTGenerator,
        IMediator mediator,
        IConfiguration configuration,
        UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager,
        IPasswordHasher<ApplicationUser> passwordHasher,
        ICurrencyRepository currencyRepository,
        ISmsSender smsSender
         )
    {
        this._httpContextAccessor = httpContextAccessor;
        this._context = context;
        this._emailSender = emailSender;
        this._jWTGenerator = jWTGenerator;
        this._mediator = mediator;
        this._configuration = configuration;
        _userManager = userManager;
        _roleManager = roleManager;
        //this._urlHelper = _httpContextAccessor.HttpContext.RequestServices.GetRequiredService<IUrlHelper>();

        _passwordHasher = passwordHasher;
        this._currencyRepository = currencyRepository;
        this._smsSender = smsSender;
    }

    public async Task<RegisterResult> RegisterAsync(RegisterModel model)
    {


        if (string.IsNullOrEmpty(model.PhoneNumber))
        {
            var registerResult = new RegisterResult();
            registerResult.error = $"PhoneNumber couldn't be empty!";
            registerResult.statusCode = HttpStatusCode.BadRequest;
            return registerResult;
        }

        ApplicationUser user = new ApplicationUser();
        user.UserName = model.Username;
        user.Email = model.Email;
        user.PhoneNumber = model.PhoneNumber;
        user.FirstName = model.FirstName ?? "FirstName";
        user.LastName = model.LastName ?? "LastName";
        user.CreationDate = DateTime.Now;
        user.NumCode = model.NumCode;

        //var userWithSameEmail = await _userManager.FindByEmailAsync(model.Email);
        var userWithSameUserName = await _userManager.FindByNameAsync(model.Username);
        using (var dbContextTransaction = _context.DatabaseBeginTransaction())
        {
            try
            {
                if (userWithSameUserName == null)
                {
                    var result = await _userManager.CreateAsync(user, model.Password);

                    if (result.Succeeded)
                    {
                        var userID = _userManager.GetUserIdAsync(user).Result;
                        //اینجا باید پروفایل هم ساخته بشه براش
                        CreateProfile command = new CreateProfile();
                        command.Username = model.Username;
                        command.UserId = userID;
                        int ProfileId = await _mediator.Send(command);
                        //ساخت کیف پول
                        CreateWallet wallet = new CreateWallet();
                        wallet.IsActive = true;
                        wallet.Description = "";
                        wallet.CurrencyId = await _currencyRepository.GetQueryList().AsNoTracking()
                            .Where(c => c.IsDefault && c.IsActive).Select(c => c.Id).FirstOrDefaultAsync();
                        wallet.Name = "DefaultWallet";
                        wallet.TotalCredit = 0;
                        wallet.ProfileId = ProfileId;
                        await _mediator.Send(wallet);
                        //Add Role
                        if (string.IsNullOrEmpty(model.Role))
                            await _userManager.AddToRoleAsync(user, Domain.Constants.Authorization.default_role.ToString());
                        else
                            await _userManager.AddToRoleAsync(user, model.Role);
                        bool verifyCode = await CheckVerifyCode(model.Code);
                        if (verifyCode)
                        {
                            var token = await GetTokenAsync(new TokenRequestModel() { Username = model.Username, Password = model.Password });
                            dbContextTransaction.Commit();
                            return new RegisterResult()
                            {
                                error = null,
                                statusCode = HttpStatusCode.OK,
                                Token = token.Token,
                            };
                        }
                        else
                        {
                            dbContextTransaction.Rollback();
                            return new RegisterResult()
                            {
                                error = $"verifying was failed!",
                                statusCode = HttpStatusCode.BadRequest
                            };
                        }
                        //End sending code scenario

                    }

                    dbContextTransaction.Rollback();
                    return new RegisterResult()
                    {
                        error = $"{result} for username {user.UserName}",
                        statusCode = HttpStatusCode.BadRequest
                    };

                }
                else
                {
                    dbContextTransaction.Rollback();
                    return new RegisterResult()
                    {
                        error = $"Email {user.UserName} is already registered.",
                        statusCode = HttpStatusCode.BadRequest


                    };

                }
            }
            catch (Exception err)
            {
                dbContextTransaction.Rollback();
                throw new RestException(HttpStatusCode.InternalServerError, "Error occured in saving data in database!");
            }
        }

    }
    public async Task<bool> Verify(VerifyModel verifyModel)
    {
        if (string.IsNullOrEmpty(verifyModel.Number))
            throw new RestException(HttpStatusCode.BadRequest, "Insert PhoneNumber!");
        if (string.IsNullOrEmpty(verifyModel.Username))
            throw new RestException(HttpStatusCode.BadRequest, "Insert UserName!");
        if (await _userManager.FindByNameAsync(verifyModel.Username) != null)
            throw new RestException(HttpStatusCode.BadRequest, "The UserName already exists!");
        if (await _context.ApplicationUsers.AnyAsync(c => c.PhoneNumber.Contains(verifyModel.Number)))
            throw new RestException(HttpStatusCode.BadRequest, "The PhoneNumber already exists!");

        var smsData = new SendSmsData();
        string verifyCode = GenerateVertificationCode.CreateVertificationCode();
        var sendSmsResult = await _smsSender.SendVertificateCode(verifyModel.Number, smsData.SenderNumber, smsData.Text + verifyCode);
        if (sendSmsResult)
        {
            var model = new SendSmsCode();
            model.Code = verifyCode;
            model.PhoneNumber = verifyModel.Number;
            model.CreationDate = DateTime.Now;
            model.ExpireDate = model.CreationDate.AddMinutes(2);

            _context.SendSmsCodes.Add(model);
            await _context.SaveChangesAsync();
            return true;
        }
        else
            return false;
    }
    public async Task<bool> CheckUsername(string username)
    {
        var result = await _userManager.FindByNameAsync(username);
        if (result == null)
            return true;
        return false;
    }
    public async Task<bool> CheckVerifyCode(string code)
    {
        var isexitCode = await _context.SendSmsCodes.AnyAsync(c => c.Code.Contains(code) && !(c.ExpireDate < DateTime.Now));
        if (isexitCode)
            return true;
        else
            return false;
    }
    public async Task<AuthenticationModel> GetTokenAsync(TokenRequestModel model)
    {

        var authenticationModel = new AuthenticationModel();
        //var user = await _userManager.FindByEmailAsync(model.Email);
        var user = await _userManager.FindByNameAsync(model.Username);
        if (user == null)
        {
            authenticationModel.IsAuthenticated = false;
            authenticationModel.Message = $"No Accounts Registered with {model.Username}.";
            return authenticationModel;
        }
        if (await _userManager.CheckPasswordAsync(user, model.Password))
        {

            authenticationModel.IsAuthenticated = true;
            JwtSecurityToken jwtSecurityToken = await _jWTGenerator.CreateJwtToken(user);
            authenticationModel.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            //authenticationModel.Email = user.Email;
            authenticationModel.UserName = user.UserName;
            var rolesList = await _userManager.GetRolesAsync(user).ConfigureAwait(false);
            authenticationModel.Roles = rolesList.ToList();

            if (user.RefreshTokens.Any(a => a.IsActive))
            {
                var activeRefreshToken = user.RefreshTokens.Where(a => a.IsActive == true).FirstOrDefault();
                authenticationModel.RefreshToken = activeRefreshToken.Token;
                authenticationModel.RefreshTokenExpiration = activeRefreshToken.Expires;
            }
            else
            {
                var refreshToken = _jWTGenerator.CreateRefreshToken();
                authenticationModel.RefreshToken = refreshToken.Token;
                authenticationModel.RefreshTokenExpiration = refreshToken.Expires;
                user.RefreshTokens.Add(refreshToken);
                await _userManager.UpdateAsync(user);
                await _context.SaveChangesAsync();
            }

            if (_httpContextAccessor.HttpContext.Request.GetCookie<string>("user") == null)
                _httpContextAccessor.HttpContext.Response.SetCookie(authenticationModel.UserName, "user", null);
            else
            {
                _httpContextAccessor.HttpContext.Response.Cookies.Delete("user");
                _httpContextAccessor.HttpContext.Response.SetCookie(authenticationModel.UserName, "user", null);

            }
            var UserLoginHistory = new UserLoginHistory()
            {
                UserName = authenticationModel.UserName,
                LoginDate = DateTime.Now,
            };
            _context.UsersLoginHistory.Add(UserLoginHistory);
            await _context.SaveChangesAsync();
            return authenticationModel;
        }
        authenticationModel.IsAuthenticated = false;
        authenticationModel.Message = $"Incorrect Credentials for user {user.UserName}.";
        return authenticationModel;
    }


    public async Task<string> AddRoleAsync(AddRoleModel model)
    {
        var user = await _userManager.FindByEmailAsync(model.Email);
        if (user == null)
        {
            return $"No Accounts Registered with {model.Email}.";
        }
        if (await _userManager.CheckPasswordAsync(user, model.Password))
        {
            var roleExists = Enum.GetNames(typeof(Role)).Any(x => x.ToLower() == model.Role.ToLower());
            if (roleExists)
            {
                var validRole = Enum.GetValues(typeof(Role)).Cast<Role>().Where(x => x.ToString().ToLower() == model.Role.ToLower()).FirstOrDefault();
                await _userManager.AddToRoleAsync(user, validRole.ToString());
                return $"Added {model.Role} to user {model.Email}.";
            }
            return $"Role {model.Role} not found.";
        }
        return $"Incorrect Credentials for user {user.Email}.";
    }


    public ApplicationUser GetById(string id)
    {
        return _userManager.Users.FirstOrDefault(x => x.Id == id);
    }

    public async Task<string> ForgetPassword(ForgotPasswordModel forgotPasswordModel)
    {

        if (forgotPasswordModel.EmailOrPhoneNumber.IsEmail())
        {
            var userWithSameEmail = await _userManager.FindByEmailAsync(forgotPasswordModel.EmailOrPhoneNumber);
            if (userWithSameEmail != null)
            {
                var token = await _userManager.GeneratePasswordResetTokenAsync(userWithSameEmail);
                // var callback = _urlHelper.Action(nameof(VerifyResetPasswordToken), "User", new { token, email = userWithSameEmail.Email, username = userWithSameEmail.UserName }, _httpContextAccessor.HttpContext.Request.Scheme);
                var message = userWithSameEmail.Email + "Reset password token";//+ callback;


                await _emailSender.SendEmailAsync(userWithSameEmail.Email, "Reset password Link", message);
                return $"check your email! we send you a recovery Link.";
            }
            else
                return $"email address doesn't excist!";

        }
        else if (forgotPasswordModel.EmailOrPhoneNumber.IsMobile())
        {
            var userWithSamePhone = await _userManager.FindByEmailAsync(forgotPasswordModel.EmailOrPhoneNumber);
            if (userWithSamePhone != null)
            {
                return $"check your phone! we send you a recovery Link.";
            }
            else
                return $"phone number doesn't excist!";
        }
        else
        {
            return $"Insert a Valid Email or PhoneNumber!";
        }
    }
    public async Task<object> VerifyResetPasswordToken(RequestUserResetModel requestUserResetModel)
    {
        var token = requestUserResetModel.token;
        var user = await _userManager.FindByNameAsync(requestUserResetModel.username);
        bool result = await _userManager.VerifyUserTokenAsync(user, this._userManager.Options.Tokens.PasswordResetTokenProvider, "ResetPassword", token);
        if (result)
            return new { token = requestUserResetModel.token, userId = requestUserResetModel.username };
        return false;
    }
    public async Task<string> ResetPassword(ResetPasswordModel resetPasswordModel)
    {
        try
        {
            var user = await _userManager.FindByNameAsync(resetPasswordModel.Username);
            var result = await _userManager.ResetPasswordAsync(user, resetPasswordModel.token, resetPasswordModel.Password);
            if (result.Succeeded)
                return $"password Reset!";
            return $"Error!";
        }
        catch (Exception)
        {

            throw;
        }

    }
    public async Task<string> GetCurrentUserIdAsync()
    {
        string Username = _httpContextAccessor.HttpContext.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
        var user = await _userManager.FindByNameAsync(Username);

        return user.Id;
    }
    public string GetCurrentUserNameAsync()
    {
        try
        {
            string Username = _httpContextAccessor.HttpContext.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            return Username;
        }
        catch (Exception err)
        {
            throw err;
        }

    }
    public async Task<ApplicationUser> GetUserByUsernameAsync(string username)
    {
        try
        {
            var user = await _context.ApplicationUsers.SingleOrDefaultAsync(c => c.UserName == username);
            return user;

        }
        catch (Exception err)
        {
            throw err;
        }
    }
    //TODO : Update User Details
    //TODO : Remove User from Role 
}
