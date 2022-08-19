namespace AluraChallengeBackEnd.Api.Controllers;

[Route("api/[controller]")]
public class ExpendituresController : MainController
{
    private readonly IExpenditureService _expenditureService;
    private readonly IExpenditureRepository _expenditureRepository;
    private readonly IMapper _mapper;

    public ExpendituresController(IExpenditureService expenditureService,
                                 IExpenditureRepository expenditureRepository, 
                                 IMapper mapper, 
                                 INotifier notifier) : base(notifier)
    {
        _expenditureService = expenditureService;
        _expenditureRepository = expenditureRepository;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ExpenditureViewModel>>> GetAll(string? description = null) 
    {
        if (string.IsNullOrWhiteSpace(description))
            return CustomResponse(_mapper.Map<IEnumerable<ExpenditureViewModel>>(await _expenditureRepository.GetAllAsync()));

        var expenditureViewModel = _mapper.Map<ExpenditureViewModel>(await _expenditureRepository.GetByDescriptionAsync(description));
        if (expenditureViewModel is null) return NotFound();

        return CustomResponse(expenditureViewModel);
    }

    [HttpGet("{id:Guid}")]
    public async Task<ActionResult<ExpenditureViewModel>> Get(Guid id)
    {
        var expenditureViewModel = _mapper.Map<ExpenditureViewModel>(await _expenditureRepository.GetAsync(id));
        if (expenditureViewModel is null) return NotFound();

        return CustomResponse(expenditureViewModel);
    }

    [HttpGet("{year:int}/{month:int}")]
    public async Task<ActionResult<IEnumerable<ExpenditureViewModel>>> GetByYearAndMonth(int year, int month) =>
        CustomResponse(_mapper.Map<IEnumerable<ExpenditureViewModel>>(await _expenditureRepository.GetByYearAndMonthAsync(year, month)));

    [HttpPost]
    public async Task<ActionResult<ExpenditureViewModel>> Save(ExpenditureViewModel expenditureViewModel)
    {
        if (!ModelState.IsValid) return CustomResponse(ModelState);

        var expenditure = _mapper.Map<Expenditure>(expenditureViewModel);
        await _expenditureService.CreateAsync(expenditure);

        return CustomResponse(_mapper.Map<ExpenditureViewModel>(expenditure), HttpStatusCode.Created);
    }

    [HttpPut("{id:Guid}")]
    public async Task<ActionResult<IncomeViewModel>> Edit(Guid id, ExpenditureViewModel expenditureViewModel)
    {
        if (id != expenditureViewModel.Id) return BadRequest();

        if (!ModelState.IsValid) return CustomResponse(ModelState);

        var expenditure = _mapper.Map<Expenditure>(expenditureViewModel);
        await _expenditureService.EditAsync(expenditure);

        return CustomResponse(_mapper.Map<ExpenditureViewModel>(expenditure));
    }

    [HttpDelete("{id:Guid}")]
    public async Task<ActionResult<ExpenditureViewModel>> Delete(Guid id)
    {
        var expenditureFromDb = await _expenditureRepository.GetAsync(id);
        if (expenditureFromDb is null) return NotFound();

        _expenditureRepository.Delete(expenditureFromDb);
        await _expenditureRepository.UnitOfWork.CommitAsync();
        
        return CustomResponse(_mapper.Map<ExpenditureViewModel>(expenditureFromDb));
    }
}