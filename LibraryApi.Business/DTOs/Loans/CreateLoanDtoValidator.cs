using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApi.Business.DTOs.Loans
{
    public class CreateLoanDtoValidator  : AbstractValidator<CreateLoanDto>
    {
        public CreateLoanDtoValidator()
        {
            RuleFor(x => x.BookId)
                .GreaterThan(0).WithMessage("Geçerli bir kitap seçmelisiniz");

            RuleFor(x => x.MemberId)
                .GreaterThan(0).WithMessage("Geçerli bir üye seçmelisiniz");
        }
    }
}
