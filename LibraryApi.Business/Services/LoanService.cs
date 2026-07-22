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
    public class LoanService : ILoanService
    {
        private readonly ILoanRepository _loanRepository;
        private readonly IUnitOfWork _unitOfWork;

        public LoanService(ILoanRepository loanRepository,IUnitOfWork unitOfWork)
        {
            _loanRepository = loanRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Loan> CreateLoanAsync(int bookId, int memberId)
        {
            var activeLoan = await _loanRepository.GetActiveLoanByMemberAsync(memberId);

            if(activeLoan != null)
            {
                throw new InvalidOperationException("Bu üyenin iade etmediği kitap var. Önce onu iade etmesi gerekiyor");
            }

            var newLoan = new Loan
            {
                BookId = bookId,
                MemberId = memberId,
                LoanDate = DateTime.UtcNow
            };

            await _loanRepository.AddAsync(newLoan);
            await _unitOfWork.SaveChangesAsync();
            return newLoan;
        }

        public async Task ReturnLoanAsync(int loanId, int requestingMemberId, bool isAdmin)
        {
            var loan = await _loanRepository.GetByIdAsync(loanId);

            if(loan == null)
            {
                throw new KeyNotFoundException("Ödünç kaydı bulunamadı");
            }

            if(isAdmin && loan.MemberId != requestingMemberId)
            {
                throw new UnauthorizedAccessException("Bu ödünç kaydını iade etme yetkiniz yok.");
            }

            if(loan.ReturnDate != null)
            {
                throw new InvalidOperationException("Bu kitap zaten iade edilmiş");
            }

            loan.ReturnDate = DateTime.UtcNow;
            _loanRepository.Update(loan);
            await _unitOfWork.SaveChangesAsync();
             
        }
    }
}
