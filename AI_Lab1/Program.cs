using System;
using UserIO;
using Item;
using System.Collections.Generic;
using System.Diagnostics;
using SearchingAlgorithms;

namespace AI_Lab1
{
    class Program
    {
        static void Main(string[] args)
        {
            
 //           Console.WriteLine("Hello World!");
    
            List<item> listOfItems = new List<item>();
            Backpack backpack = new Backpack();
        
            
            IO.Reader("./Items.txt", listOfItems, backpack); // loading list and backpack

            Console.WriteLine("Backpack dimension: " + backpack.Dimensions + "\nMax. kg: " + backpack.MaxWeight + "\nNumber of items: " + listOfItems.Count);

            Console.WriteLine("Items:");
           for(int i = 0; i < listOfItems.Count; i++)
           {
               Console.WriteLine(listOfItems[i].Id + " " + " " + listOfItems[i].Benefit + " " + listOfItems[i].Weigth);

           }
            Console.WriteLine();
            Algorithms alg = new Algorithms(listOfItems, backpack);

            alg.executeAlgorithmBFS();
            
            Console.WriteLine("Best: " + alg.bestBenefitBFS.totalBenefit + " Depth: " + alg.bestBenefitBFS.Depth);

            Console.WriteLine();

            alg.executeAlgorithmDFS();
            
            Console.WriteLine("Best: " + alg.bestBenefitBFS.totalBenefit + " Depth: " + alg.bestBenefitBFS.Depth);
            
        }
    }
}
