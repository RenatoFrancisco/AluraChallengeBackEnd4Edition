namespace AluraChallengeBackEnd.Api.Controllers;

[Route("api/[controller]")]
public class SummaryController : MainController
{
    private readonly ISummaryService _summaryService;

    public SummaryController(ISummaryService summaryService, INotifier notifier) : base(notifier) =>
        _summaryService = summaryService;

    [HttpGet("{year:int}/{month:int}")]
    public async Task<ActionResult<SummaryDTO>> Get(int year, int month) =>
        CustomResponse(await _summaryService.GetSummaryAsync(year, month));
}