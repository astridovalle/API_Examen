using API_Examen.Repository;
using Microsoft.AspNetCore.Mvc;

namespace API_Examen.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActivityController : ControllerBase
    {
        public IActivity _ActivityRepo;
        public ActivityController(IActivity activity)
        {
            _ActivityRepo = activity;   
        }

        [HttpGet("/GetActivities")]
        public ActionResult<IEnumerable<Object>> GetActivities()
        {
            return _ActivityRepo.GetActivities().ToList();
        }

    }
}
