using FluentValidation;

namespace Library.Aplication.DTOs
{
    public class PaginatedDtoValidator : AbstractValidator<PaginatedDto>
    {
        public PaginatedDtoValidator()
        {
            RuleFor(p => p.Page)
                .Must(p => p > 0)
                .WithMessage(ValidationMessages.PropertyHasInvalidValue(nameof(PaginatedDto.Page)));
            RuleFor(p => p.PageSize)
                .Must(p => p > 0)
                .WithMessage(ValidationMessages.PropertyHasInvalidValue(nameof(PaginatedDto.Page)));
        }
    }
}
