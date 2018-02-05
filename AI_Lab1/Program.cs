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
       //     Stack<item> stekOfItems = new Stack<item>();
        
            
            IO.Reader("./Items.txt", listOfItems, backpack); // loading list and backpack

            Console.WriteLine(backpack.Dimensions + " " + backpack.MaxWeight + " " + listOfItems.Count);

           for(int i = 0; i < listOfItems.Count; i++)
           {
               Console.WriteLine(listOfItems[i].Id + " " + " " + listOfItems[i].Benefit + " " + listOfItems[i].Weigth);
            //    stekOfItems.Push(listOfItems[i]);
           }

            // Console.WriteLine("_________________________________________________________________________________________");

            // for(int i = 0; i < listOfItems.Count; i++)
            // {
            //     Console.WriteLine(stekOfItems.Peek().Id + " " + " " + stekOfItems.Peek().Benefit + " " + stekOfItems.Peek().Weigth);
            //     stekOfItems.Pop();
            // }
            
 //           Console.WriteLine("Hello World!");
            

            Console.WriteLine();
            BFS BFSAlgoritm = new BFS(listOfItems, backpack);

            var watch = Stopwatch.StartNew();
            BFSAlgoritm.executeAlgorithmBFS();
            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;

            Console.WriteLine("Execution time: " + elapsedMs/1000.0);
            Console.WriteLine("Best: " + BFSAlgoritm.bestBenefitBFS.totalBenefit + " Depth: " + BFSAlgoritm.bestBenefitBFS.Depth);

            Console.WriteLine();

            watch = Stopwatch.StartNew();
            BFSAlgoritm.executeAlgorithmDFS();
            watch.Stop();
            elapsedMs = watch.ElapsedMilliseconds;

            Console.WriteLine("Execution time: " + elapsedMs/1000.0);
            Console.WriteLine("Best: " + BFSAlgoritm.bestBenefitBFS.totalBenefit + " Depth: " + BFSAlgoritm.bestBenefitBFS.Depth);
            
          
            


        }
    }
}
