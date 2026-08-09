using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApi.Business.DTOs.Authors
{
    public class CreateAuthorDtoValidator : AbstractValidator<CreateAuthorDto>
    {
        public CreateAuthorDtoValidator()
        {
            RuleFor(x => x.FullName)
                .NotEmpty().WithMessage("Yazar adı boş olamaz")
                .MaximumLength(150).WithMessage("Yazar adı en fazla 150 karakter olabilir");

           

        }
    }
}
