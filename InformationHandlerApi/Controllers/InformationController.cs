using InformationHandlerApi.Database;
using InformationHandlerApi.Database.Models;
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

        [HttpGet(Name = "GetInformation")]
        public object Get()
        {


            return new { Data = "sample" };
        }

        [HttpPost]
        public ActionResult<SampleObj> PostTodoItem(string text)
        {
            var sample = new SampleObj { Data = text };
            _databaseContext.SampleObjs.Add(sample);
            _databaseContext.SaveChanges();
            return sample;
        }
    }
}
