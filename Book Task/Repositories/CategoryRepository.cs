using Book_Task.Data;
using Book_Task.Models;
using Book_Task.Repositories.IRepositories;

namespace Book_Task.Repositories
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public CategoryRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
            this._applicationDbContext = applicationDbContext;
        }
    }
}
