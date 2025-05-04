using Book_Task.DTOs.Request;
using Book_Task.DTOs.Response;
using Book_Task.Models;
using Book_Task.Repositories;
using Book_Task.Repositories.IRepositories;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace Book_Task.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookRepository _bookRepository;

        public BookController(IBookRepository bookRepository)
        {
            this._bookRepository = bookRepository;
        }

        [HttpGet("GetAll")]
        public IActionResult Get()
        {
            var books = _bookRepository.Get(includes: [e=>e.Category]);
            return Ok(books.Adapt<IEnumerable<BookResponse>>());
        }
        [HttpGet("GetOne/{id}")]
        public IActionResult GetOne([FromRoute] int id) 
        {
            var book = _bookRepository.GetOne(filter: e => e.Id == id, includes: [e=>e.Category]);
            if(book == null)
                return NotFound();
            return Ok(book.Adapt<BookResponse>());
        }
        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] BookRequest bookRequest,CancellationToken cancellationToken)
        {
            if (bookRequest == null)
                return BadRequest();
            var book = bookRequest.Adapt<Book>();
           await _bookRepository.CreateAsync(book,cancellationToken);
            await _bookRepository.CommitAsync(cancellationToken);
            return CreatedAtAction(nameof(GetOne), new {id= book.Id},book.Adapt<BookResponse>());

        }

        [HttpPut("Update/{id}")]
        public IActionResult Update([FromRoute] int id, [FromBody] BookRequest bookRequest,CancellationToken cancellationToken)
        {
            var bookInDb = _bookRepository.GetOne(filter:e=>e.Id==id,includes: [e=>e.Category],isTrack:false);
            if(bookInDb != null)
            {
                var book = bookRequest.Adapt<Book>();
                book.Id = id;
                _bookRepository.UpdateAsync(book, cancellationToken);
                return NoContent();
            }
            return NotFound();

        }
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id,CancellationToken cancellationToken)
        {
            var book = _bookRepository.GetOne(filter:e=>e.Id==id);
            if (book == null)
                return NotFound();
            await _bookRepository.DeleteAsync(book, cancellationToken);
            return NoContent();
        }
    }
}
