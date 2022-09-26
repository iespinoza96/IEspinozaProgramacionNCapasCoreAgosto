// See https://aka.ms/new-console-template for more information
using System.Globalization;

Console.WriteLine("Hello, World!");
//CargaMasiva();
//ReadFile();


//static void ReadFile()
//{
//    string file = @"C:\Users\digis\source\repos\IEspinozaProgramacionNCapasCoreAgosto\LayoutAlumno2.txt";
//    if (File.Exists(file))
//    {
//        StreamReader Textfile = new StreamReader(file);
//        string line;
//        line = Textfile.ReadLine();
//        while ((line = Textfile.ReadLine()) != null)
//        {
//            string[] lines = line.Split('|');

//            ML.Alumno alumno = new ML.Alumno();

//            alumno.Nombre = lines[0];
//            alumno.ApellidoPaterno = lines[1];
//            alumno.ApellidoMaterno = lines[2];
//            alumno.FechaNacimiento = lines[3];
//            alumno.Sexo = lines[4];
              
//            alumno.Semestre = new ML.Semestre();
//            alumno.Semestre.IdSemestre = byte.Parse(lines[5]);
//            alumno.Imagen = null;

//            alumno.Direccion = new ML.Direccion();
//            alumno.Direccion.Calle = lines[6];
//            alumno.Direccion.NumeroExterior = lines[7];
//            alumno.Direccion.NumeroInterior = lines[8];
//            alumno.Direccion.IdColonia = int.Parse(lines[9]);


//            ML.Result result = BL.Alumno.Add(alumno);

             
            //if (result.Correct)
            //{
            //    Console.WriteLine("Correcto");
            //    Console.ReadKey();
            //}
            //else
            //{

//            }
//        }
//    }
//}