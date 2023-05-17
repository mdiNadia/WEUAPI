
using Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{

    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        protected readonly IApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private DbSet<TEntity> _dbSet;

        public GenericRepository(IApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            this._context = context;
            this._httpContextAccessor = httpContextAccessor;
            this._dbSet = context.set<TEntity>();
        }

        public IQueryable<TEntity> GetQueryList()
        {
            return _dbSet;
        }
        public async Task<TEntity> GetByID(object id)
        {
            return await _dbSet.FindAsync(id);
        }

        public void Insert(TEntity entity)
        {
            _dbSet.Add(entity);
        }
        public void InsertRange(ICollection<TEntity> entity)
        {
            _dbSet.AddRange(entity);
        }
        public bool Delete(object id)
        {
            try
            {
                TEntity entityToDelete = _dbSet.Find(id);
                Delete(entityToDelete);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Delete(TEntity entityToDelete)
        {
            try
            {
                if (entityToDelete == null)
                {
                    throw new ArgumentNullException("entity");
                }
                _dbSet.Remove(entityToDelete);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool Delete(ICollection<TEntity> entityToDelete)
        {
            try
            {
                _dbSet.RemoveRange(entityToDelete);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public void Update(TEntity entity)
        {

            _dbSet.Update(entity);
        }

    }


}