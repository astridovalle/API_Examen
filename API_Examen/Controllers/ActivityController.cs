using API_Examen.DTO;
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

        [HttpGet("ObtenerActividades")]
        public ActionResult<IEnumerable<Object>> GetActivities()
        {
            return _ActivityRepo.GetActivities().ToList();
        }

        [HttpPost("AgregarActividades")]
        public ActionResult<string> AddActivities([FromBody] ActivityDTO activity)
        {
            return _ActivityRepo.AddActivities(activity);
        }

        [HttpPut("CancelarActividad/{Id}")]
        public ActionResult CancelActivities(int Id)
        {
            _ActivityRepo.CancelActivities(Id);
            return Ok("Actividad Cancelada.");
        }

        [HttpPut("ReagendarActividad/{Id}")]
        public ActionResult<string> ReagendarActividad(int Id, DateTime Fecha) {
            return _ActivityRepo.RescheduleActivities(Id, Fecha);
        }

    }
}
