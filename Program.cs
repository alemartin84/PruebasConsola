using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Data.SQLite;

namespace PruebasConsola
{
    
    class Program
    {
        
        static void Main(string[] args)
        {
            
            string nombreReceta;

            Receta recet = new Receta();
            

            do
            {
                nombreReceta = EntradaSalida.ReadString("INGRESE EL NOMBRE DE LA RECETA: ");
                if (EntradaSalida.CheckIfRecordExists(nombreReceta))
                {
                    EntradaSalida.DeserealizeFromSqlite(nombreReceta, ref recet); //SI EXISTE PONE EL RESULTADO DEL BLOB DE SQLITE EN EL OBJETO recet
                }
                else
                {
                    recet.NombreReceta = nombreReceta; //SI NO EXISTE CARGO LOS DATOS DEL OBJETO DE LA CLASE RECETA
                    recet.CargarIngredientes();
                    recet.CargaDescripcion();
                    recet.CargaHorneado();
                    if (recet.ListaIngredientes.Count > 0) EntradaSalida.SerializeToSqlite(recet); //POR AHORA LA RECETA TIENE QUE TENER INGREDIENTES
                }
                recet.MostrarCompleta();
                //Console.WriteLine("\n\nDESEA INGRESAR OTRA RECETA? Y/N:");

            } while (EntradaSalida.ReadString("\n\nDESEA INGRESAR OTRA RECETA? Y/N:").ToUpper() != "N");

            
            /*
            string alePrueba = "█▀▄ █▀▀ ▄▀▀ █▀▀ ▀█▀ ▄▀▄ ▄▀▀\n" +
                               "█▀▄ █▀▀ █░░ █▀▀ ░█░ █▄█ ░▀▄\n" +
                               "▀░▀ ▀▀▀ ░▀▀ ▀▀▀ ░▀░ ▀░▀ ▀▀░";
            */



           // Console.Write(alePrueba);

            
                                 


            Console.Read(); //dummy de entrada para que no se vaya la pantalla
        }
    }
}
