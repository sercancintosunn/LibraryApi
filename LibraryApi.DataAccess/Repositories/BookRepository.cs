using LibraryApi.Business.Interfaces.Repositories;
using LibraryApi.DataAccess.Data;
using LibraryApi.Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApi.DataAccess.Repositories
{
    public class BookRepository : GenericRepository<Book>, IBookRepository
    {
        public BookRepository(LibraryDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Book>> GetByAuthorIdAsync(int authorId)
        {
            return await _dbSet
                .Include(b => b.Author)
                .Include(b => b.Category)
                .Where(b => b.AuthorId == authorId)
                .ToListAsync();
        }

        public new async Task<Book?> GetByIdAsync(int id)
        {
            return await _dbSet
                .Include(b => b.Author)
                .Include(b => b.Category)
                .FirstOrDefaultAsync(b => b.Id == id);
        }

        public new async Task<IEnumerable<Book>> GetAllAsync()
        {
            return await _dbSet
                .Include(b => b.Author)
                .Include(b => b.Category)
                .ToListAsync();
        }
    }
}
