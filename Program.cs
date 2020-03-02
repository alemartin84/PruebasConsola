﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace PruebasConsola
{
    class Program
    {
       
        static void Main(string[] args)
        {
            
            string nombreReceta;


            EntradaSalida io = new EntradaSalida();
            Receta recet = new Receta();

            
            io.ShowFilesinConsole("*.bin");
                 
             

            nombreReceta = io.ReadString("INGRESE EL NOMBRE DE LA RECETA: ");

            if (File.Exists(nombreReceta + ".bin"))
            {
                io.DeserializeFile(nombreReceta + ".bin", ref recet);
            }
            else
            {
                recet.NombreReceta = nombreReceta;
                recet.CargarIngredientes();
                recet.CargaDescripcion();
                recet.CargaHorneado();
                if (recet.ListaIngredientes.Count>0) io.SerializeToFile(recet);
            }


            /* TODO ESTO ES PARA GUARDAR Y MOSTRAR LA FORMA DE SVC TRADICIONAL, PRUEBO LA SERIALIZACION
            archivoRecetas = recet.NombreReceta + ".TXT";
                        
            io.DiccToFile(recet.ListaIngredientes, archivoRecetas);

            Console.WriteLine("FIN DE CARGA DE RECETA, MOSTRANDO RECETAS.TXT\n\n");

            Console.WriteLine("NOMBRE DE LA RECETA: {0 }\n", recet.NombreReceta);

            io.ReadRecipeFromFile(archivoRecetas);
            */

            //mostrar ingredientes del objeto de la clase receta
            Console.WriteLine("\n\n LEYENDO DE ARCHIVO... \n\n");
            Console.WriteLine("RECETA: " + recet.NombreReceta);
            recet.MostrarIngredientes();
            Console.WriteLine("DESCRIPCION: " + recet.Descripcion);
            Console.WriteLine("HORNEADO: " + recet.Horneado);
            

            Console.Read();
        }
    }
}
