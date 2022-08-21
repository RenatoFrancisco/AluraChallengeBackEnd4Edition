namespace AluraChallengeBackEnd.Domain.Tests;
public class IncomeTest
{
    [Fact]
    public void CreateIncome_WhenValid_DoesNotThrowsDomainException()
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
}