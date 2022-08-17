namespace AluraChallengeBackEnd.Domain.Entities.Validations;
public class CategoryExpenditureValidation : AbstractValidator<CategoryExpenditure>
{
    public CategoryExpenditureValidation()
    {
        RuleFor(c => c.Description)
            .NotEmpty()
            .WithMessage("The field {PropertyName} is required")
            .Length(3, 100)
            .WithMessage("The field {PropertyName} must contain between {MinLength} and {MaxLength} caracteres");;   
    }
}