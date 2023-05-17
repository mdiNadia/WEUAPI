using Application.Interfaces;
using Application.Services.FileStorage;
using MediatR;

namespace Application.Features.Province.Commands
{
    public class CreateBoost : IRequest<int>
    {
        public int NumberOfadViews { get; set; }
        public int ValuePerVisit { get; set; }
        public decimal Debit { get; set; }
        public int AdvertisingId { get; set; }
        public class CreateBoostHandler : IRequestHandler<CreateBoost, int>
        {
            private readonly IMediator _mediator;
            private readonly IUnitOfWork _unitOfWork;
            private readonly IFileUploader _fileUploader;

            public CreateBoostHandler(IMediator mediator, IUnitOfWork unitOfWork, IFileUploader fileUploader)
            {
                this._mediator = mediator;
                this._unitOfWork = unitOfWork;
                this._fileUploader = fileUploader;
            }

            public async Task<int> Handle(CreateBoost command, CancellationToken cancellationToken)
            {

                var boost = new Domain.Entities.Boost();
                boost.NumberOfadViews = command.NumberOfadViews;
                boost.ValuePerVisit = command.ValuePerVisit;
                boost.Debit = command.Debit;
                boost.AdvertisingId = command.AdvertisingId;
                boost.CreationDate = DateTime.Now;
                _unitOfWork.Boosts.Insert(boost);
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
