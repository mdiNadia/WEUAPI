using Application.Errors;
using Application.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Application.Features.User.Commands
{
    public class UpdateUser : IRequest<string>
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string? Password { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Role { get; set; }
        public class UpdateUserHandler : IRequestHandler<UpdateUser, string>
        {
            private readonly IPasswordHasher<ApplicationUser> _passwordHasher;
            private readonly RoleManager<IdentityRole> _roleManager;
            private readonly UserManager<ApplicationUser> _userManager;
            private readonly IUnitOfWork _unitOfWork;

            public UpdateUserHandler(IPasswordHasher<ApplicationUser> passwordHasher, RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager, IUnitOfWork unitOfWork)
            {
                this._passwordHasher = passwordHasher;
                this._roleManager = roleManager;
                this._userManager = userManager;
                this._unitOfWork = unitOfWork;
            }
            public async Task<string> Handle(UpdateUser command, CancellationToken cancellationToken)
            {

                var User = await _userManager.FindByIdAsync(command.Id);
                if (User == null)
                {
                    throw new RestException(HttpStatusCode.BadRequest, "اطلاعات وجود ندارد!");
                }
                else
                {
                    if (await _userManager.Users.Where(c => c.Email == command.Email && c.Id != command.Id).FirstOrDefaultAsync() != null)
                        throw new RestException(HttpStatusCode.BadRequest, "ایمیل تکراری است");
                    if (await _userManager.Users.Where(c => c.UserName == command.Username && c.Id != command.Id).FirstOrDefaultAsync() != null)
                        throw new RestException(HttpStatusCode.BadRequest, "نام کاربری تکراری است");
                    if (await _userManager.Users.Where(c => c.PhoneNumber == command.PhoneNumber && c.Id != command.Id).FirstOrDefaultAsync() != null)
                        throw new RestException(HttpStatusCode.BadRequest, "شماره تلفن تکراری است");
                    User.UserName = command.Username;
                    User.Email = command.Email;
                    User.PhoneNumber = command.PhoneNumber;
                    User.FirstName = command.FirstName;
                    User.LastName = command.LastName;
                    try
                    {

                        if (!await _userManager.IsInRoleAsync(User, command.Role))
                        {
                            var roles = _roleManager.Roles.Select(c => c.Name).ToList();
                            if (roles.Any())
                            {
                                foreach (var item in roles)
                                {
                                    if (await _userManager.IsInRoleAsync(User, item))
                                    {
                                        await _userManager.RemoveFromRoleAsync(User, item);
                                    }
                                }

                            }
                            await _userManager.AddToRoleAsync(User, command.Role);
                        }
                        if (!string.IsNullOrEmpty(command.Password))
                        {
                            var passwordHash = _passwordHasher.HashPassword(User, command.Password);
                            User.PasswordHash = passwordHash;
                        }
                        else
                            User.PasswordHash = User.PasswordHash;
                        var result = await _userManager.UpdateAsync(User);
                        if (result.Succeeded)
                        {
                            return User.Id;
                        }
                        return "خطا";
                    }
                    catch (Exception err) { throw new Exception("خطا در ذخیره اطلاعات!"); }


                }

            }
        }
    }
}
