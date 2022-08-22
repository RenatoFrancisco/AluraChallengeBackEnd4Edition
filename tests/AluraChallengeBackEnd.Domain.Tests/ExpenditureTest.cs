namespace AluraChallengeBackEnd.Domain.Tests;
public class ExpenditureTest
{
    [Fact]
    public void CreateExpenditure_WhenValid_DoesNotThrowsDomainException()
    {
        // Assert & Act
        var domainException = Record.Exception(() => new Expenditure("Aluguel", 2500.0m, DateTime.Now, "Moradia"));

        // Arrange
        Assert.Null(domainException);
    }

    [Fact]
    public void CreateExpenditure_WhenInvalid_ThrowsDomainException()
    {
        // Arrange & Act 
        var domainException = Assert.Throws<DomainException>(() => new Expenditure(string.Empty, 0, DateTime.Now, "Moradia"));

        var errors = domainException.Message.Split(';');

        // Assert
        Assert.NotEmpty(errors);
    }

    [Fact]
    public void CreateExpenditure_WhenCategoryIsNotNullOrEmpty_MustSetCategory()
    {
        // Arrange & Act
        const string category = "Moradia";
        var expenditure = new Expenditure("Aluguel", 2500.0m, DateTime.Now, category);

        // Assert
        Assert.Equal(category, expenditure.CategoryExpenditure.Description);
    }

    [Fact]
    public void CreateExpenditure_WhenCategoryIsNull_MustSetOutras()
    {
        // Arrange & Act
        var expenditure = new Expenditure("Sindicato", 50.0m, DateTime.Now, null);

        // Assert
        Assert.Equal("Outras", expenditure.CategoryExpenditure.Description);
    }

    [Fact]
    public void CreateExpenditure_WhenCategoryIsEmpty_MustSetOutras()
    {
        // Arrange & Act
        var expenditure = new Expenditure("Sindicato", 50.0m, DateTime.Now, string.Empty);

        // Assert
        Assert.Equal("Outras", expenditure.CategoryExpenditure.Description);
    }

    [Fact]
    public void CGetCategoryOrDefault_WhenCategoryIsNull_MustReturnsOutras()
    {
        // Arrange
        var expenditure = new Expenditure("Sindicato", 50.0m, DateTime.Now, null);

        // Act
        var category = expenditure.GetCategoryOrDefault();

        // Assert
        Assert.Equal("Outras", category);
    }

    [Fact]
    public void CGetCategoryOrDefault_WhenCategoryIsEmpty_MustReturnsOutras()
    {
        // Arrange
        var expenditure = new Expenditure("Sindicato", 50.0m, DateTime.Now, string.Empty);

        // Act
        var category = expenditure.GetCategoryOrDefault();

        // Assert
        Assert.Equal("Outras", category);
    }

    [Fact]
    public  void SetCategoryExpenditure()
    {
        // Arrange
        const string description = "Moradia";
        var expenditure = new Expenditure("Aluguel", 2500.0m, DateTime.Now, null);

        // Act
        expenditure.SetCategoryExpenditure(new CategoryExpenditure(description));

        // Assert
        Assert.Equal(expenditure.CategoryExpenditureId, expenditure.CategoryExpenditure.Id);
        Assert.Equal(expenditure.CategoryExpenditure.Description, description);
        Assert.Contains(expenditure, expenditure.CategoryExpenditure.Expenditures);
    }
}