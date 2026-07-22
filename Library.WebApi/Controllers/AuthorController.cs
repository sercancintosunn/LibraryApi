using LibraryApi.Business.Interfaces.Services;
using LibraryApi.Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApi.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly IGenericService<Author> _authorService;

        public AuthorController(IGenericService<Author> authorService)
        {
            _authorService = authorService;
            
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var authors = await _authorService.GetAllAsync();
            return Ok(authors);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var author = await _authorService.GetByIdAsync(id);
            if(author == null)
            {
                return NotFound(new { message = "Yazar bulunamadı" });
            }
            return Ok(author);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Author author)
        {
            var created = await _authorService.CreateAsync(author);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

    }
}
