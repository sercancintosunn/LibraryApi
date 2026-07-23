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
    public class LoanRepository : GenericRepository<Loan>, ILoanRepository
    {
        public LoanRepository(LibraryDbContext context) : base(context)
        {
        }

        public async Task<Loan?> GetActiveLoanByMemberAsync(int memberId)
        {
            return await _dbSet.FirstOrDefaultAsync(l => l.MemberId == memberId && l.ReturnDate == null);
           
        }

        public new async Task<Loan?> GetByAsync(int id)
        {
            return await _dbSet
                .Include(l => l.Book)
                .Include(l => l.Member)
                .FirstOrDefaultAsync(l => l.Id == id);
        }

        public new async Task<IEnumerable<Loan>> GetAllAsync()
        {
            return await _dbSet
                .Include(l => l.Book)
                .Include(l => l.Member)
                .ToListAsync();
        }
    }
}
