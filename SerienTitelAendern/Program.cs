using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SerienTitelAendern
{
    class Program
    {
        static bool tryAgain = false;

        static void Main(string[] args)
        {
            string path = @"F:\Clemens\Eigene Videos\Filme & Serien";

            NextFolder(path);
        }

        private static void NextFolder(string path)
        {
            foreach (string folderPath in Directory.GetDirectories(path))
            {
                NextFolder(folderPath);
            }

            foreach (string filePath in Directory.GetFiles(path))
            {
                if (IsSeriesName(Path.GetFileNameWithoutExtension(filePath)))
                {
                    string newName = GetChangedFileName(Path.GetFileNameWithoutExtension(filePath));
                    string newPath = GetFilePath(filePath, newName);

                    if (File.Exists(filePath)) { }
                    //   if (!File.Exists(newPath)) File.Create(newPath);
                    File.Move(filePath, newPath);
                }
            }
        }

        private static bool IsSeriesName(string name)
        {
            try
            {
                char[] cs = name.ToCharArray();
                int index = name.IndexOf("Staffel ");

                if (index == -1) return false;

                index += 7;

                do
                {
                    index++;
                }
                while (char.IsNumber(name[index]));

                if (index != name.IndexOf(" Folge ")) return false;

                index += 6;

                do
                {
                    index++;
                }
                while (char.IsNumber(name[index]));


                if (name[index] != ' ' || name[index + 1] != '-' || name[index + 2] != ' ' || name[index + 3] == ' ')
                {
                    Console.WriteLine(name);
                    Console.ReadLine();

                    return false;
                }
            }
            catch
            {
                Console.WriteLine(name);
                Console.ReadLine();

                return false;
            }

            return true;
        }

        private static string GetChangedFileName(string name)
        {
            return name.Replace("Staffel ", "S").Replace(" Folge ", "E");
        }

        private static string GetFilePath(string original, string newName)
        {
            return Path.Combine(Path.GetDirectoryName(original), newName + Path.GetExtension(original));
        }
    }
}
