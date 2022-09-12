using System;
using System.Collections.Generic;

namespace DL
{
    public partial class Alumno
    {
        public Alumno()
        {
            Direccions = new HashSet<Direccion>();
        }

        public int IdAlumno { get; set; }
        public string? Nombre { get; set; }
        public string? ApellidoPaterno { get; set; }
        public string? ApellidoMaterno { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public string? Sexo { get; set; }
        public byte? IdSemestre { get; set; }
        public string? Imagen { get; set; }

        public virtual Semestre? IdSemestreNavigation { get; set; }
        public virtual ICollection<Direccion> Direccions { get; set; }

        public string NombreSemestre { get; set; }
    }
}
