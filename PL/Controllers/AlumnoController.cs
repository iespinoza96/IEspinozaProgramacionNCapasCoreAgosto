using Microsoft.AspNetCore.Mvc;
using System.Drawing.Drawing2D;
using System.Net.Http;

namespace PL.Controllers
{
    public class AlumnoController : Controller
    {
        private readonly IConfiguration _configuration;

        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _hostingEnvironment;

        public AlumnoController(IConfiguration configuration, Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment)
        {
            _configuration = configuration;
            _hostingEnvironment = hostingEnvironment;
        }
        [HttpGet]
        //public ActionResult GetAll()
        //{
        //    ML.Alumno alumno = new ML.Alumno();


        //    ML.Result result = BL.Alumno.GetAll(alumno);

        //    if (result.Correct)
        //    {
        //        alumno.Alumnos = result.Objects;

        //        return View(alumno);
        //    }
        //    else
        //    {
        //        return View(alumno);
        //    }
        //}
        public ActionResult GetAll()
        {
            ML.Alumno alumno = new ML.Alumno();
            //materia.Semestre = new ML.Semestre();

            //ML.Result resultSemestre = BL.Semestre.GetAll();

            //materia.Semestre.Semestres = resultSemestre.Objects;

            ML.Result resultMaterias = new ML.Result();
            resultMaterias.Objects = new List<Object>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_configuration["WebAPI"]);

                var responseTask = client.GetAsync("GetAll");
                responseTask.Wait();
                //Debe de entrar al servicio
                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<ML.Result>();
                    readTask.Wait();

                    foreach (var resultItem in readTask.Result.Objects)
                    {
                        ML.Alumno resultItemList = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Alumno>(resultItem.ToString());
                        resultMaterias.Objects.Add(resultItemList);
                    }
                }
                alumno.Alumnos = resultMaterias.Objects;
            }
            return View(alumno);

        }
        [HttpPost]
        public ActionResult GetAll(ML.Alumno alumno)
        {
            ML.Result result = BL.Alumno.GetAll(alumno);

            if (result.Correct)
            {
                alumno.Alumnos = result.Objects;

                return View(alumno);
            }
            else
            {
                return View(alumno);
            }
        }
        [HttpGet]
        public ActionResult Form(int? IdAlumno)
        {
            ML.Alumno alumno = new ML.Alumno();//instancia de objeto
            alumno.Semestre = new ML.Semestre();//instancia de FK

            ML.Result result = BL.Semestre.GetAllEF();

            if (IdAlumno == null)
            {
                alumno.Semestre.Semestres = result.Objects;
                return View(alumno);
            }
            else
            {
                return View();
            }
        }
        [HttpPost]
        public ActionResult Form(ML.Alumno alumno)
        {
            IFormFile image = Request.Form.Files["IFImage"];
            if (ModelState.IsValid)
            {
                
                //valido si traigo imagen
                if (image != null)
                {
                    //llamar al metodo que convierte a bytes la imagen
                    byte[] ImagenBytes = ConvertToBytes(image);
                    //convierto a base 64 la imagen y la guardo en mi objeto materia
                    alumno.Imagen = Convert.ToBase64String(ImagenBytes);
                }

                ML.Result result = new ML.Result();

                if (alumno.IdAlumno == 0)
                {
                    result = BL.Alumno.Add(alumno);

                    if (result.Correct)
                    {
                        ViewBag.Message = "Alumno agregado correctamente";
                    }
                    else
                    {
                        ViewBag.Message = "Ocurrio un error al agregar al alumno" + result.ErrorMessage;
                    }

                }
                else
                {
                    //result = BL.Alumno.Update(alumno);

                    //if (result.Correct)
                    //{
                    //    ViewBag.Message = "Alumno actualizado correctamente";
                    //}
                    //else
                    //{
                    //    ViewBag.Message = "Ocurrio un error al actualizar al alumno" + result.ErrorMessage;
                    //}

                }
                return View("Modal");

            }
            else
            {
                ML.Result resultSemestre = BL.Semestre.GetAllEF();

                alumno.Semestre = new ML.Semestre();
                alumno.Semestre.Semestres = resultSemestre.Objects;
                return View(alumno);
            }
            
        }
        public static byte[] ConvertToBytes(IFormFile imagen)
        {

            using var fileStream = imagen.OpenReadStream();

            byte[] bytes = new byte[fileStream.Length];
            fileStream.Read(bytes, 0, (int)fileStream.Length);

            return bytes;
        }
        [HttpGet]
        public ActionResult UpdateStatus(int IdAlumno)
        {
            ML.Result result = BL.Alumno.GetById(IdAlumno);

            if (result.Correct)
            {
                ML.Alumno alumno = new ML.Alumno();
                alumno = ((ML.Alumno)result.Object);

                alumno.Estatus = (alumno.Estatus) ? false : true;

                result = BL.Alumno.Update(alumno);

                if (result.Correct)
                {
                    ViewBag.Mensaje = "El estatus se actualizo correctamente";
                }
                else
                {
                    ViewBag.Mensaje = "Ocurrio un error al actualizar el Estatus" + result.ErrorMessage;
                }
            }
            else
            {
                ViewBag.Mensaje = "Ocurrio un erro al actualizar el Estatus" + result.ErrorMessage;
            }
            return PartialView("Modal");
        }
    }
}
