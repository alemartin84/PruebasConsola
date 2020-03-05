using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Data.SQLite;

namespace PruebasConsola
{
    class EntradaSalida
    {
        public const string FILEDB = @"URI=file:C:\Users\Ale\Source\Repos\alemartin84\PruebasConsola\ale.db";
        public static string ReadString(string prompt)
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

        public static int ReadInt(string prompt, int low, int high)
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

        public static void DiccToFile(Dictionary<string, int> diccionario, String archivo)
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

        public static void LineToFile(string linea, String archivo)
        {
            StreamWriter writer;
            writer = new StreamWriter(archivo);


            writer.WriteLine("{0}", linea);
           

            writer.Close();
        }

        public static void ReadRecipeFromFile(string archivo)
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

        public static void SerializeToFile(Receta recet)
        {
            FileStream stream = new FileStream((recet.NombreReceta+".bin"), FileMode.Create, FileAccess.Write);
            BinaryFormatter b = new BinaryFormatter();
            b.Serialize(stream, recet);
            stream.Close();
            
        }

        public static void DeserializeFile(String nombrearchivo, ref Receta recet)
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

        public static void ShowFilesinConsole(string extension)
        {
            DirectoryInfo dir = new DirectoryInfo(Environment.CurrentDirectory);
            foreach (FileInfo file in dir.GetFiles(extension))
            {
                Console.WriteLine(file.Name.Remove(file.Name.Length - 4));
            }

        }

        public static void SerializeToSqlite(Receta recet)
        {

            string cs = FILEDB; //COMMIT

            var con = new SQLiteConnection(cs);
            con.Open();

           
            var cmd = new SQLiteCommand(con);

                                
            byte[] arData;

            using (MemoryStream stream = new MemoryStream())
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(stream,recet);
                arData = stream.ToArray();
                stream.Close();
            }

            cmd.CommandText = "INSERT INTO Recetas(NombreReceta, Datos) VALUES(@NombreReceta, @Datos)";

            cmd.Parameters.AddWithValue("@NombreReceta", recet.NombreReceta);
            cmd.Parameters.AddWithValue("@Datos", arData);
            cmd.ExecuteNonQuery();
            con.Close();

        }

        public static void DeserealizeFromSqlite(String receta, ref Receta recet)
        {
            string cs = FILEDB; //COMMIT

            var con = new SQLiteConnection(cs);
            con.Open();

            
            string stm = "SELECT Datos FROM Recetas WHERE NombreReceta='" + receta + "'";

            var cmd = new SQLiteCommand(stm, con);
           // SQLiteDataReader rdr = cmd.ExecuteReader();

            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {

                    Stream stream = new MemoryStream(GetBytes(reader));
                    BinaryFormatter deserializer = new BinaryFormatter();
                    recet = (Receta)deserializer.Deserialize(stream);
                    
                    
                }
                
            }

            con.Close();    

        }

        public static bool CheckIfRecordExists(String record)
        {
            string cs = FILEDB; 

            var con = new SQLiteConnection(cs);
            con.Open();

            string stm = "SELECT count(*) FROM Recetas WHERE NombreReceta='" + record + "'";

            var cmd = new SQLiteCommand(stm, con);
            
            if (Convert.ToInt32(cmd.ExecuteScalar()) > 0) //SI HAY MAS DE UNO ES QUE EXISTE
            {
                con.Close();
                return true;
            }
            else
            {
                con.Close();
                return false;
            }

        }

        public static byte[] GetBytes(SQLiteDataReader reader)
        {
            const int CHUNK_SIZE = 2 * 1024;
            byte[] buffer = new byte[CHUNK_SIZE];
            long bytesRead;
            long fieldOffset = 0;
            using (MemoryStream stream = new MemoryStream())
            {
                while ((bytesRead = reader.GetBytes(0, fieldOffset, buffer, 0, buffer.Length)) > 0)
                {
                    stream.Write(buffer, 0, (int)bytesRead);
                    fieldOffset += bytesRead;
                }
                return stream.ToArray();
                //return stream;
            }
        }
    }
}
