using LibraryApi.Business.DTOs.Loans;
using LibraryApi.Business.Interfaces.Repositories;
using LibraryApi.Business.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApi.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class LoansController : ControllerBase
    {
        private readonly ILoanService _loanService;

        public LoansController(ILoanService loanService)
        {
            _loanService = loanService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var loans = await _loanService.GetAllAsync();
            return Ok(loans);

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var loan = await _loanService.GetByIdAsync(id);

            if(loan == null)
            {
                return NotFound(new { message = "Ödünç kaydı bulunamadı" });

            }

            return Ok(loan);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody]CreateLoanDto dto)
        {
            try
            {
                var created = await _loanService.CreateLoanAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
            }
            catch(InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });

            }
        }

        [HttpPut("{id}/return")]
        public async Task<IActionResult> Return(int id)
        {
            var memberIdClaim = User.FindFirst("MemberId")?.Value;
            var requestingMemberId = int.Parse(memberIdClaim!);

            var isAdmin = User.IsInRole("Admin");

            try
            {
                await _loanService.ReturnLoanAsync(id, requestingMemberId, isAdmin);
                return NoContent();
            }
            catch(KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch(UnauthorizedAccessException ex)
            {
                return StatusCode(403, new { message = ex.Message });

            }
            catch(InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });

            }
        }

    }
}
