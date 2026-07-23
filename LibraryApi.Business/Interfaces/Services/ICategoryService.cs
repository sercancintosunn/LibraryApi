using LibraryApi.Business.DTOs.Categories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApi.Business.Interfaces.Services
{
    public interface ICategoryService
    {
        Task<CategoryResponseDto?> GetByIdAsync(int id);
        Task<IEnumerable<CategoryResponseDto>> GetAllAsync();
        Task<CategoryResponseDto> CreateAsync(CreateCategoryDto dto);
        Task UpdateAsync(int id, CreateCategoryDto dto);
        Task DeleteAsync(int id);

    }
}
