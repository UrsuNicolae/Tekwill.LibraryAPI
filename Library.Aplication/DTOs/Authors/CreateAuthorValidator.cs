using FluentValidation;

namespace Library.Aplication.DTOs.Authors
{
    public class CreateAuthorValidator : AbstractValidator<CreateAuthorDto>
    {
        public CreateAuthorValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty()
                .WithMessage(ValidationMessages.PropertyMustNotBeEmpty(nameof(CreateAuthorDto.FirstName)))
                .NotNull()
                .WithMessage(ValidationMessages.PropertyIsRequired(nameof(CreateAuthorDto.FirstName)))
                .Length(1, 50)
                .WithMessage(ValidationMessages.PropertyHasInvalidLenght(nameof(CreateAuthorDto.FirstName)));

            RuleFor(x => x.LastName)
                .NotEmpty()
            .NotNull()
                .WithMessage(ValidationMessages.PropertyIsRequired(nameof(CreateAuthorDto.LastName)))
                .Length(1, 50)
                .WithMessage(ValidationMessages.PropertyHasInvalidLenght(nameof(CreateAuthorDto.LastName)));
        }
    }
}
