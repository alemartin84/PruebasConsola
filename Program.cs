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
        static string ReadString(string prompt) 
        { 
            string result;
            do 
            { 
                Console.Write(prompt); 
                result = Console.ReadLine(); 
            }
            while (result == ""); 

            return result; 
        }

        static int ReadInt(string prompt, int low, int high)
        {
            int result;

            do 
            { 
                string intString = ReadString(prompt);

                try
                {
                    result = int.Parse(intString);
                }
                catch (Exception e)//(FormatException)
                {
                    Console.WriteLine(e.Message);
                    result = 0;
                }
                
            } 
            while ((result < low) || (result > high));

            return result;
        }

        static void DiccionarioAArchivo(Dictionary<string, int> diccionario, String archivo)
        {
            StreamWriter writer;
            writer = new StreamWriter(archivo);
           

            foreach (KeyValuePair<string, int> kvp in diccionario)
            {
                writer.WriteLine("{0},{1},", kvp.Key, kvp.Value);
            }

            writer.Close();
        }

        static void VerArchivoConComas(string archivo)
        {
            StreamReader fileReader = new StreamReader(archivo);
                       
            while (fileReader.EndOfStream == false)
            {
                string line = fileReader.ReadLine();
                string[] Columns = line.Split(',');
                Console.WriteLine("Ingrediente leido: {0,10}             Cantidad leida: {1,15}", Columns[0], int.Parse(Columns[1]));
            }
            fileReader.Close();

        }

        static void Main(string[] args)
        {

            string ingrediente;
            int cantidad;
            Dictionary<string, int> listaRecetas = new Dictionary<string, int>();

            Receta recet = new Receta();

            recet.NombreReceta = "MARQUISSE";
            Console.WriteLine(recet.NombreReceta);


            Console.WriteLine("CARGADOR DE RECETAS\n\n");

            do
            {

                ingrediente = ReadString("INGRESE INGREDIENTE: (XXX PARA SALIR): ");

                if (ingrediente == "XXX")
                {
                    break;
                }


                cantidad = ReadInt("INGRESE LA CANTIDAD EN GR.: ",1,10000);

                
                    try
                    {
                        listaRecetas.Add(ingrediente, cantidad);
                    }
                    catch (ArgumentException)
                    {
                        Console.WriteLine("El ingrediente = \"{0}\" ya tiene valor!.", ingrediente);
                    }
                

            } while (ingrediente != "XXX");

            Console.WriteLine("FIN DE CARGA DE RECETA, CHEQUEE RECETAS.TXT");

            DiccionarioAArchivo(listaRecetas, ARCHIVO_RECETAS);
            VerArchivoConComas(ARCHIVO_RECETAS);

            
            Console.ReadLine();
        }
    }
}
