namespace AluraChallengeBackEnd.Domain.Entities;

public class Expenditure : Entity
{
    public string Description { get; private set; }
    public decimal Value { get; private set; }
    public DateTime DateExpenditure { get; private set; }
    public DateTime CreatedOn { get; private set; }

    protected Expenditure() { }

    public Expenditure(string description,
                  decimal value, 
                  DateTime dateExpenditure) 
    {
        Description = description;
        Value = value;
        DateExpenditure = dateExpenditure;
        
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