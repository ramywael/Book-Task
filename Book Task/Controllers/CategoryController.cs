using Book_Task.DTOs.Request;
using Book_Task.DTOs.Response;
using Book_Task.Models;
using Book_Task.Repositories.IRepositories;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Book_Task.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryController(ICategoryRepository categoryRepository) 
        {
            this._categoryRepository = categoryRepository;
        }
        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            var categories = _categoryRepository.Get();
            return Ok(categories.Adapt<IEnumerable<CategoryResponse>>());
        }

        [HttpGet("GetOne/{id}")]
        public IActionResult GetOne([FromRoute]int id) 
        {
            var category = _categoryRepository.GetOne(filter:e=>e.Id==id);
            if (category == null)
                return NotFound();
            return Ok(category.Adapt<CategoryResponse>());
        }


        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] CategoryRequest categoryRequest,CancellationToken cancellationToken)
        {
            if(categoryRequest != null)
            {
                var category = categoryRequest.Adapt<Category>();
                await _categoryRepository.CreateAsync(category,cancellationToken);
                await _categoryRepository.CommitAsync(cancellationToken);


                return CreatedAtAction(nameof(GetOne),new {id = category.Id},category.Adapt<CategoryResponse>());
            }
            return BadRequest();
        }

        [HttpPut("Update/{id}")]
        public IActionResult Update([FromRoute] int id, [FromBody] CategoryRequest categoryRequest,CancellationToken cancellationToken)
        {
            var categoryInDb = _categoryRepository.GetOne(filter: e=>e.Id == id,isTrack:false);
            if (categoryInDb == null)
                return NotFound();
             if(categoryRequest != null)
            {
                var category = categoryRequest.Adapt<Category>();
                category.Id = id;
                _categoryRepository.UpdateAsync(category,cancellationToken);
                return NoContent();
            }
            return NotFound();
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id,CancellationToken cancellationToken)
        {
            var category = _categoryRepository.GetOne(filter: e=>e.Id==id);
            if(category == null)
                return NotFound();
            await _categoryRepository.DeleteAsync(category, cancellationToken);
            return NoContent();
        }

    }
}
