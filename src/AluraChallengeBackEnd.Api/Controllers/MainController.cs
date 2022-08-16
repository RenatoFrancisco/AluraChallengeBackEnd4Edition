namespace AluraChallengeBackEnd.Api.Controllers;

[ApiController]
public class MainController : ControllerBase
{
    private readonly INotifier _notifier;

    public MainController(INotifier notifier) => _notifier = notifier;

    protected bool ValidOperation() => !_notifier.HasNotification();

    protected ActionResult CustomResponse(object? result = null, HttpStatusCode statusCode = HttpStatusCode.OK)
    {
        if (ValidOperation())
        {
            return StatusCode((int)statusCode, new
            {
                success = true,
                data = result
            });
        }

        return BadRequest(new
        {
            success = false,
            errors = _notifier.GetNotifications().Select(n => n.Message)
        });
    }

    protected ActionResult CustomResponse(ModelStateDictionary modelState)
    {
        if (!modelState.IsValid) NotifyErrorModelInvalid(modelState);
        return CustomResponse();
    }

    protected void NotifyErrorModelInvalid(ModelStateDictionary modelState)
    {
        var errors = modelState.Values.SelectMany(e => e.Errors);
        foreach (var error in errors)
        {
            var errorMsg = error.Exception == null ? error.ErrorMessage : error.Exception.Message;
            NotifyError(errorMsg);
        }
    }

    protected void NotifyError(string message)
    {
        _notifier.Handle(new Notification(message));
    }
}