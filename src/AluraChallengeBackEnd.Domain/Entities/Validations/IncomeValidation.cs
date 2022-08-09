namespace AluraChallengeBackEnd.Domain.Entities.Validations;

public class IncomeValidation : AbstractValidator<Income>
{
    public IncomeValidation()
    {
        RuleFor(i => i.Description)
            .NotEmpty()
            .WithMessage("The {PropertyName} field is required")
            .Length(3, 100)
            .WithMessage("The {PropertyName} field must contain between {MinLength} and {MaxLength} caracteres");;

        RuleFor(i => i.Value)
            .GreaterThanOrEqualTo(1)
            .WithMessage("The {PropertyName} must be greater then {ComparisonValue}");
    }
}