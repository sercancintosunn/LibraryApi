using LibraryApi.Business.DTOs.Authors;
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
        private readonly IAuthorService _authorService;

        public AuthorController(IAuthorService authorService)
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
        public async Task<IActionResult> Create([FromBody] CreateAuthorDto dto)
        {
            var created = await _authorService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut]
        public async Task<IActionResult> Update(int id, [FromBody] CreateAuthorDto dto)
        {
            try
            {
                await _authorService.UpdateAsync(id, dto);
                return NoContent();
            }
            catch(KeyNotFoundException ex)
            {
                return NotFound(new { message = ex });
            }

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _authorService.DeleteAsync(id);
                return NoContent();
            }
            catch(KeyNotFoundException ex)
            {
                return NotFound(new { message = ex });
            }
        }

    }
}
