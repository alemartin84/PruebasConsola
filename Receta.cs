using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;

namespace PruebasConsola
{
    public interface IReceta 
    {
       
    }
    
    [Serializable()]
    public class Receta// : IReceta
    {

        
        public Receta()
        {
           //  Console.WriteLine("se instanció un objeto del tipo RECETA");
           // CONSTRUCTOR 
        }

        public Dictionary<string, int> ListaIngredientes { get; set; }

       
        public string NombreReceta { get; set; }

        public string Descripcion { get; set; }

        public string Horneado { get; set; }

        public void CargaDescripcion()
        {
            //EntradaSalida io = new EntradaSalida();
            Descripcion= EntradaSalida.ReadString("INGRESE EL PROCEDIMIENTO O ANOTACIONES:");
        }

        public void CargaHorneado()
        {
            //EntradaSalida io = new EntradaSalida();
            Horneado = EntradaSalida.ReadString("INGRESE TEMP. Y TIEMPO DE COCCION: XXXº XX'");
        }


        public void MostrarIngredientes()
        {
            foreach (KeyValuePair<string, int> kvp in ListaIngredientes)
            {
                Console.WriteLine("INGREDIENTE:{0,-20} CANTIDAD:{1}", kvp.Key, kvp.Value);
            }

        }

        public void MostrarCompleta()
        {
            Console.WriteLine("\n\n LEYENDO DE ARCHIVO... \n\n");
            Console.WriteLine("RECETA: " + NombreReceta);
            MostrarIngredientes();
            Console.WriteLine("DESCRIPCION: " + Descripcion);
            Console.WriteLine("HORNEADO: " + Horneado);

        }


        public void CargarIngredientes()
        {
            string ingrediente;
            int cantidad;

            Dictionary<string, int> listaIng = new Dictionary<string, int>();
            //EntradaSalida io = new EntradaSalida();


            do
            {

                ingrediente = EntradaSalida.ReadString("INGRESE INGREDIENTE: (XXX PARA SALIR): ").ToUpper();

                if (ingrediente == "XXX")
                {
                    break;
                }


                cantidad = EntradaSalida.ReadInt("INGRESE LA CANTIDAD EN GR.: ", 1, 10000);


                try
                {

                    listaIng.Add(ingrediente, cantidad);
                }
                catch (ArgumentException)
                {
                    Console.WriteLine("El ingrediente = \"{0}\" ya tiene valor!.", ingrediente);
                }


            } while (ingrediente != "XXX");

            // return listaIngredientes;
            ListaIngredientes = listaIng;
        }

    }
}
