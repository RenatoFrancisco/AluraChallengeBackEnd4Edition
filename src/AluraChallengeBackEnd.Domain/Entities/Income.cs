namespace AluraChallengeBackEnd.Domain.Entities;

public class Income : Entity
{
    public string Description { get; private set; }
    public decimal Value { get; private set; }
    public DateTime DateIncome { get; private set; }
    public DateTime CreatedOn { get; private set; }

    protected Income() { }

    public Income(string description,
                  decimal value, 
                  DateTime dateIncome) 
    {
        Description = description;
        Value = value;
        DateIncome = dateIncome;
        CreatedOn = DateTime.Now;

        Validate();
    }
    
    public override void Validate()
    {
        var validation = new IncomeValidation().Validate(this);
        if (!validation.IsValid)
        {
            var errors =  string.Join(';', validation.Errors);
            throw new DomainException(errors);
        }
    }
}