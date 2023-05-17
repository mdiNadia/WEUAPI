using Domain.Entities;

namespace Application.Interfaces
{
    public interface IAdCategoryRepository : IGenericRepository<AdCategory>
    {
        Task<bool> CheckIfHasChildren(int AdCategoryId);
    }
}
