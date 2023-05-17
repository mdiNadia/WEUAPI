using Application.Errors;
using Application.Interfaces;
using MediatR;
using System.Net;

namespace Application.Features.AppSetting.Commands
{
    public class UpdateAppSetting : IRequest<int>
    {
        public int Id { get; set; }
        public decimal MinBoostAmount { get; set; }
        public int MinWeuPerVisit { get; set; }
        public int MinView { get; set; }
        public decimal Value { get;set; }//چند دهم یک سکه
        public int AppFee { get; set; }
        public class UpdateBoostSettingHandler : IRequestHandler<UpdateAppSetting, int>
        {
            private readonly IUnitOfWork _unitOfWork;

            public UpdateBoostSettingHandler(IUnitOfWork unitOfWork)
            {
                this._unitOfWork = unitOfWork;
            }
            public async Task<int> Handle(UpdateAppSetting command, CancellationToken cancellationToken)
            {

                var boostSetting = await _unitOfWork.AppSettings.GetByID(command.Id);
                if (boostSetting == null)
                {
                    throw new RestException(HttpStatusCode.BadRequest, "اطلاعات وجود ندارد!");
                }
                else
                {
                    boostSetting.MinValuePerVisit = command.MinWeuPerVisit;
                    boostSetting.MinBoostAmount = command.MinBoostAmount;
                    boostSetting.MinView = command.MinView;
                    boostSetting.Value = command.Value;
                    boostSetting.AppFee = command.AppFee;
                    _unitOfWork.AppSettings.Update(boostSetting);
                    try
                    {
                        await _unitOfWork.CompleteAsync();
                        return boostSetting.Id;
                    }
                    catch (Exception err) { throw new Exception("خطا در ذخیره اطلاعات!"); }


                }
            }
        }
    }
}
