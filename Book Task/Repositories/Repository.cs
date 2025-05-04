using Book_Task.Data;
using Book_Task.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Book_Task.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly DbSet<T> _dbSet;
        public Repository(ApplicationDbContext applicationDbContext) 
        {
            this._applicationDbContext = applicationDbContext;
            _dbSet = _applicationDbContext.Set<T>();
        }
        public async Task<bool> CommitAsync(CancellationToken cancellationToken)
        {
            try
            {
                await _applicationDbContext.SaveChangesAsync(cancellationToken);
                return true;
            }
            catch (Exception ex) {
                Console.WriteLine($"Error: {ex.Message}");
                return false;
            }
        }

        public async Task<T> CreateAsync(T entity, CancellationToken cancellationToken)
        {
            await _applicationDbContext.AddAsync(entity, cancellationToken);
            return entity;
        }

        public async Task<T> DeleteAsync(T entity, CancellationToken cancellationToken)
        {
            _applicationDbContext.Remove(entity);
            await CommitAsync(cancellationToken);
            return entity;
        }

        public IQueryable<T> Get(Expression<Func<T, bool>>? filter = null, Expression<Func<T, object>>[]? includes = null, bool isTrack = true)
        {
            IQueryable<T> query = _dbSet;
            if(filter != null)
            {
                query = query.Where(filter);
            }

            if(includes != null)
            {
                foreach(var item in includes)
                {
                    query.Include(item);
                }
            }

            if (!isTrack)
            {
                query = query.AsNoTracking();
            }
            return query;
        }

        public T? GetOne(Expression<Func<T, bool>>? filter = null, Expression<Func<T, object>>[]? includes = null, bool isTrack = true)
        {
            return Get(filter, includes, isTrack).FirstOrDefault();
        }

        public async Task<T> UpdateAsync(T entity, CancellationToken cancellationToken)
        {
            _applicationDbContext.Update(entity);
             await CommitAsync(cancellationToken);
             return entity;
        }
    }
}
