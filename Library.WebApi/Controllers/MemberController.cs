using LibraryApi.Business.DTOs.Members;
using LibraryApi.Business.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApi.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MemberController : ControllerBase
    {
        private readonly IMemberService _memberService;

        public MemberController(IMemberService memberService)
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
    }
}
