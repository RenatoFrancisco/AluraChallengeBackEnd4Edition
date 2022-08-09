namespace AluraChallengeBackEnd.Domain.Entities;

public class Expenditure : Entity
{
    public string Description { get; private set; }
    public decimal Value { get; private set; }
    public DateTime DateIncome { get; private set; }

    public Expenditure(string description,
                  decimal value, 
                  DateTime dateIncome) 
    {
        Description = description;
        Value = value;
        DateIncome = dateIncome;

        Validate();
    }
    
    public override void Validate()
    {
        var validation = new ExpenditureValidation().Validate(this);
        if (!validation.IsValid)
        {
            var errors =  string.Join(';', validation.Errors);
            throw new DomainException(errors);
        }
    }
}