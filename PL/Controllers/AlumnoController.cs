using Microsoft.AspNetCore.Mvc;
using System.Drawing.Drawing2D;

namespace PL.Controllers
{
    public class AlumnoController : Controller
    {
        [HttpGet]
        public ActionResult GetAll()
        {
            ML.Result result = BL.Alumno.GetAll();
            ML.Alumno alumno = new ML.Alumno();
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

        public static byte[] ConvertToBytes(IFormFile imagen)
        {

            using var fileStream = imagen.OpenReadStream();

            byte[] bytes = new byte[fileStream.Length];
            fileStream.Read(bytes, 0, (int)fileStream.Length);

            return bytes;
        }
    }
}
