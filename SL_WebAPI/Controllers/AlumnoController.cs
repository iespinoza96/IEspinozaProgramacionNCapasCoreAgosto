using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SL_WebAPI.Controllers
{
    [Route("api/alumno/")]
    [ApiController]
    public class AlumnoController : ControllerBase
    {
        //// GET: api/<AlumnoController>
        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        [Route("GetAll")]
        [HttpGet]
        public IActionResult GetAll()
        {
            ML.Alumno alumno = new ML.Alumno();

            ML.Result result = BL.Alumno.GetAll(alumno);

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
            
        }

        [Route("GetAll")]
        [HttpPost]
        public IActionResult GetAll(ML.Alumno alumno)
        {
           // ML.Alumno alumno = new ML.Alumno();

            ML.Result result = BL.Alumno.GetAll(alumno);

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }

        }

        // GET api/<AlumnoController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<AlumnoController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<AlumnoController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<AlumnoController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
