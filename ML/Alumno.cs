namespace ML
{
    public class Alumno
    {
        public int IdAlumno { get; set; }
        public string Nombre { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string FechaNacimiento { get; set; }
        public string Sexo { get; set; }
        public string Imagen { get; set; }
        public List<object> Alumnos { get; set; }
        public ML.Semestre Semestre { get; set; } // propiedad de navegación

        public ML.Direccion Direccion { get; set; }
    }
}