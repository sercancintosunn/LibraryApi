using LibraryApi.Business.DTOs.Books;
using LibraryApi.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApi.Business.Interfaces.Services
{
    public interface IBookService
    {
        Task<BookResponseDto?> GetByIdAsync(int id);
        Task<IEnumerable<BookResponseDto>> GetAllAsync();
        Task<BookResponseDto> CreateBookAsync(CreateBookDto dto);
        Task UpdateBookAsync(int id,CreateBookDto dto);
        Task DeleteBookAsync(int id);
    }
}
