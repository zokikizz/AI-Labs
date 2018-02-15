using System;
using System.IO;


namespace CustomObjects
{
    public class Reader
    {
        
        public static void readListOfCitiesFromFile(string filename, Trevel tour)
        {
            using(StreamReader fileReader = new StreamReader(filename))
            {
                string line = string.Empty;

                int numberOfCities = 0;

                string[] words;

                Location current;

                Console.WriteLine("Information about input data:");
                for(int i = 0; i < 6; i++)
                {
                    Console.WriteLine(line = fileReader.ReadLine());
                    words = line.Split(' ');
                    if(words[0] == "DIMENSION:")
                    {
                       // Console.WriteLine("Read:" + arr[1]);\
                       numberOfCities = Int32.Parse(words[1]);
                    }

                } 

                for(int i = 0; i < numberOfCities; i++)
                {
                    line = fileReader.ReadLine();

                    words = line.Split(" ");

                    current = new Location(Int32.Parse(words[0]),  Double.Parse(words[1]), Double.Parse(words[2]));

                  //  Console.WriteLine("Id: " + current.ID + " x: " + current.X + " y: " + current.Y);

                    tour.listOfCities.Add(current);
                    
                }   

            }
        }
    }
}