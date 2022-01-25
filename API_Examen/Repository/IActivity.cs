
using API_Examen.DTO;
using Microsoft.AspNetCore.Mvc;

namespace API_Examen.Repository
{
    public interface IActivity
    {
        public IEnumerable<Object> GetActivities();
        public IEnumerable<Property> GetProperties();
        public void CancelActivities(int Id);
        public string RescheduleActivities(int Id, DateTime Fecha);
        public string AddActivities(ActivityDTO activity);
        public Activity GetActivity(int Id);

    }
}
