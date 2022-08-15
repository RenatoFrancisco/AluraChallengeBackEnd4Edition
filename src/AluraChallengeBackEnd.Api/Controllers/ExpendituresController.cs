namespace AluraChallengeBackEnd.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ExpendituresController : ControllerBase
{
    private readonly IExpenditureRepository _expenditureRepository;
    private readonly IMapper _mapper;

    public ExpendituresController(IExpenditureRepository expenditureRepository, IMapper mapper)
    {
        _expenditureRepository = expenditureRepository;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IEnumerable<ExpenditureViewModel>> GetAll() =>
        _mapper.Map<IEnumerable<ExpenditureViewModel>>(await _expenditureRepository.GetAllAsync());

    [HttpGet("{id:Guid}")]
    public async Task<ActionResult<ExpenditureViewModel>> Get(Guid id)
    {
        var expenditureViewModel = _mapper.Map<ExpenditureViewModel>(await _expenditureRepository.GetAsync(id));
        if (expenditureViewModel is null) NotFound();

        return Ok(expenditureViewModel);
    }

    [HttpPost]
    public async Task<ActionResult<ExpenditureViewModel>> Save(ExpenditureViewModel expenditureViewModel)
    {
        if (!ModelState.IsValid) return BadRequest();

        var expenditure = _mapper.Map<Expenditure>(expenditureViewModel);
        _expenditureRepository.Save(expenditure);
        await CommitAsync();

        return CreatedAtAction(nameof(Get), new { Id = expenditure.Id }, _mapper.Map<ExpenditureViewModel>(expenditure));
    }

    [HttpPut("{id:Guid}")]
    public async Task<ActionResult<IncomeViewModel>> Edit(Guid id, ExpenditureViewModel expenditureViewModel)
    {
        if (id != expenditureViewModel.Id) return BadRequest();

        if (!ModelState.IsValid) return BadRequest();

        _expenditureRepository.Edit(_mapper.Map<Expenditure>(expenditureViewModel));
        await CommitAsync();

        return Ok(expenditureViewModel);
    }

    [HttpDelete("{id:Guid}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        var expenditureFromDb = await _expenditureRepository.GetAsync(id);
        if (expenditureFromDb is null) return NotFound();

        _expenditureRepository.Delete(expenditureFromDb);
        await CommitAsync();
        
        return Ok();
    }

    private async Task<bool> CommitAsync() => await _expenditureRepository.UnitOfWork.CommitAsync();
}