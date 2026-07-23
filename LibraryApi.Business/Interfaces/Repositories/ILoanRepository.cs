using LibraryApi.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApi.Business.Interfaces.Repositories
{
    public interface ILoanRepository : IGenericRepository<Loan>
    {
        Task<Loan?> GetActiveLoanByMemberAsync(int memberId);
        new Task<Loan?> GetByIdAsync(int id);
        new Task<IEnumerable<Loan>> GetAllAsync();
    }
}
