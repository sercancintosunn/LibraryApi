using LibraryApi.Business.DTOs.Loans;
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

        public async Task<LoanResponseDto> CreateLoanAsync(CreateLoanDto dto)
        {
            var activeLoan = await _loanRepository.GetActiveLoanByMemberAsync(dto.MemberId);

            if(activeLoan != null)
            {
                throw new InvalidOperationException("Bu üyenin iade etmediği kitap var");
            }

            var newLoan = new Loan
            {
                BookId = dto.BookId,
                MemberId = dto.MemberId,
                LoanDate = DateTime.UtcNow
            };

            await _loanRepository.AddAsync(newLoan);
            await _unitOfWork.SaveChangesAsync(); ;

            var created = await _loanRepository.GetByIdAsync(newLoan.Id);
            return MapToResponseDto(created!);
            
        }

        public async Task<IEnumerable<LoanResponseDto>> GetAllAsync()
        {
            var loans = await _loanRepository.GetAllAsync();
            return loans.Select(MapToResponseDto);

        }

        public async Task<LoanResponseDto?> GetByIdAsync(int id)
        {
            var loan = await _loanRepository.GetByIdAsync(id);
            return loan == null ? null : MapToResponseDto(loan);
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
                throw new UnauthorizedAccessException("Bu ödünç kaydını iade etme hakkınız yok");
            }

            if(loan.ReturnDate != null)
            {
                throw new InvalidOperationException("Bu kitap zaten iade edilmiş");
            }

            loan.ReturnDate = DateTime.UtcNow;
            _loanRepository.Update(loan);
            await _unitOfWork.SaveChangesAsync();
           
        }


        private static LoanResponseDto MapToResponseDto(Loan loan)
        {
            return new LoanResponseDto
            {
                Id = loan.Id,
                BookTitle = loan.Book?.Title ?? string.Empty,
                MemberName = loan.Member?.FullName ?? string.Empty,
                LoanDate = loan.LoanDate,
                ReturnDate = loan.ReturnDate
            };
        }
    }
}
