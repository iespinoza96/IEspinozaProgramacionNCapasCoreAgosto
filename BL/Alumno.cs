using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Data.OleDb;

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
                    var query = context.Database.ExecuteSqlRaw($"AlumnoAdd '{alumno.Nombre}', '{alumno.ApellidoPaterno}', '{alumno.ApellidoMaterno}', '{alumno.FechaNacimiento}', '{alumno.Sexo}', {alumno.Semestre.IdSemestre},'{alumno.Imagen}','{alumno.Direccion.Calle}','{alumno.Direccion.NumeroExterior}','{alumno.Direccion.NumeroInterior}',{alumno.Direccion.IdColonia}");
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
                            //alumno.Estatus = (bool)obj.Estatus;

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
                            //alumno.Estatus = (bool)obj.Estatus;

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

        public static ML.Result ConvertirExceltoDataTable(string connString)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (OleDbConnection context = new OleDbConnection(connString))
                {
                    string query = "SELECT * FROM [Sheet1$]";
                    using (OleDbCommand cmd = new OleDbCommand())
                    {
                        cmd.CommandText = query;
                        cmd.Connection = context;


                        OleDbDataAdapter da = new OleDbDataAdapter();
                        da.SelectCommand = cmd;

                        DataTable tablealumno = new DataTable();

                        da.Fill(tablealumno);

                        if (tablealumno.Rows.Count > 0)
                        {
                            result.Objects = new List<object>();

                            foreach (DataRow row in tablealumno.Rows)
                            {
                                ML.Alumno alumno = new ML.Alumno();

                                alumno.Nombre = row[0].ToString();
                                alumno.ApellidoPaterno = row[1].ToString();
                                alumno.ApellidoMaterno = row[2].ToString();
                                alumno.FechaNacimiento = row[3].ToString();
                                alumno.Sexo = row[4].ToString();

                                alumno.Semestre = new ML.Semestre();
                                alumno.Semestre.IdSemestre = int.Parse(row[5].ToString());
                                alumno.Imagen = row[6].ToString();
                                alumno.Estatus = bool.Parse(row[7].ToString());
                                

                                alumno.Direccion = new ML.Direccion();
                                alumno.Direccion.Calle = row[8].ToString();
                                alumno.Direccion.NumeroInterior = row[9].ToString();
                                alumno.Direccion.NumeroExterior = row[10].ToString();
                                alumno.Direccion.IdColonia = int.Parse(row[11].ToString());

                                result.Objects.Add(alumno);
                            }

                            result.Correct = true;

                        }

                        result.Object = tablealumno;

                        if (tablealumno.Rows.Count > 1)
                        {
                            result.Correct = true;
                        }
                        else
                        {
                            result.Correct = false;
                            result.ErrorMessage = "No existen registros en el excel";
                        }
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
        public static ML.Result ValidarExcel(List<object> Object)
        {
            ML.Result result = new ML.Result();

            try
            {
                result.Objects = new List<object>();
                //DataTable  //Rows //Columns
                int i = 1;
                foreach (ML.Alumno alumno in Object)
                {
                    ML.ErrorExcel error = new ML.ErrorExcel();
                    error.IdRegistro = i++;


                    alumno.Nombre = (alumno.Nombre == "") ? error.Mensaje += "Ingresar el nombre  " : alumno.Nombre;

                    if (alumno.ApellidoPaterno == "")
                    {
                        error.Mensaje += "Ingresar el Apellido Paterno ";
                    }
                    if (alumno.ApellidoMaterno == "")
                    {
                        error.Mensaje += "Ingresar el Apellido Materno ";
                    }
                    if (alumno.Semestre.IdSemestre.ToString() == "")
                    {
                        error.Mensaje += "Ingresar el semestre ";
                    }
                    //if (alumno.Grupo.Horario == "")
                    //{
                    //    error.Mensaje += "Ingresar el horario ";
                    //}
                    //if (alumno.Grupo.Plantel.IdPlantel.ToString() == "")
                    //{
                    //    error.Mensaje += "Ingresar el plantel ";
                    //}
                    if (alumno.Estatus.ToString() == "")
                    {
                        error.Mensaje += "Ingresar el status ";
                    }

                    if (error.Mensaje != null)
                    {
                        result.Objects.Add(error);
                    }


                }
                result.Correct = true;
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