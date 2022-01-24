using Microsoft.AspNetCore.Mvc;

namespace API_Examen.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActivityController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
