
using Application.Errors;
using Application.Features.Value.Commands;
using Application.Interfaces;
using Application.Services.UserAccessor;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Application.Features.View.Commands
{
    public class ViewCommand : IRequest<bool>
    {
        public int ConfirmedResultId { get; set; }
        public class ViewHandler : IRequestHandler<ViewCommand, bool>
        {
            private readonly IMediator _mediator;
            private readonly IUnitOfWork _unitOfWork;
            private readonly IUserAccessor _userAccessor;

            public ViewHandler(IMediator mediator,IUnitOfWork unitOfWork, IUserAccessor userAccessor)
            {
                this._mediator = mediator;
                _unitOfWork = unitOfWork;
                this._userAccessor = userAccessor;
            }
            public async Task<bool> Handle(ViewCommand command, CancellationToken cancellationToken)
            {

                var observer = await _unitOfWork.Profiles.GetQueryList().SingleOrDefaultAsync(x => x.Username == _userAccessor.GetCurrentUserNameAsync());
                if (observer == null)
                    throw new RestException(HttpStatusCode.NotFound, "Not found User");

                var target = await _unitOfWork.ConfirmedResults.GetByID(command.ConfirmedResultId);
                if (target == null)
                    throw new RestException(HttpStatusCode.NotFound, "Not found Advertisement");
                var isExcist = await _unitOfWork.Views.GetQueryList().SingleOrDefaultAsync(c => c.ObserverId == observer.Id && c.TargetId == target.Id);
               
                if (isExcist == null)
                {
                    var view = new Domain.Entities.View
                    {
                        Observer = observer,
                        Target = target,
                    };
                    view.CreationDate = DateTime.Now;
                    _unitOfWork.Views.Insert(view);
                    var boost = await _unitOfWork.Boosts.GetQueryList().FirstOrDefaultAsync(c=>c.AdvertisingId == target.AdId);
                    if (boost != null)
                    {
                        GetValue getValue = new GetValue();
                        getValue.TargetId = target.Id;
                        getValue.TargetName = target.Name;
                        getValue.ValuePerVisit = boost.ValuePerVisit;
                        await _mediator.Send(getValue);
                    }

                }
                
                try
                {
                    await _unitOfWork.CompleteAsync();
                    return true;
                }
                catch (Exception err) { throw new Exception("خطا در ذخیره اطلاعات!"); }
            }
        }
    }
}
