using LibraryApi.Business.DTOs.Books;
using LibraryApi.Business.Interfaces.Services;
using LibraryApi.Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApi.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BookController(IBookService bookService)
        {
            _bookService = bookService;
            
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var books = await _bookService.GetAllAsync();
            return Ok(books);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var book = await _bookService.GetByIdAsync(id);

            if(book == null)
            {
                return NotFound(new { message = "Kitap bulunamadı." });
                
            }

            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateBookDto dto)
        {
            var created = await _bookService.CreateBookAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody]CreateBookDto dto)
        {
            

            try
            {
                await _bookService.UpdateBookAsync(id,dto);
                return NoContent()  ;
            }
            catch(KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpDelete("id")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _bookService.DeleteBookAsync(id);
                return NoContent();
            }
            catch(KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }



    }
}
