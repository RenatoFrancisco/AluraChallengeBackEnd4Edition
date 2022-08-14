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
    public async Task<ActionResult<Income>> Save(Income income)
    {
        if (!ModelState.IsValid) return BadRequest();

        _incomeRepository.Save(income);
        await _incomeRepository.UnitOfWork.CommitAsync();

        return Ok(income);
    }

    [HttpPut("{id:Guid}")]
    public async Task<ActionResult<Income>> Edit(Guid id, Income income)
    {
        if (id != income.Id) return BadRequest();

        if (!ModelState.IsValid) return BadRequest();

       _incomeRepository.Edit(income);
       await _incomeRepository.UnitOfWork.CommitAsync();

       return Ok(income);
    }

    [HttpDelete("{id:Guid}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        var incomeFromDb = await _incomeRepository.GetAsync(id);
        if (incomeFromDb is null) return NotFound();

        _incomeRepository.Delete(incomeFromDb);
        await _incomeRepository.UnitOfWork.CommitAsync();
        return Ok();
    }
}