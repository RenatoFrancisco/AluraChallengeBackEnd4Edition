namespace AluraChallengeBackEnd.Domain.Tests.Entities;

public class IncomeTest
{
    [Fact]
    public void CreateIncome_WhenValid_DoesNotThrowDomainException()
    {
        // Arrange & Act
        var domainException = Record.Exception(() => new Income("Salary", 1200.0m, DateTime.Now));

        // Assert
        Assert.Null(domainException);
    }

    [Fact]
    public void CreateIncome_WhenInvalid_ThrowsDomainException()
    {
        // Arrange & Act 
        var domainException = Assert.Throws<DomainException>(() => new Income(string.Empty, 0, DateTime.Now));

        var errors = domainException.Message.Split(';');

        // Assert
        Assert.NotEmpty(errors);
    }

    [Fact]
    public void AddExpenditure()
    {
        // Arrange
        const string description = "Moradia";
        var category = new CategoryExpenditure(description);
        var expenditure = new Expenditure("Aluguel", 2500.0m, DateTime.Now, description);

        // Act
        category.AddExpenditure(expenditure);

        // Assert
        Assert.Equal(1, category.Expenditures.Count);
        Assert.Equal(expenditure, category.Expenditures.First());
    }
}