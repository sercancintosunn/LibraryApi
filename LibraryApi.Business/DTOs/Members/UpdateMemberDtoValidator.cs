using FluentValidation;

namespace LibraryApi.Business.DTOs.Members
{
    public class UpdateMemberDtoValidator : AbstractValidator<UpdateMemberDto>
    {
        public UpdateMemberDtoValidator()
        {
            RuleFor(member => member.FullName)
                .NotEmpty().WithMessage("İsim boş olamaz")
                .MaximumLength(150).WithMessage("İsim en fazla 150 karakter olabilir");

            RuleFor(member => member.Email)
                .NotEmpty().WithMessage("Email boş olamaz")
                .EmailAddress().WithMessage("Geçerli bir email giriniz")
                .MaximumLength(256).WithMessage("Email en fazla 256 karakter olabilir");

            RuleFor(member => member.NewPassword)
                .MinimumLength(6).WithMessage("Şifre en az 6 karakter olmalıdır")
                .When(member => !string.IsNullOrWhiteSpace(member.NewPassword));
        }
    }
}
