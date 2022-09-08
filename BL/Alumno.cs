using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Alumno
    {
        public static ML.Result Add(ML.Alumno alumno)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.LEscogidoProgramacionNCapasAgostoContext context = new DL.LEscogidoProgramacionNCapasAgostoContext())
                {
                    var query = context.Database.ExecuteSqlRaw($"AlumnoAdd '{alumno.Nombre}', '{alumno.ApellidoPaterno}', '{alumno.ApellidoMaterno}', '{alumno.FechaNacimiento}', '{alumno.Sexo}', {alumno.Semestre.IdSemestre}");
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
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();
            try
            {

                using (DL.LEscogidoProgramacionNCapasAgostoContext context = new DL.LEscogidoProgramacionNCapasAgostoContext())

                {
                    
                    var query = context.Alumnos.FromSqlRaw($"AlumnoGetAll").ToList();

                    result.Objects = new List<object>();
                    if (query != null)
                    {
                        foreach (var obj in query)
                        {
                            ML.Alumno alumno = new ML.Alumno();
                            alumno.IdAlumno = obj.IdAlumno;
                            alumno.Nombre = obj.Nombre;
                            alumno.ApellidoPaterno = obj.ApellidoPaterno;
                            alumno.ApellidoMaterno = obj.ApellidoMaterno;
                            alumno.FechaNacimiento = obj.Nombre;
                            alumno.Sexo = obj.Nombre;

                            alumno.Semestre = new ML.Semestre();
                            alumno.Semestre.IdSemestre = obj.IdSemestre.Value;

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
        public static ML.Result GetById(int IdAlumno)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.LEscogidoMarzoContext context = new DL.LEscogidoMarzoContext())
                {
                    var obj = context.Materia.FromSqlRaw($"MateriaGetById {IdAlumno}").AsEnumerable().FirstOrDefault();

                    if (obj != null)
                    {
                        ML.Materia materia = new ML.Materia();
                        materia.IdMateria = obj.IdMateria;
                        materia.Nombre = obj.Nombre;
                        materia.Costo = obj.Costo;
                        materia.Creditos = obj.Creditos.Value;
                        materia.Imagen = obj.Imagen;
                        materia.Status = obj.Status;

                        materia.Semestre = new ML.Semestre();
                        materia.Semestre.IdSemestre = obj.IdSemestre.Value;

                        materia.Grupo = new ML.Grupo();
                        materia.Grupo.IdGrupo = obj.IdGrupo.Value;
                        materia.Grupo.Horario = obj.Horario;

                        materia.Grupo.Plantel = new ML.Plantel();
                        materia.Grupo.Plantel.IdPlantel = obj.IdPlantel;
                        materia.Grupo.Plantel.Nombre = obj.Plantel;

                        result.Object = materia;
                        result.Correct = true;

                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se pudo realizar la consulta";
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
    }
}
