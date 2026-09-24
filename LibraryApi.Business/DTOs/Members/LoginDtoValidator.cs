using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApi.Business.DTOs.Members
{
    public class LoginDtoValidator : AbstractValidator<LoginDto>
    {
        public LoginDtoValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email boş olamaz")
                .EmailAddress().WithMessage("Geçerli bir email adresi giriniz")
                .MaximumLength(256).WithMessage("Email en fazla 256 karakter olabilir");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Şifre boş olamaz");
        }
    }
}
