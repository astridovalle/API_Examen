using API_Examen.DTO;
using API_Examen.Repository;
using Microsoft.AspNetCore.Authorization;
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

        [Authorize]
        [HttpGet("ObtenerActividades")]
        public ActionResult<IEnumerable<Object>> GetActivities()
        {
            return _ActivityRepo.GetActivities().ToList();
        }

        [Authorize]
        [HttpGet("ObtenerPropiedades")]
        public ActionResult<IEnumerable<Property>> GetProperties()
        {
            return _ActivityRepo.GetProperties().ToList();
        }

        [Authorize]
        [HttpPost("AgregarActividades")]
        public ActionResult<string> AddActivities([FromBody] ActivityDTO activity)
        {
            if (!ModelState.IsValid)
                return "Modelo Invalido";

            return _ActivityRepo.AddActivities(activity);
        }

        [Authorize]
        [HttpPut("CancelarActividad/{Id}")]
        public ActionResult CancelActivities(int Id)
        {
            _ActivityRepo.CancelActivities(Id);
            return Ok("Actividad Cancelada.");
        }

        [Authorize]
        [HttpPut("ReagendarActividad/{Id}")]
        public ActionResult<string> ReagendarActividad(int Id, DateTime Fecha) {
            return _ActivityRepo.RescheduleActivities(Id, Fecha);
        }

    }
}
