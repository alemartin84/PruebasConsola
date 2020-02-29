using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

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
            string archivoRecetas;

            EntradaSalida io = new EntradaSalida();

            
            Receta recet = new Receta();

            recet.NombreReceta = io.ReadString("INGRESE EL NOMBRE DE LA RECETA: ");
            recet.ListaIngredientes = CargaIngrediente();

            archivoRecetas = recet.NombreReceta + ".TXT";

            //io.LineToFile(recet.NombreReceta, archivoRecetas);
            io.DiccToFile(recet.ListaIngredientes, archivoRecetas);

            Console.WriteLine("FIN DE CARGA DE RECETA, MOSTRANDO RECETAS.TXT\n\n");

            Console.WriteLine("NOMBRE DE LA RECETA: {0 }\n", recet.NombreReceta);

            io.ReadRecipeFromFile(archivoRecetas);

            
            Console.Read();
        }
    }
}
