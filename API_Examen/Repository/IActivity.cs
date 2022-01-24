
namespace API_Examen.Repository
{
    public interface IActivity
    {
        public IEnumerable<Activity> GetActivities();
        public void CancelActivities(int Id);
        public void RescheduleActivities(int Id);
        public void AddActivities(Activity activity);
        public Activity GetActivity(int Id);

    }
}
