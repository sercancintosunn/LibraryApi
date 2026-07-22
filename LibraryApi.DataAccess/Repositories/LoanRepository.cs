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
    }
}
