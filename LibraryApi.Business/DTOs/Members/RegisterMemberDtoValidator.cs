using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApi.Business.DTOs.Members
{
    public class RegisterMemberDtoValidator : AbstractValidator<RegisterMemberDto>
    {
        public RegisterMemberDtoValidator()
        {
            RuleFor(x => x.FullName)
                .NotEmpty().WithMessage("İsim boş olamaz")
                .MaximumLength(150).WithMessage("İsim en fazla 150 karakter olabilir");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email boş olamaz")
                .EmailAddress().WithMessage("Geçerli bir email giriniz");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Şifre boş olamaz")
                .MinimumLength(6).WithMessage("Şifre en az  6 karakter olmalıdır");


        }
    }
}
