

namespace Application.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        IQueryable<TEntity> GetQueryList();
        Task<TEntity> GetByID(object id);
        void Insert(TEntity entity);
        void InsertRange(ICollection<TEntity> entity);
        bool Delete(object id);
        bool Delete(TEntity entityToDelete);
        bool Delete(ICollection<TEntity> entityToDelete);
        void Update(TEntity entityToUpdate);



    }
}
