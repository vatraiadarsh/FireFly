using Category.API.DataTransferObjects;
using FluentValidation;

namespace Category.API.Validator
{
    public class CategoryValidator : AbstractValidator<CategoryDto>
    {
        public CategoryValidator()
        {
            RuleFor(c => c.Name)
                .NotEmpty().WithMessage("Category name is required")
                .MaximumLength(50).WithMessage("Category name can't be longer than 50 characters");
            RuleFor(c => c.Notes)
                .MaximumLength(250).WithMessage("Category description can't be longer than 250 characters");
            RuleFor(c => c.Attachments)
                .Matches(@"^((http|https):\/\/)?([\da-z\.-]+)\.([a-z\.]{2,6})([\/\w \.-]*)*\/?$");
        }
    }
}
