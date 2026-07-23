using LibraryApi.Business.DTOs.Categories;
using LibraryApi.Business.Interfaces;
using LibraryApi.Business.Interfaces.Repositories;
using LibraryApi.Business.Interfaces.Services;
using LibraryApi.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApi.Business.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CategoryService(ICategoryRepository categoryRepository,IUnitOfWork unitOfWork)
        {
            _categoryRepository = categoryRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<CategoryResponseDto> CreateAsync(CreateCategoryDto dto)
        {
            var category = new Category
            {
                Name = dto.Name
            };

            await _categoryRepository.AddAsync(category);
            await _unitOfWork.SaveChangesAsync();

            return MapToResponseDto(category);
        }

        public async Task DeleteAsync(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if(category == null)
            {
                throw new KeyNotFoundException("Category bulunamadı");
            }
            _categoryRepository.Delete(category);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<IEnumerable<CategoryResponseDto>> GetAll()
        {
            var categories = await _categoryRepository.GetAllAsync();
            return categories.Select(MapToResponseDto);
        }

        public async Task<CategoryResponseDto?> GetByIdAsync(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category == null) return null;
            return MapToResponseDto(category);
        }

        public async Task UpdateAsync(int id, CreateCategoryDto dto)
        {
            var category = await _categoryRepository.GetByIdAsync(id);

            if(category == null)
            {
                throw new KeyNotFoundException("Categori bulunamadı");
            }

            category.Name = dto.Name;
            _categoryRepository.Update(category);
            await _unitOfWork.SaveChangesAsync();
           
        }

        private static CategoryResponseDto MapToResponseDto(Category category)
        {
            return new CategoryResponseDto
            {
                Id = category.Id,
                Name = category.Name
            };
        }
    }
}
