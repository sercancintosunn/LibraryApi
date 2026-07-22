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

        public async Task<Book> CreateBookAsync(Book book)
        {
            await _bookRepository.AddAsync(book);
            await _unitOfWork.SaveChangesAsync();
            return book;
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

        public async Task<IEnumerable<Book>> GetAllAsync()
        {
            return await _bookRepository.GetAllAsync();
            
        }

        public async Task<Book?> GetByIdAsync(int id)
        {
            return await _bookRepository.GetByIdAsync(id);
            
        }

        public async Task UpdateBookAsync(Book book)
        {
            var existing = await _bookRepository.GetByIdAsync(book.Id);

            if(existing == null)
            {
                throw new KeyNotFoundException("Kitap Bulunamadı");
            }

            _bookRepository.Update(book);
            await _unitOfWork.SaveChangesAsync();
            
        }
    }   
}
