using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace PruebasConsola
{
    class EntradaSalida
    {
        public string ReadString(string prompt)
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

        public int ReadInt(string prompt, int low, int high)
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

        public void DiccToFile(Dictionary<string, int> diccionario, String archivo)
        {
            StreamWriter writer;
            writer = new StreamWriter(archivo);


            foreach (KeyValuePair<string, int> kvp in diccionario)
            {
                writer.WriteLine("{0},{1},", kvp.Key, kvp.Value);
            }

            writer.Close();
        }

        public void ViewCvs(string archivo)
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
    }
}
