﻿using Microsoft.AspNetCore.Mvc;

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
    }
}
