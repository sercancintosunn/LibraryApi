using LibraryApi.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApi.Business.Interfaces.Services
{
    public interface ILoanService
    {
        Task<Loan> CreateLoanAsync(int bookId, int memberId);
        Task ReturnLoanAsync(int loanId, int requestingMemberId, bool isAdmin);
    }
}
