using InformationHandlerApi.Database;
using Microsoft.AspNetCore.Mvc;

namespace InformationHandlerApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InformationController : Controller
    {
        private readonly DatabaseContext _databaseContext;

        public InformationController(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        //public IActionResult Index()
        //{
        //    return View();
        //}

        [HttpGet(Name = "GetInformation")]
        public object Get()
        {
            

            return new { Data = "sample" };
        }
    }
}
