using System;
using System.Collections.Generic;

namespace DL
{
    public partial class Direccion
    {
        public int IdDireccion { get; set; }
        public string? Calle { get; set; }
        public string? NumeroInterior { get; set; }
        public string? NumeroExterior { get; set; }
        public int? IdColonia { get; set; }
        public int? IdAlumno { get; set; }

        public virtual Alumno? IdAlumnoNavigation { get; set; }
    }
}
