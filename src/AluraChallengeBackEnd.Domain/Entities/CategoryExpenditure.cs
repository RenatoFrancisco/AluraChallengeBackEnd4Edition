namespace AluraChallengeBackEnd.Domain.Entities;
public class CategoryExpenditure : Entity
{
    public string Description { get; init; }

    public ICollection<Expenditure> Expenditures { get; private set; } = new List<Expenditure>();

    protected CategoryExpenditure() { }

    public CategoryExpenditure(string description) 
    {
        Description = description;
        Validate();
    }

    public void AddExpenditure(Expenditure expenditure) => Expenditures.Add(expenditure);

    public override void Validate()
    {
        var validation = new CategoryExpenditureValidation().Validate(this);
        if (!validation.IsValid)
        {
            var errors =  string.Join(';', validation.Errors);
            throw new DomainException(errors);
        }
    }
}