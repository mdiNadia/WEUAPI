using Application.Errors;
using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Application.Features.City.Commands
{
    public class DeleteCityById : IRequest<string>
    {
        public int Id { get; set; }
        public class DeleteCityByIdHandler : IRequestHandler<DeleteCityById, string>
        {
            private readonly IUnitOfWork _unitOfWork;

            public DeleteCityByIdHandler(IUnitOfWork unitOfWork)
            {
                this._unitOfWork = unitOfWork;
            }
            public async Task<string> Handle(DeleteCityById command, CancellationToken cancellationToken)
            {
                var city = await _unitOfWork.Cities.GetByID(command.Id);
                if (city == null) throw new RestException(HttpStatusCode.BadRequest, "طلاعات وجود ندارد!");
                var hasChildren = await _unitOfWork.Neighborhoods.GetQueryList()
                    .AsNoTracking().AnyAsync(c => c.CityId == command.Id);
                if (hasChildren) throw new RestException(HttpStatusCode.BadRequest, "این شهر دارای محله می باشد!");
                _unitOfWork.Cities.Delete(city);
                try
                {
                    await _unitOfWork.CompleteAsync();
                    return $"{city.Id}";
                }
                catch (Exception err) { throw new Exception("خطا در ذخیره اطلاعات!"); }



            }
        }
    }
}
