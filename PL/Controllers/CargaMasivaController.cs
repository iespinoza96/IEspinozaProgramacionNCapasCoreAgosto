using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class CargaMasivaController : Controller
    {
        //private readonly IConfiguration _configuration;

        //private readonly IHostingEnvironment _hostingEnvironment;

        //public CargaMasiva(IConfiguration configuration, IHostingEnvironment hostingEnvironment)
        //{
        //    _configuration = configuration;
        //    _hostingEnvironment = hostingEnvironment;
        //}
        private readonly IConfiguration _configuration;


        public CargaMasivaController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public ActionResult CargaMasiva()
        {
            return View();
        }
    }
}
