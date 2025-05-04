using System.Linq.Expressions;

namespace Book_Task.Repositories.IRepositories
{
    public interface IRepository <T> where T : class
    {
        public IQueryable<T> Get(Expression<Func<T,bool>>? filter = null ,Expression<Func<T,object>>[]? includes=null,bool isTrack=true);
        public T? GetOne(Expression<Func<T, bool>>? filter=null, Expression<Func<T, object>>[]? includes=null, bool isTrack = true);
        public Task<T> CreateAsync (T entity,CancellationToken cancellationToken);
        public Task<T> UpdateAsync (T entity,CancellationToken cancellationToken);
        public Task<bool> CommitAsync (CancellationToken cancellationToken);
        public Task<T> DeleteAsync (T entity,CancellationToken cancellationToken);
    }
}
