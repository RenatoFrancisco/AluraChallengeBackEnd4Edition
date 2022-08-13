namespace AluraChallengeBackEnd.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class IncomesController : ControllerBase
{
    private readonly IIncomeRepository _incomeRepository;

    public IncomesController(IIncomeRepository incomeRepository)
    {
        _incomeRepository =  incomeRepository;
    }

    [HttpGet]
    public async Task<IEnumerable<Income>> GetAll()
    {
        return await _incomeRepository.GetAllAsync();
    }

    [HttpGet("{id:Guid}")]
    public async Task<ActionResult<Income>> Get(Guid id)
    {
        var income = await _incomeRepository.GetAsync(id);
        if (income is null) return NotFound();

        return income;
    }

    [HttpPost]
    public async Task<ActionResult> Save(Income income)
    {
        _incomeRepository.Save(income);
        await _incomeRepository.UnitOfWork.CommitAsync();

        return Ok();
    }
}