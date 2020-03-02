using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace PruebasConsola
{
    class Program
    {
        //const string ARCHIVO_RECETAS = "RECETA.TXT"; //PROBANDO SI SE ACTUALIZA GITyyyW

        static Dictionary<string, int> CargaIngrediente()
        {
            string ingrediente;
            int cantidad;
            

            Dictionary<string, int> listaIngredientes = new Dictionary<string, int>();

            EntradaSalida io = new EntradaSalida();
                       

            do
            {

                ingrediente = io.ReadString("INGRESE INGREDIENTE: (XXX PARA SALIR): ");

                if (ingrediente == "XXX")
                {
                    break;
                }


                cantidad = io.ReadInt("INGRESE LA CANTIDAD EN GR.: ", 1, 10000);


                try
                {
                    listaIngredientes.Add(ingrediente, cantidad);
                }
                catch (ArgumentException)
                {
                    Console.WriteLine("El ingrediente = \"{0}\" ya tiene valor!.", ingrediente);
                }


            } while (ingrediente != "XXX");

            return listaIngredientes;
        }
        

        static void Main(string[] args)
        {
            
            string nombreReceta;


            EntradaSalida io = new EntradaSalida();
            Receta recet = new Receta();

            DirectoryInfo dir = new DirectoryInfo(Environment.CurrentDirectory);
            

            foreach (FileInfo file in dir.GetFiles("*.bin"))
            {
                Console.WriteLine(file.Name.Remove(file.Name.Length-4));
            }

             

            nombreReceta = io.ReadString("INGRESE EL NOMBRE DE LA RECETA: ");

            if (File.Exists(recet.NombreReceta+".bin"))
            {
                io.DeserializeFile(recet.NombreReceta + ".bin", ref recet);
            }
            else
            {
                recet.NombreReceta = nombreReceta;
                recet.ListaIngredientes = CargaIngrediente();
                io.SerializeToFile(recet);
            }


            /* TODO ESTO ES PARA GUARDAR Y MOSTRAR LA FORMA DE SVC TRADICIONAL, PRUEBO LA SERIALIZACION
            archivoRecetas = recet.NombreReceta + ".TXT";
                        
            io.DiccToFile(recet.ListaIngredientes, archivoRecetas);

            Console.WriteLine("FIN DE CARGA DE RECETA, MOSTRANDO RECETAS.TXT\n\n");

            Console.WriteLine("NOMBRE DE LA RECETA: {0 }\n", recet.NombreReceta);

            io.ReadRecipeFromFile(archivoRecetas);
            */

            foreach (KeyValuePair<string, int> kvp in recet.ListaIngredientes)
            {
                Console.WriteLine("INGREDIENTE:{0}     CANTIDAD:{1}", kvp.Key, kvp.Value);
            }


            Console.Read();
        }
    }
}
