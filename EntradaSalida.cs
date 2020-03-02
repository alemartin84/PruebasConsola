using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

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
            writer = new StreamWriter(archivo, append: true);

            writer.WriteLine("{0}", archivo.Remove(archivo.Length-4));
            

            foreach (KeyValuePair<string, int> kvp in diccionario)
            {
                writer.WriteLine("{0},{1},", kvp.Key, kvp.Value);
            }

            writer.Close();
        }

        public void LineToFile(string linea, String archivo)
        {
            StreamWriter writer;
            writer = new StreamWriter(archivo);


            writer.WriteLine("{0}", linea);
           

            writer.Close();
        }

        public void ReadRecipeFromFile(string archivo)
        {
            StreamReader fileReader = new StreamReader(archivo);

            string line;
            line = fileReader.ReadLine(); //para leer el nombre de la receta, despues adaptar

            while (fileReader.EndOfStream == false)
            {
                //string 
                    line = fileReader.ReadLine();
                string[] Columns = line.Split(',');
                Console.WriteLine("Ingrediente leido: {0,10}             Cantidad leida: {1,15}", Columns[0], int.Parse(Columns[1]));
            }
            fileReader.Close();

        }

        public void SerializeToFile(Receta recet)
        {
            FileStream stream = new FileStream((recet.NombreReceta+".bin"), FileMode.Create, FileAccess.Write);
            BinaryFormatter b = new BinaryFormatter();
            b.Serialize(stream, recet);
            stream.Close();
        }

        public void DeserializeFile(String nombrearchivo, ref Receta recet)
        {
            if (File.Exists(nombrearchivo))
            {
                Console.WriteLine("Exite! se muestra la guardada");
                Stream openFileStream = File.OpenRead(nombrearchivo);
                BinaryFormatter deserializer = new BinaryFormatter();
                recet = (Receta)deserializer.Deserialize(openFileStream);
                openFileStream.Close();
            }
        }

        public void ShowFilesinConsole(string extension)
        {
            DirectoryInfo dir = new DirectoryInfo(Environment.CurrentDirectory);
            foreach (FileInfo file in dir.GetFiles(extension))
            {
                Console.WriteLine(file.Name.Remove(file.Name.Length - 4));
            }

        }
    }
}
