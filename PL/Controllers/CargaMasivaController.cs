using Microsoft.AspNetCore.Mvc;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace PL.Controllers
{
    public class CargaMasivaController : Controller
    {
        private readonly IConfiguration _configuration;

        private readonly IHostingEnvironment _hostingEnvironment;

        public CargaMasivaController(IConfiguration configuration, IHostingEnvironment hostingEnvironment)
        {
            _configuration = configuration;
            _hostingEnvironment = hostingEnvironment;
        }
        

        [HttpGet]
        public ActionResult CargaMasiva()
        {
            ML.Result result = new ML.Result();
            return View(result);
        }

        [HttpPost]
        public ActionResult CargaMasiva(ML.Alumno alumno)
        {
            IFormFile excelCargaMasiva = Request.Form.Files["FileExcel"];

            if (HttpContext.Session.GetString("PathArchivo") == null)
            {
                if (excelCargaMasiva.Length > 0)
                {
                    string fileName = Path.GetFileName(excelCargaMasiva.FileName);
                    string folderPath = _configuration["PathFolder:value"];
                    string extensionArchivo = Path.GetExtension(excelCargaMasiva.FileName).ToLower();
                    string extensionModulo = _configuration["TipoExcel"];

                    if (extensionArchivo == extensionModulo)
                    {

                        string filePath = Path.Combine(_hostingEnvironment.ContentRootPath, folderPath, Path.GetFileNameWithoutExtension(fileName)) + '-' + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";

                        if (!System.IO.File.Exists(filePath))
                        {
                            using (FileStream stream = new FileStream(filePath, FileMode.Create))
                            {
                                excelCargaMasiva.CopyTo(stream);
                            }

                            string connectionString = _configuration["ConnectionStringExcel:value"] + filePath;

                            ML.Result resultAlumnos = BL.Alumno.ConvertirExceltoDataTable(connectionString);

                            if (resultAlumnos.Correct)
                            {
                                ML.Result resultValidacion = BL.Alumno.ValidarExcel(resultAlumnos.Objects);
                                if (resultValidacion.Objects.Count == 0)
                                {
                                    resultValidacion.Correct = true;
                                    HttpContext.Session.SetString("PathArchivo", filePath);
                                }

                                return View(resultValidacion);
                            }
                            else
                            {

                            }
                        }
                    }
                }
            }
            else
            {

            }
           
            return View();
        }
    }
}
