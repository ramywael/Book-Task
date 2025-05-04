using Book_Task.Data;
using Book_Task.Models;
using Book_Task.Repositories.IRepositories;

namespace Book_Task.Repositories
{
    public class BookRepository : Repository<Book> , IBookRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public BookRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
            this._applicationDbContext = applicationDbContext;
        }
    }
}
