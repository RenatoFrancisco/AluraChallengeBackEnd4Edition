namespace AluraChallengeBackEnd.Domain.Entities.Validations;

public class ExpenditureValidation : AbstractValidator<Expenditure>
{
    public ExpenditureValidation()
    {
        RuleFor(e => e.Description)
            .NotEmpty()
            .WithMessage("The field {PropertyName} is required")
            .Length(3, 100)
            .WithMessage("The field {PropertyName} must contain between {MinLength} and {MaxLength} caracteres");;

        RuleFor(e => e.Value)
            .GreaterThanOrEqualTo(1)
            .WithMessage("The field {PropertyName} must be greater then {ComparisonValue}");

        RuleFor(e => e.CategoryExpenditureId)
            .NotEmpty()
            .WithMessage("The field {PropertyName} cannot be empty");

        RuleFor(e => e.CategoryExpenditure)
            .NotNull()
            .WithMessage("The field {PropertyName} cannot be null");
    }
}