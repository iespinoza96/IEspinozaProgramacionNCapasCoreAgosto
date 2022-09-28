using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ML
{
    public class Alumno
    {

        public int IdAlumno { get; set; }

        [Required]
        [DisplayName("Nombre:")]
        //[RegularExpression(@"[A-Za-z]")]
        public string Nombre { get; set; }

        [Required]
        [DisplayName("Apellido Paterno:")]
        public string ApellidoPaterno { get; set; }

        [Required]
        [DisplayName("Apellido Materno:")]
        public string ApellidoMaterno { get; set; }

        [Required]
        [DisplayName("Fecha de nacimiento:")]
        public string FechaNacimiento { get; set; }

        public string Sexo { get; set; }
        public string Imagen { get; set; }
        public bool Estatus { get; set; }
        public List<object> Alumnos { get; set; }
        public ML.Semestre Semestre { get; set; } // propiedad de navegación

        public ML.Direccion Direccion { get; set; }
    }
}