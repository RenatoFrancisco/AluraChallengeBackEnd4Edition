namespace AluraChallengeBackEnd.Domain.Entities.Validations;

public class ExpenditureValidation : AbstractValidator<Expenditure>
{
    public ExpenditureValidation()
    {
        RuleFor(i => i.Description)
            .NotEmpty()
            .WithMessage("The field {PropertyName} is required")
            .Length(3, 100)
            .WithMessage("The field {PropertyName} must contain between {MinLength} and {MaxLength} caracteres");;

        RuleFor(i => i.Value)
            .GreaterThanOrEqualTo(1)
            .WithMessage("The field {PropertyName} must be greater then {ComparisonValue}");
    }
}