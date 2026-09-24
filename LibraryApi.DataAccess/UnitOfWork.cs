using LibraryApi.Business.Interfaces;
using LibraryApi.DataAccess.Data;
using LibraryApi.Business.Exceptions;
using LibraryApi.Entities.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApi.DataAccess
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly LibraryDbContext _context;

        public UnitOfWork(LibraryDbContext context)
        {
            _context = context;
        }
        public async Task<int> SaveChangesAsync()
        {
            try
            {
                return await _context.SaveChangesAsync();
            }
            catch (DbUpdateException exception) when (
                exception.GetBaseException() is SqlException { Number: 2601 or 2627 } &&
                exception.Entries.Any(entry => entry.Entity is Member))
            {
                throw new DuplicateEmailException();
            }
        }
    }
}
