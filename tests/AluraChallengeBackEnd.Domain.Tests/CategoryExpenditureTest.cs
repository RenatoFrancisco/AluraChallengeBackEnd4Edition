namespace AluraChallengeBackEnd.Domain.Tests;

public class CategoryExpenditureTest
{
    [Fact]
    public void CreateCategooryExpenditure_WhenValid_DoesNotThrowDomainException()
    {
        // Arrange & Act
        var domainException = Record.Exception(() => new CategoryExpenditure("Moradia"));

        // Assert
        Assert.Null(domainException);
    }

        [Fact]
    public void CreateCategooryExpenditure_WhenInvalid_ThrowDomainException()
    {
        // Arrange & Act
        var domainException = Assert.Throws<DomainException>(() => new CategoryExpenditure(string.Empty));

        var errors = domainException.Message.Split(';');

        // Assert
        Assert.NotEmpty(errors);
    }
}