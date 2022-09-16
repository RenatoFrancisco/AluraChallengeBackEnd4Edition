namespace AluraChallengeBackEnd.Api.Tests;

[TestCaseOrderer("AluraChallengeBackEnd.Api.Tests.Attributes.PriorityOrderer", "AluraChallengeBackEnd.Api.Tests")]
public class ExpendituresControllerTest : IClassFixture<WebApplicationFactory<StartupTest>>
{
    private readonly WebApplicationFactory<StartupTest> _factory;
    private readonly HttpClient _client;
    private readonly Expenditure _validExpenditure;

    public ExpendituresControllerTest(WebApplicationFactory<StartupTest> factory)
    {
        _factory = factory;
        _client = _factory.CreateClient();
        _validExpenditure = new Expenditure("Aluguel", 2500.0m, DateTime.Now, "Moradia");
    }

    [Fact, TestPriority(1)]
    public async Task GetAll_WhenDescriptionDoesNotExist_MustReturn404()
    {
        // Arrange & Act
        var response = await _client.GetAsync($"api/expenditures?description={_validExpenditure.Description}");

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact, TestPriority(2)]
    public async Task Create_ValidIncome_MustReturn201StatusCode()
    {
        // Arrange & Act
        var response = await _client.PostAsJsonAsync("api/expenditures", _validExpenditure);

        // Assert
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        Assert.Contains("\"success\":true", await response.Content.ReadAsStringAsync());
    }

    [Fact, TestPriority(3)]
    public async Task GetAll_WhenDescriptionExist_MustReturn200()
    {
        // Arrange & Act
        var response = await _client.GetAsync($"api/expenditures?description={_validExpenditure.Description}");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact, TestPriority(4)]
    public async Task Create_WhenDuplicate_MustReturn400StatusCode()
    {
        // Arrange & Act
        var response = await _client.PostAsJsonAsync("api/expenditures", _validExpenditure);

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        Assert.Contains("\"success\":false", await response.Content.ReadAsStringAsync());
    }

    [Fact, TestPriority(5)]
    public async Task GetByYearAndMonth_WhenExists_MustReturn200()
    {
        // Arrange & Act
        var response = await _client.GetAsync($"api/expenditures/{_validExpenditure.DateExpenditure.Year}/{_validExpenditure.DateExpenditure.Month}");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact, TestPriority(6)]
    public async Task Get_WhentExists_MustReturn200()
    {
        // Arrange 
        var response = await _client.GetAsync($"api/expenditures?description={_validExpenditure.Description}");
        var id = ExtractId(await response.Content.ReadAsStringAsync());

        // Act
        response = await _client.GetAsync($"api/expenditures/{id}");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact, TestPriority(99)]
    public async Task Delete_WhenIdtExists_MustReturn200()
    {
        // Arrange 
        var response = await _client.GetAsync($"api/expenditures?description={_validExpenditure.Description}");
        var id = ExtractId(await response.Content.ReadAsStringAsync());

        // Act
        response = await _client.DeleteAsync($"api/expenditures/{id}");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetByYearAndMonth_WhenDoestNotExist_MustReturn200()
    {
        // Arrange & Act
        var response = await _client.GetAsync($"api/expenditures/2100/1");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetAll_WhenDescriptionIsNull_MustReturn200()
    {
        // Arrange & Act
        var response = await _client.GetAsync($"api/expenditures");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task Get_WhenIdDoesNotExist_MustReturn404()
    {
        // Arrange & Act
        var response = await _client.GetAsync("api/expenditures/c36bfa05-9724-449c-b4bc-5ae322b55e71");

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task Edit_WhenIdsDoesNotMatch_MustReturn400()
    {
        // Arrange & Act
        var response = await _client.PutAsJsonAsync("api/expenditures/c36bfa05-9724-449c-b4bc-5ae322b55e71", _validExpenditure);

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task Delete_WhenIdDoesNotExist_MustReturn404()
    {
        // Arrange & Act
        var response =  await _client.DeleteAsync("api/expenditures/c36bfa05-9724-449c-b4bc-5ae322b55e71");

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    private Guid ExtractId(string result)
    {
        var idPosition = result.IndexOf("\"id\":");
		var id = result.Substring(idPosition + 6, 36);

        return Guid.Parse(id);
    }
}
