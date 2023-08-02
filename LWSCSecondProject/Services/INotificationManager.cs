namespace LWSCSecondProject.Services
{
    public interface INotificationManager
    {
        Task Notify(string msg);
    }
}