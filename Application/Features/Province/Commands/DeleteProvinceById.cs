using Application.Errors;
using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Application.Features.Province.Commands
{
    public class DeleteProvinceById : IRequest<string>
    {
        public int Id { get; set; }
        public class DeleteProvinceByIdHandler : IRequestHandler<DeleteProvinceById, string>
        {
            private readonly IUnitOfWork _unitOfWork;

            public DeleteProvinceByIdHandler(IUnitOfWork unitOfWork)
            {
                this._unitOfWork = unitOfWork;
            }
            public async Task<string> Handle(DeleteProvinceById command, CancellationToken cancellationToken)
            {

                var province = await _unitOfWork.Provinces.GetByID(command.Id);
                if (province == null) throw new RestException(HttpStatusCode.BadRequest, "طلاعات وجود ندارد!");
                var hasChildren = await _unitOfWork.Cities.GetQueryList()
                    .AsNoTracking().AnyAsync(c => c.ProvinceId == command.Id);
                if (hasChildren) throw new RestException(HttpStatusCode.BadRequest, "این استان دارای شهر می باشد!");
                _unitOfWork.Countries.Delete(province);
                try
                {
                    await _unitOfWork.CompleteAsync();
                    return $"{province.Id}";
                }
                catch (Exception err)
                {

                    throw new Exception("خطا در ذخیره اطلاعات!");

                }
            }
        }
    }
}
