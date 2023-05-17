using WEUPanel.Pages.Neighborhood;
using WEUPanel.Wrappers;

namespace WEUPanel.Services.Interfaces
{
    public interface INeighborhoodService
    {
        Task<List<GetNameAndId>> GetAll();
        Task<List<GetNameAndId>> GetAllWithoutPaging();
        Task<List<GetNameAndId>> GetAllByCityIds(List<int> ids);
        Task<PagedResponse<IEnumerable<NeighborhoodModels.Neighborhood>>> GetAllByPaging(int pageIndex, int pageSize);
        Task<Response<NeighborhoodModels.Neighborhood>> GetById(int id);

        Task<HttpResponseMessage> AddEntity(NeighborhoodModels.CreateNeighborhood command);
        Task<HttpResponseMessage> UpdateEntity(int id, NeighborhoodModels.EditNeighborhood command);
        Task<HttpResponseMessage> RemoveEntity(int id);

        Task<HttpResponseMessage> AddEntityFormFile(MultipartFormDataContent command);
        Task<HttpResponseMessage> UpdateEntityFormFile(int id, MultipartFormDataContent command);

    }
}
