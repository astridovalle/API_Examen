
using API_Examen.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API_Examen.Repository
{
    public class ActivityRepo : IActivity
    {
        public ActivitiesDbContext context;
        public ActivityRepo(ActivitiesDbContext context)
        {
            this.context = context;
        }
        public string AddActivities(ActivityDTO activity)
        {
            var property = context.Properties.Where(x => x.Id == activity.PropertyId).FirstOrDefault();
            var propertyActivities = context.Activities.Where(x => x.PropertyId == activity.PropertyId).Select(s => s.Schedule).ToList();
            var LastId = context.Activities.Select(x => x.Id).ToList().Last();

            if (property.Status == "Desactivada") {
                return "Propiedad desactivada";
            }

            foreach (var item in propertyActivities)
            {
                var Schedule= activity.Schedule;

                if (Schedule >= item && Schedule <= item.AddHours(1)) { 
                    return "No se pueden crear actividades, elija despues de las " +  item.AddHours(1);
                }
            }

            Activity act = new Activity()
            {
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                Id = LastId + 1,
                Schedule = activity.Schedule,
                PropertyId = activity.PropertyId,
                Status = activity.Status,
                Title = activity.Title
            };
          
            context.Activities.Add(act);
            context.SaveChanges();
            return "Activiada creada";
        }

        public void CancelActivities(int Id)
        {
            var act = context.Activities.Where(x => x.Id == Id).FirstOrDefault();

            act.Status = "Disabled";
            act.UpdatedAt = DateTime.Now;

            context.Entry(act).State = EntityState.Modified;
            context.SaveChanges();
        }

        public IEnumerable<Object> GetActivities()
        {
            var result = context.Activities.Where(w => DateTime.Now.AddDays(-3) <= w.Schedule && w.Schedule <= DateTime.Now.AddDays(14)).Select(x => new ActivityListDTO
            {
                Id = x.Id,
                Schedule = x.Schedule.ToString("dd/MMM/yyyy hh:mm tt"),
                Title = x.Title,
                CreatedAt = x.CreatedAt.ToString("dd/MMM/yyyy hh:mm tt"),
                Status = x.Status,
                Condition = x.Status == "Active" && x.Schedule >= DateTime.Now ? "Pendiente a realizar" : (x.Status == "Active" && x.Schedule < DateTime.Now ? "Atrasada" : (x.Status == "Disabled" ? "Finalizada" : "Invalido")),
                Property = new PropertyDTO
                {
                    Id = x.Property.Id,
                    Title = x.Property.Title,
                    Address = x.Property.Address
                },
                Survey = x.Surveys.Select(y => y.Id).ToList(),
            }).ToList();

            return result;
        }

        public Activity GetActivity(int Id)
        {
            return context.Activities.Where(x => x.Id == Id).FirstOrDefault();
        }

        public IEnumerable<Property> GetProperties()
        {
            return context.Properties.ToList();
        }

        public string RescheduleActivities(int Id, DateTime Fecha)
        {
            var act = context.Activities.Where(x => x.Id == Id).FirstOrDefault();

            if (act.Status == "Disabled") {
                return "Actividad desactivada, no se puede reagendar.";
            }
            act.Schedule = Fecha;
            act.UpdatedAt = DateTime.Now;

            context.Entry(act).State = EntityState.Modified;
            context.SaveChanges();

            return "Actividad reagendada";
        }
    }
}
