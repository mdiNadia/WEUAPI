using Application.Errors;
using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Application.Features.Province.Commands
{
    public class DeleteBoostById : IRequest<string>
    {
        public int Id { get; set; }
        public class DeleteBoostByIdHandler : IRequestHandler<DeleteBoostById, string>
        {
            private readonly IUnitOfWork _unitOfWork;

            public DeleteBoostByIdHandler(IUnitOfWork unitOfWork)
            {
                this._unitOfWork = unitOfWork;
            }
            public async Task<string> Handle(DeleteBoostById command, CancellationToken cancellationToken)
            {

                var boost = await _unitOfWork.Boosts.GetByID(command.Id);
                if (boost == null) throw new RestException(HttpStatusCode.BadRequest, "طلاعات وجود ندارد!");
                var hasChildren = await _unitOfWork.Advertisings.GetQueryList()
                    .AsNoTracking().AnyAsync(c => c.BoostId == command.Id);
                if (hasChildren) throw new RestException(HttpStatusCode.BadRequest, "این شتابدهی دارای آگهی می باشد!");
                _unitOfWork.Boosts.Delete(boost);
                try
                {
                    await _unitOfWork.CompleteAsync();
                    return $"{boost.Id}";
                }
                catch (Exception err) { throw new Exception("خطا در ذخیره اطلاعات!"); }




            }
        }
    }
}
