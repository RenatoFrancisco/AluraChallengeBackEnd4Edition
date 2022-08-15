namespace AluraChallengeBackEnd.Domain.Notifications;

public class Notifier : INotifier
{
    private IList<Notification> _notifications;

    public Notifier() => _notifications = new List<Notification>();

    public IList<Notification> GetNotifications() => _notifications;

    public void Handle(Notification notification) => _notifications.Add(notification);

    public bool HasNotification() => _notifications.Any();
}