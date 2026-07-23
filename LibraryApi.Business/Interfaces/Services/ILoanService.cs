using LibraryApi.Business.DTOs.Loans;
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
        Task<LoanResponseDto?> GetByIdAsync(int id);
        Task<IEnumerable<LoanResponseDto>> GetAllAsync();
        Task<LoanResponseDto> CreateLoanAsync(CreateLoanDto dto)
        Task ReturnLoanAsync(int loanId, int requestingMemberId, bool isAdmin);
    }
}
