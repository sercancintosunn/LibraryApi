using LibraryApi.Business.DTOs.Members;
using LibraryApi.Business.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApi.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MembersController : ControllerBase
    {
        private readonly IMemberService _memberService;

        public MembersController(IMemberService memberService)
        {
            _memberService = memberService;
            
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterMemberDto dto)
        {
            
                var result = await _memberService.RegisterAsync(dto);
                return Ok(result);

            
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            
                var result = await _memberService.LoginAsync(dto);
                return Ok(result);

            
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateMemberDto dto)
        {
            var memberIdClaim = User.FindFirst("MemberId")?.Value;
            var requestingMemberId = int.Parse(memberIdClaim!);
            var isAdmin = User.IsInRole("Admin");

            await _memberService.UpdateAsync(id, dto, requestingMemberId, isAdmin);
            return NoContent(); 
        }

        [HttpGet("{me}")]
        [Authorize]
        public async Task<IActionResult> GetMe()
        {
            var memberIdClaim = User.FindFirst("MemberId")?.Value;
            var memberId = int.Parse(memberIdClaim!);

            var member = await _memberService.GetByIdAsync(memberId);
            return Ok(member);
        }
    }
}
