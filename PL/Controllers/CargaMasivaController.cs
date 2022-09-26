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
                                ViewBag.Message = "Ocurrio un error al leer el arhivo";
                            }
                        }
                        else
                        {
                            ViewBag.Message = "Ya existe un archivo con ese nombre";
                        }
                    }
                    else
                    {
                        ViewBag.Message = "El arhivo debe de ser .xlsx";
                    }

                }
                else
                {
                    ViewBag.Message = "No se encuentra un archivo que procesar ";
                }
            }
            else
            {
                string rutaArchivoExcel = HttpContext.Session.GetString("PathArchivo");
                string connectionString = _configuration["ConnectionStringExcel:value"] + rutaArchivoExcel;

                ML.Result resultData = BL.Alumno.ConvertirExceltoDataTable(connectionString);
                if (resultData.Correct)
                {
                    ML.Result resultErrores = new ML.Result();
                    resultErrores.Objects = new List<object>();

                    foreach (ML.Alumno alumnoItem in resultData.Objects)
                    {

                        ML.Result resultAdd = BL.Alumno.Add(alumnoItem);
                        if (!resultAdd.Correct)
                        {
                            resultErrores.Objects.Add("No se insertó el Alumno con nombre: " + alumnoItem.Nombre + " Error: " + resultAdd.ErrorMessage);
                        }
                    }
                    if (resultErrores.Objects.Count > 0)
                    {

                        string fileError = Path.Combine(_hostingEnvironment.WebRootPath, @"~\Files\logErrores.txt");
                        using (StreamWriter writer = new StreamWriter(fileError))
                        {
                            foreach (string ln in resultErrores.Objects)
                            {
                                writer.WriteLine(ln);
                            }
                        }
                        ViewBag.Message = "Las Materias No han sido registrados correctamente";
                    }
                    else
                    {
                        ViewBag.Message = "Las Materias han sido registrados correctamente";

                    }

                }

            }
            return PartialView("Modal");

        }
    }
}
