using BCrypt.Net;
using LibraryApi.Business.DTOs.Members;
using LibraryApi.Business.Interfaces;
using LibraryApi.Business.Interfaces.Repositories;
using LibraryApi.Business.Interfaces.Services;
using LibraryApi.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApi.Business.Services
{
    public class MemberService : IMemberService
    {
        private readonly IMemberRepository _memberRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITokenService _tokenService;

        public MemberService(IMemberRepository memberRepository, IUnitOfWork unitOfWork, ITokenService tokenService)
        {
            _memberRepository = memberRepository;
            _unitOfWork = unitOfWork;
            _tokenService = tokenService;

        }

        public async Task<AuthResponseDto> LoginAsync(LoginDto dto)
        {
            var members = await _memberRepository.GetAllAsync();
            var member = members.FirstOrDefault(m => m.Email == dto.Email);

            if(member != null || !BCrypt.Net.BCrypt.Verify(dto.Password, member.PasswordHash))
            {
                throw new UnauthorizedAccessException("Email veya Şifre Hatalı");
            }

            var token = _tokenService.GenerateToken(member);

            return new AuthResponseDto
            {
                Token = token,
                Member = MapToResponseDto(member)
            };
        }

        public async Task<MemberResponseDto> RegisterAsync(RegisterMemberDto dto)
        {
            var existingMembers = await _memberRepository.GetAllAsync();
            if(existingMembers.Any(m => m.Email == dto.Email))
            {
                throw new InvalidOperationException("Bu email ile zaten biri kayıtlı");
            }

            var member = new Member
            {
                FullName = dto.FullName,
                Email = dto.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                Role = "Member"
            };

            await _memberRepository.AddAsync(member);
            await _unitOfWork.SaveChangesAsync();

            return MapToResponseDto(member);
        }

        private static MemberResponseDto MapToResponseDto(Member member)
        {
            return new MemberResponseDto
            {
                Id = member.Id,
                FullName = member.FullName,
                Email = member.Email,
                Role = member.Role

            };
        }
    }
}
