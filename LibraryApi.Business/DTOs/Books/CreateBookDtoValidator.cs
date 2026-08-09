using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApi.Business.DTOs.Books
{
    public class CreateBookDtoValidator : AbstractValidator<CreateBookDto>
    {
        public CreateBookDtoValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Kitap adı boş olamaz.")
                .MaximumLength(200).WithMessage("Kitap adı en fazla 200 karakter olabilir.");

            RuleFor(x => x.ISBN)
                .MaximumLength(20).WithMessage("ISBN en fazla 20 karakter olabilir.")
                .When(x => !string.IsNullOrEmpty(x.ISBN));

            RuleFor(x => x.AuthorId)
                .GreaterThan(0).WithMessage("Geçerli bir yazar seçmelisiniz.");

            RuleFor(x => x.CategoryId)
                .GreaterThan(0).WithMessage("Geçerli bir kategori seçmelisiniz.");
        }
    }
}
