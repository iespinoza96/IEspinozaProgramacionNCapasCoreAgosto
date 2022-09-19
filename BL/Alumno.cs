using Microsoft.EntityFrameworkCore;

namespace BL
{
    public class Alumno
    {
        public static ML.Result Add(ML.Alumno alumno)
        {
            ML.Result result = new ML.Result();

            try
            {
                alumno.Estatus = true;
                using (DL.LEscogidoProgramacionNCapasAgostoContext context = new DL.LEscogidoProgramacionNCapasAgostoContext())
                {
                    var query = context.Database.ExecuteSqlRaw($"AlumnoAdd '{alumno.Nombre}', '{alumno.ApellidoPaterno}', '{alumno.ApellidoMaterno}', '{alumno.FechaNacimiento}', '{alumno.Sexo}', {alumno.Semestre.IdSemestre},'{alumno.Imagen}','{alumno.Direccion.Calle}','{alumno.Direccion.NumeroExterior}','{alumno.Direccion.NumeroInterior}',{alumno.Direccion.IdColonia}, {alumno.Estatus}");
                    //ExecuteSqlRaw -- add,delete,update
                    //FromSqlRaw -- consultas getall, getbyid

                    if (query > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
            return result;
        }
        public static ML.Result GetAll(ML.Alumno alumno)
        {
            ML.Result result = new ML.Result();
            try
            {

                using (DL.LEscogidoProgramacionNCapasAgostoContext context = new DL.LEscogidoProgramacionNCapasAgostoContext())

                {

                    var query = context.Alumnos.FromSqlRaw($"AlumnoGetAll '{alumno.Nombre}','{alumno.ApellidoPaterno}','{alumno.ApellidoMaterno}'").ToList();

                    result.Objects = new List<object>();
                    if (query != null)
                    {
                        foreach (var obj in query)
                        {
                            alumno = new ML.Alumno();

                            alumno.IdAlumno = obj.IdAlumno;
                            alumno.Nombre = obj.Nombre;
                            alumno.ApellidoPaterno = obj.ApellidoPaterno;
                            alumno.ApellidoMaterno = obj.ApellidoMaterno;
                            alumno.FechaNacimiento = obj.FechaNacimiento.Value.ToString("dd-MM-yyyy");
                            alumno.Sexo = obj.Sexo;
                            alumno.Imagen = obj.Imagen;
                            alumno.Estatus = obj.Estatus;

                            alumno.Semestre = new ML.Semestre();
                            alumno.Semestre.IdSemestre = obj.IdSemestre.Value;
                            alumno.Semestre.Nombre = obj.NombreSemestre.ToString();


                            result.Objects.Add(alumno);
                        }
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se ha podido realizar la consulta";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }

            return result;
        }
        public static ML.Result Update(ML.Alumno alumno)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.LEscogidoProgramacionNCapasAgostoContext context = new DL.LEscogidoProgramacionNCapasAgostoContext())
                {
                    var query = context.Database.ExecuteSqlRaw($"AlumnoUpdate '{alumno.Nombre}', '{alumno.ApellidoPaterno}', '{alumno.ApellidoMaterno}', '{alumno.FechaNacimiento}', '{alumno.Sexo}', {alumno.Semestre.IdSemestre},'{alumno.Imagen}','{alumno.Direccion.Calle}','{alumno.Direccion.NumeroExterior}','{alumno.Direccion.NumeroInterior}',{alumno.Direccion.IdColonia}, {alumno.Estatus}");

                    if (query >= 1)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }

            return result;
        }
        public static ML.Result GetById(int IdAlumno)
        {
            ML.Result result = new ML.Result();
            try
            {

                using (DL.LEscogidoProgramacionNCapasAgostoContext context = new DL.LEscogidoProgramacionNCapasAgostoContext())

                {

                    var obj = context.Alumnos.FromSqlRaw($"AlumnoGetById {IdAlumno}").AsEnumerable().FirstOrDefault();

                    if (obj != null)
                    {
                           ML.Alumno alumno = new ML.Alumno();

                            alumno.IdAlumno = obj.IdAlumno;
                            alumno.Nombre = obj.Nombre;
                            alumno.ApellidoPaterno = obj.ApellidoPaterno;
                            alumno.ApellidoMaterno = obj.ApellidoMaterno;
                            alumno.FechaNacimiento = obj.FechaNacimiento.Value.ToString("dd-MM-yyyy");
                            alumno.Sexo = obj.Sexo;
                            alumno.Imagen = obj.Imagen;
                            alumno.Estatus = obj.Estatus;

                            alumno.Semestre = new ML.Semestre();
                            alumno.Semestre.IdSemestre = obj.IdSemestre.Value;
                            alumno.Semestre.Nombre = obj.NombreSemestre.ToString();



                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se ha podido realizar la consulta";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }

            return result;
        }
    }
}