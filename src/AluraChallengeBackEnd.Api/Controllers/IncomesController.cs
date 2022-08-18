namespace AluraChallengeBackEnd.Api.Controllers;

[Route("api/[controller]")]
public class IncomesController : MainController
{
    private readonly IIncomeService _incomeService;
    private readonly IIncomeRepository _incomeRepository;
    private readonly IMapper _mapper;

    public IncomesController(IIncomeService incomeService,
                             IIncomeRepository incomeRepository, 
                             IMapper mapper, 
                             INotifier notifier) : base(notifier)
    {
        _incomeService = incomeService;
        _incomeRepository =  incomeRepository;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<IncomeViewModel>>> GetAll() =>
         CustomResponse(_mapper.Map<IEnumerable<IncomeViewModel>>(await _incomeRepository.GetAllAsync()));
 
    [HttpGet("{id:Guid}")]
    public async Task<ActionResult<IncomeViewModel>> Get(Guid id)
    {
        var incomeViewModel = _mapper.Map<IncomeViewModel>(await _incomeRepository.GetAsync(id));
        if (incomeViewModel is null) return NotFound();

        return CustomResponse(incomeViewModel);
    }

    [HttpPost]
    public async Task<ActionResult<IncomeViewModel>> Save(IncomeViewModel incomeViewModel)
    {
        if (!ModelState.IsValid) return CustomResponse(ModelState);
        
        var income = _mapper.Map<Income>(incomeViewModel);
        await _incomeService.CreateAsync(income);

        return CustomResponse(_mapper.Map<IncomeViewModel>(income), HttpStatusCode.Created);
    }

    [HttpPut("{id:Guid}")]
    public async Task<ActionResult<IncomeViewModel>> Edit(Guid id, IncomeViewModel incomeViewModel)
    {
        if (id != incomeViewModel.Id) return BadRequest();

        if (!ModelState.IsValid) return CustomResponse(ModelState);

       await _incomeService.EditAsync(_mapper.Map<Income>(incomeViewModel));

       return CustomResponse(incomeViewModel);
    }

    [HttpDelete("{id:Guid}")]
    public async Task<ActionResult<IncomeViewModel>> Delete(Guid id)
    {
        var incomeFromDb = await _incomeRepository.GetAsync(id);
        if (incomeFromDb is null) return NotFound();

        _incomeRepository.Delete(incomeFromDb);
        await _incomeRepository.UnitOfWork.CommitAsync();
        
        return CustomResponse(_mapper.Map<IncomeViewModel>(incomeFromDb));
    }  
}