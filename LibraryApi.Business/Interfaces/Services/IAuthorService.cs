using LibraryApi.Business.DTOs.Authors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApi.Business.Interfaces.Services
{
    public interface IAuthorService
    {
        Task<AuthorResponseDto?> GetByIdAsync(int id);
        Task<IEnumerable<AuthorResponseDto>> GetAll();
        Task<AuthorResponseDto> CreateAsync(CreateAuthorDto dto);
        Task UpdateAsync(int id, CreateAuthorDto dto);
        Task DeleteAsync(int id);

    }
}
