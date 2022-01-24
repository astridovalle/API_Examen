
namespace API_Examen.Repository
{
    public class ActivityRepo : IActivity
    {
        public ActivitiesDbContext context;
        public ActivityRepo(ActivitiesDbContext context)
        {
            this.context = context;
        }
        public void AddActivities(Activity activity)
        {
            context.Activities.Add(activity);
        }

        public void CancelActivities(int Id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Object> GetActivities()
        {
            var result = context.Activities.Where( w => DateTime.Now.AddDays(-3) <= w.Schedule && w.Schedule <= DateTime.Now.AddDays(14)).Select(x => new
            {
                x.Id,
                x.Schedule,
                x.Title,
                x.CreatedAt,
                x.Status,
                condition = "",
                Property = new
                {
                    x.Property.Id,
                    x.Property.Title,
                    x.Property.Address
                },
                Survey = x.Surveys.Select(y => y.Id).ToList(),
            }).ToList();

            return result;
        }

        public Activity GetActivity(int Id)
        {
            return context.Activities.Where(x => x.Id == Id).FirstOrDefault();
        }

        public void RescheduleActivities(int Id)
        {
            throw new NotImplementedException();
        }
    }
}
