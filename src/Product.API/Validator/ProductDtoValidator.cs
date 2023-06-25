using API.Dto;
using FluentValidation;

namespace API.Validator;

public class ProductValidator : AbstractValidator<ProductDto>
{
    public ProductValidator()
    {
        RuleFor(x => x.Name)
            .NotNull()
            .NotEmpty()
            .Length(1, 50);

        RuleFor(x => x.Description)
            .Length(0, 500);

        RuleFor(x => x.Price).GreaterThan(0);
    }
}