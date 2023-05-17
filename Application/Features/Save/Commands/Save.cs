
using Application.Errors;
using Application.Interfaces;
using Application.Services.UserAccessor;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Application.Features.Advertising.Commands
{
    public class SaveCommand : IRequest<bool>
    {
        public int ConfirmedResultId { get; set; }
        public class SaveAdvertisingHandler : IRequestHandler<SaveCommand, bool>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IUserAccessor _userAccessor;

            public SaveAdvertisingHandler(IUnitOfWork unitOfWork, IUserAccessor userAccessor)
            {
                _unitOfWork = unitOfWork;
                this._userAccessor = userAccessor;
            }
            public async Task<bool> Handle(SaveCommand command, CancellationToken cancellationToken)
            {

                var observer = await _unitOfWork.Profiles.GetQueryList().SingleOrDefaultAsync(x => x.Username == _userAccessor.GetCurrentUserNameAsync());
                if (observer == null)
                    throw new RestException(HttpStatusCode.NotFound, "Not found User");

                var target = await _unitOfWork.ConfirmedResults.GetByID(command.ConfirmedResultId);
                if (target == null)
                    throw new RestException(HttpStatusCode.NotFound, "Not found Advertisement");
                var isExcist = await _unitOfWork.SavedAds.GetQueryList().SingleOrDefaultAsync(c => c.ProfileId == observer.Id && c.AdvertisingId == target.Id);
                if (isExcist != null)
                {
                    _unitOfWork.SavedAds.Delete(isExcist);
                    try
                    {
                        await _unitOfWork.CompleteAsync();
                        return false;
                    }
                    catch (Exception err) { throw new Exception("خطا در ذخیره اطلاعات!"); }
                }
                if (isExcist == null)
                {
                    var saved = new SavedAd
                    {
                        Profile = observer,
                        Advertising = target,
                    };
                    saved.CreationDate = DateTime.Now;
                    _unitOfWork.SavedAds.Insert(saved);
                    try
                    {
                        await _unitOfWork.CompleteAsync();
                        return true;
                    }
                    catch (Exception err) { throw new Exception("خطا در ذخیره اطلاعات!"); }
                }
                return false;
            }
        }
    }
}
