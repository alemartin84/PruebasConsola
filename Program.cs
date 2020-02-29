using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace PruebasConsola
{
    class Program
    {
        const string ARCHIVO_RECETAS = "RECETA.TXT"; //PROBANDO SI SE ACTUALIZA GITyyyW

        static Dictionary<string, int> CargaIngrediente()
        {
            string ingrediente;
            int cantidad;

            Dictionary<string, int> listaRecetas = new Dictionary<string, int>();

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
                    listaRecetas.Add(ingrediente, cantidad);
                }
                catch (ArgumentException)
                {
                    Console.WriteLine("El ingrediente = \"{0}\" ya tiene valor!.", ingrediente);
                }


            } while (ingrediente != "XXX");

            return listaRecetas;
        }
        

        static void Main(string[] args)
        {

            EntradaSalida io = new EntradaSalida();

            /*
            Receta recet = new Receta();
            recet.NombreReceta = "MARQUISSE";
            Console.WriteLine(recet.NombreReceta);
            */

            Console.WriteLine("CARGADOR DE RECETAS\n\n");
                       
            io.DiccToFile(CargaIngrediente(), ARCHIVO_RECETAS);
            Console.WriteLine("FIN DE CARGA DE RECETA, CHEQUEE RECETAS.TXT");
            io.ViewCvs(ARCHIVO_RECETAS);

            
            Console.Read();
        }
    }
}
