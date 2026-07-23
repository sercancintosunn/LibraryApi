using LibraryApi.Business.DTOs.Books;
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
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;
        private readonly IUnitOfWork _unitOfWork;

        public BookService(IBookRepository bookRepository,IUnitOfWork unitOfWork)
        {
            _bookRepository = bookRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<BookResponseDto> CreateBookAsync(CreateBookDto dto)
        {
            var book = new Book
            {
                Title = dto.Title,
                ISBN = dto.ISBN,
                AuthorId = dto.AuthorId,
                CategoryId = dto.CategoryId
            };

            await _bookRepository.AddAsync(book);
            await _unitOfWork.SaveChangesAsync();

            var created = await _bookRepository.GetByIdAsync(book.Id);
            return MapToResponseDto(created!);
        }

        public async Task DeleteBookAsync(int id)
        {
            var book = await _bookRepository.GetByIdAsync(id);

            if(book == null)
            {
                throw new KeyNotFoundException("Kitap bulunamadı");
            }

            _bookRepository.Delete(book);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<IEnumerable<BookResponseDto>> GetAllAsync()
        {
            var books = await _bookRepository.GetAllAsync();
            return books.Select(MapToResponseDto);
        }

        public async Task<BookResponseDto?> GetByIdAsync(int id)
        {
            var book = await _bookRepository.GetByIdAsync(id);
            if (book == null) return null;
            return MapToResponseDto(book);
        }

        public async Task UpdateBookAsync(int id, CreateBookDto dto)
        {
            var book = await _bookRepository.GetByIdAsync(id);

            if(book == null)
            {
                throw new KeyNotFoundException("Kitap bulunamadı");
            }

            book.Title = dto.Title;
            book.ISBN = dto.ISBN;
            book.AuthorId = dto.AuthorId;
            book.CategoryId = dto.CategoryId;

            _bookRepository.Update(book);
            await _unitOfWork.SaveChangesAsync();
        }

        private static BookResponseDto MapToResponseDto(Book book)
        {
            return new BookResponseDto
            {
                Id = book.Id,
                Title = book.Title,
                ISBN = book.ISBN,
                AuthorName = book.Author?.FullName ?? string.Empty,
                CategoryName = book.Category?.Name ?? string.Empty
            };
        }
    }   
}
