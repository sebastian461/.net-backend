using Backend.DTOs;
using FluentValidation;

namespace Backend.Validators
{
    public class BeerInsertValidator : AbstractValidator<BeerInsertDto>
    {
        public BeerInsertValidator() 
        { 
            RuleFor(x => x.Name).NotEmpty().WithMessage("El nombre es obligatorio");
            RuleFor(x => x.Alcohol).NotNull().WithMessage("El grado de alcohol es obligatorio");
            RuleFor(x => x.BrandId).NotNull().WithMessage("La {PropertyName} es obligatoria");
            RuleFor(x => x.BrandId).GreaterThan(0).WithMessage("La {PropertyName} es obligatoria");
        }
    }
}
