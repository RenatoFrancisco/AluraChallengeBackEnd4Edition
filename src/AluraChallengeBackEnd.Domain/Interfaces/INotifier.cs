namespace AluraChallengeBackEnd.Domain.Interfaces;

public interface INotifier
{
    bool HasNotification();
    IList<Notification> GetNotifications();
    void Handle(Notification notification);       
}