using LibraryApi.Business.DTOs.Members;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApi.Business.Interfaces.Services
{
    public interface IMemberService
    {
        Task<MemberResponseDto> RegisterAsync(RegisterMemberDto dto);
        Task<AuthResponseDto> LoginAsync(LoginDto dto);

        Task UpdateAsync(int memberId, UpdateMemberDto dto, int requestingMemberId,bool isAdmin);

        Task<MemberResponseDto> GetByIdAsync(int id);
    }
}
