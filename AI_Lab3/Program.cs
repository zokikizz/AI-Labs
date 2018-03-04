using System;
using CustomObjects;

namespace AI_Lab3
{
    class Program
    {
        static void Main(string[] args)
        {
            Trevel tour = new Trevel();

            Reader.readListOfCitiesFromFile("./Assignment 3 input data berlin52.tsp", tour);

            GenericAlgorithmForTSP tsp = new GenericAlgorithmForTSP(2000, 500, tour, tour.listOfCities.Find(x => x.ID== 1));
            
            tsp.ExecuteAlgorithm();


            Console.WriteLine("======================\n Result with 2000 generations and 500 individuals in one generation:");
            
            tsp.printTheBest();

            Console.WriteLine(tsp.start);

            foreach(Location city in tsp.theBest.listOfCities)
            {
                Console.WriteLine(" " + city);
            }
            
            Console.WriteLine(tsp.start);

            Console.WriteLine("======================");


        }
    }
}
