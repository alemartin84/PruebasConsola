using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PruebasConsola
{
    public class Receta
    {

        private string nombreReceta;
        public Receta()
        {
            Console.WriteLine("se instanció un objeto del tipo RECETA");
        }

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
