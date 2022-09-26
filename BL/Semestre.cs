using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Semestre
    {
        public static ML.Result GetAllEF()
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.LEscogidoProgramacionNCapasAgostoContext context = new DL.LEscogidoProgramacionNCapasAgostoContext())

                {

                    var query = context.Semestres.FromSqlRaw($"SemestreGetAll").ToList();
                    // var query = context.SemestreGetAll().ToList();

                    result.Objects = new List<object>();

                    if (query != null)
                    {
                        foreach (var obj in query)
                        {
                            ML.Semestre semestre = new ML.Semestre();
                            semestre.IdSemestre = obj.IdSemestre;
                            semestre.Nombre = obj.Nombre;

                            result.Objects.Add(semestre);
                        }
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se encontraron registros";
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
