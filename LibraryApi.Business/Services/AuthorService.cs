using LibraryApi.Business.DTOs.Authors;
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
    public class AuthorService : IAuthorService
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AuthorService(IAuthorRepository authorRepository,IUnitOfWork unitOfWork)
        {
            _authorRepository = authorRepository;
            _unitOfWork = unitOfWork;
            
        }

        public async Task<AuthorResponseDto> CreateAsync(CreateAuthorDto dto)
        {
            var author = new Author
            {
                FullName = dto.FullName
            };
            await _authorRepository.AddAsync(author);
            await _unitOfWork.SaveChangesAsync();

            return MapToResponseDto(author);
        }

        public async Task DeleteAsync(int id)
        {
            var author = await _authorRepository.GetByIdAsync(id);
            if(author == null)
            {
                throw new KeyNotFoundException("Yazar Bulunamadı");
            }

            _authorRepository.Delete(author);
            await _unitOfWork.SaveChangesAsync();


                
        }

        public async Task<IEnumerable<AuthorResponseDto>> GetAllAsync()
        {
            var authors = await _authorRepository.GetAllAsync();
            return authors.Select(MapToResponseDto);
            
        }

        public async Task<AuthorResponseDto?> GetByIdAsync(int id)
        {
            var author = await _authorRepository.GetByIdAsync(id);
            if (author == null) return null;
            return MapToResponseDto(author);
        }

        public async Task UpdateAsync(int id, CreateAuthorDto dto)
        {
            var author = await _authorRepository.GetByIdAsync(id);
            if(author == null)
            {
                throw new KeyNotFoundException("Yazar bulunamadı");
            }
            author.FullName = dto.FullName;
            _authorRepository.Update(author);
            await _unitOfWork.SaveChangesAsync();

        }

        private static AuthorResponseDto MapToResponseDto(Author author)
        {
            return new AuthorResponseDto
            {
                Id = author.Id,
                FullName = author.FullName

            };
        }

        
    }
}
