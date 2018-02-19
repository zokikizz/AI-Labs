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

            GenericAlgorithmForTSP tsp = new GenericAlgorithmForTSP(10,2, tour, tour.listOfCities.Find(x => x.ID== 1));
            tsp.generateFirstgeneration();
            
            
            Console.WriteLine("I: \n" + tsp.Generations[0].population[0]);
            //Console.WriteLine("II: \n" +tsp.Generations[0].population[1]);

           // Console.WriteLine(tsp.crossOver(tsp.Generations[0].population[0],tsp.Generations[0].population[1]));
            tsp.Mutate(tsp.Generations[0].population[0]);


        }
    }
}
