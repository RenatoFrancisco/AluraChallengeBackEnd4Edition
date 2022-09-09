
namespace AluraChallengeBackEnd.Api.Tests;

public class IncomeTest
{
    private readonly IntegrationTestsFixture<StartupTest> _testFixture;

    public IncomeTest(IntegrationTestsFixture<StartupTest> testFixture) => _testFixture = testFixture;

    [Fact, TestPriority(1)]
    public async Task CreateAsync_ValidIncome_MustReturn201StatusCode()
    {
        // Assert
        var validIncome = new Income("Salary", 1300.0m, DateTime.Now);

        // Act
        var postResponse = await _testFixture.Client.PostAsJsonAsync("api/incomes", validIncome);

        // Arrange
        Assert.Equal(HttpStatusCode.Created, postResponse.StatusCode);
    }
}
