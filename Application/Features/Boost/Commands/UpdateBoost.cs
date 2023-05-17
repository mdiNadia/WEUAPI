using Application.Errors;
using Application.Interfaces;
using MediatR;
using System.Net;

namespace Application.Features.Province.Commands
{
    public class UpdateBoost : IRequest<int>
    {
        public int Id { get; set; }
        public int NumberOfadViews { get; set; }
        public int ValuePerVisit { get; set; }
        public decimal Debit { get; set; }
        public int AdvertisingId { get; set; }
        public class UpdateBoostHandler : IRequestHandler<UpdateBoost, int>
        {
            private readonly IUnitOfWork _unitOfWork;

            public UpdateBoostHandler(IUnitOfWork unitOfWork)
            {
                this._unitOfWork = unitOfWork;
            }
            public async Task<int> Handle(UpdateBoost command, CancellationToken cancellationToken)
            {

                var boost = await _unitOfWork.Boosts.GetByID(command.Id);

                if (boost == null)
                {
                    throw new RestException(HttpStatusCode.BadRequest, "اطلاعات وجود ندارد!");

                }
                else
                {
                    boost.Debit = command.Debit;
                    boost.NumberOfadViews = command.NumberOfadViews;
                    boost.ValuePerVisit = command.ValuePerVisit;
                    boost.AdvertisingId = command.AdvertisingId;
                    _unitOfWork.Boosts.Update(boost);
                    try
                    {
                        await _unitOfWork.CompleteAsync();
                        return boost.Id;
                    }
                    catch (Exception err) { throw new Exception("خطا در ذخیره اطلاعات!"); }


                }
            }
        }
    }
}
