using System;
using Structures;
using System.IO;

namespace AI_Lab2
{
    class Program
    {
        static void Main(string[] args)
        {
            ReadFromFile("SpainMap.txt");
        }

        public static void ReadFromFile(string file)
        {
            string line = string.Empty;
            using(StreamReader stream = new StreamReader(file))
            {
                while( (line = stream.ReadLine()) != null)
                    Console.WriteLine(line);
            }
        }
    }
}
