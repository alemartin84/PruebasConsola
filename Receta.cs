using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PruebasConsola
{
    public class Receta
    {

        private string nombreReceta;
        //private Dictionary<string, int> listaIngredientes = new Dictionary<string, int>();

        public Receta()
        {
            Console.WriteLine("se instanció un objeto del tipo RECETA");
        }

        public Dictionary<string, int> ListaIngredientes { get; set; }
        public string NombreReceta
        {
            set
            {
                nombreReceta = value;
            }

            get
            {
                return nombreReceta;
            }
        }
    }
}
