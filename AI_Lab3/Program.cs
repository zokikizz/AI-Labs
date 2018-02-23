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

            GenericAlgorithmForTSP tsp = new GenericAlgorithmForTSP(10000, 15, tour, tour.listOfCities.Find(x => x.ID== 1));
            
            tsp.ExecuteAlgorithm();
            tsp.printTheBest();


        }
    }
}
