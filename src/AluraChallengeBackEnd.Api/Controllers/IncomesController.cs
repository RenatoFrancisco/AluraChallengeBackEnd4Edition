namespace AluraChallengeBackEnd.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class IncomesController : ControllerBase
{
    private readonly IIncomeRepository _incomeRepository;
    private readonly IMapper _mapper;

    public IncomesController(IIncomeRepository incomeRepository, IMapper mapper)
    {
        _incomeRepository =  incomeRepository;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IEnumerable<IncomeViewModel>> GetAll() =>
         _mapper.Map<IEnumerable<IncomeViewModel>>(await _incomeRepository.GetAllAsync());

    [HttpGet("{id:Guid}")]
    public async Task<ActionResult<IncomeViewModel>> Get(Guid id)
    {
        var incomeViewModel = _mapper.Map<IncomeViewModel>(await _incomeRepository.GetAsync(id));
        if (incomeViewModel is null) return NotFound();

        return incomeViewModel;
    }

    [HttpPost]
    public async Task<ActionResult<Income>> Save(IncomeViewModel incomeViewModel)
    {
        if (!ModelState.IsValid) return BadRequest();

        _incomeRepository.Save(_mapper.Map<Income>(incomeViewModel));
        await CommitAsync();

        return Ok(incomeViewModel);
    }

    [HttpPut("{id:Guid}")]
    public async Task<ActionResult<IncomeViewModel>> Edit(Guid id, IncomeViewModel incomeViewModel)
    {
        if (id != incomeViewModel.Id) return BadRequest();

        if (!ModelState.IsValid) return BadRequest();

       _incomeRepository.Edit(_mapper.Map<Income>(incomeViewModel));
       await CommitAsync();

       return Ok(incomeViewModel);
    }

    [HttpDelete("{id:Guid}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        var incomeFromDb = await _incomeRepository.GetAsync(id);
        if (incomeFromDb is null) return NotFound();

        _incomeRepository.Delete(incomeFromDb);
        await CommitAsync();
        
        return Ok();
    }

    private async Task<bool> CommitAsync() => await _incomeRepository.UnitOfWork.CommitAsync();
}