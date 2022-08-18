namespace AluraChallengeBackEnd.Domain.Entities;

public class Expenditure : Entity
{
    public string Description { get; private set; }
    public decimal Value { get; private set; }
    public DateTime DateExpenditure { get; private set; }
    public DateTime CreatedOn { get; private set; }

    public Guid CategoryExpenditureId { get; private set; }
    public CategoryExpenditure CategoryExpenditure { get; private set; }

    protected Expenditure() { }

    public Expenditure(string description,
                  decimal value, 
                  DateTime dateExpenditure,
                  string category) 
    {
        Description = description;
        Value = value;
        DateExpenditure = dateExpenditure;
        CategoryExpenditure = new CategoryExpenditure(GetCategoryOrDefault(category));
        CategoryExpenditureId = CategoryExpenditure.Id;

        Validate();
    }
    
    public void SetCategoryExpenditure(CategoryExpenditure category)
    {
        CategoryExpenditure = category;
        CategoryExpenditureId = category.Id;
        CategoryExpenditure.AddExpenditure(this);
    }

    public string GetCategoryOrDefault() => GetCategoryOrDefault(CategoryExpenditure.Description);

    public override void Validate()
    {
        var validation = new ExpenditureValidation().Validate(this);
        if (!validation.IsValid)
        {
            var errors =  string.Join(';', validation.Errors);
            throw new DomainException(errors);
        }
    }

    private string GetCategoryOrDefault(string category) =>
        string.IsNullOrWhiteSpace(category)
        ? "Outras"
        : category;
}