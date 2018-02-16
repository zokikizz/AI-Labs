using System;
using Structures;
using System.IO;

namespace AI_Lab2
{
    class Program
    {
        static void Main(string[] args)
        {
            Graph graph = new Graph();
            ReadFromFile("SpainMap.txt", graph);


            graph.GreedyBFS();
            
            
            Console.WriteLine();

            Console.WriteLine(" Results of Greedy BFS:");


            Node current = graph.Destination;
            while(current.cameFrom != current)
            {
                Console.WriteLine(" City name: " + current.cityName + " total cost: " + current.totalCost + " heruistic: " + current.heurisitc);
                current = current.cameFrom;
            }

            Console.WriteLine(" City name: " + current.cityName + " total cost: " + current.totalCost + " heruistic: " + current.heurisitc);


            graph.Clean();

            
            graph.AStar();
            
            Console.WriteLine();

            
            Console.WriteLine(" Results of Astar:");


            current = graph.Destination;
            while(current.cameFrom != current)
            {
                Console.WriteLine(" City name: " + current.cityName + " total cost: " + current.totalCost + " heruistic: " + current.heurisitc);
                current = current.cameFrom;
            }

            Console.WriteLine(" City name: " + current.cityName + " total cost: " + current.totalCost + " heruistic: " + current.heurisitc);



        }

        public static void ReadFromFile(string file, Graph g)
        {
            string line = string.Empty;
            int i =0;
            using(StreamReader stream = new StreamReader(file))
            {
                
                while( i < 6)
                {
                    line = stream.ReadLine();
                    Console.WriteLine(line); 
                    i++;
                }

                i=1;

                string[] words;
                line = stream.ReadLine();
                while(line != string.Empty)
                {
                    words = line.Split(" ");
                    Node current = new Node() { cityName = words[0], heurisitc = Int32.Parse(words[1]) };
                    g.listOfNodes.Add(current);
                    if(current.heurisitc == 0)
                        g.Destination = current;
                    if(current.cityName == "Malaga")
                        g.Start = current;
                    
                    Console.WriteLine( i++ + ".City name: " + current.cityName + " h(n)=" + current.heurisitc);
                    line = stream.ReadLine();
                }

                Console.WriteLine();
                Console.WriteLine(stream.ReadLine());
                Console.WriteLine();
                Edge e;
                Node temp;

                while( (line = stream.ReadLine()) != null)
                {
                    Console.WriteLine(line);

                    
                    words = line.Split(" ");

                    if(words.Length == 3)
                    {
                        e = new Edge() { Cost = Int32.Parse(words[2])};
                        temp = g.listOfNodes.Find(x => x.cityName == words[0]);
                        e.Nodes.Add(temp);
                        temp.Edges.Add(e);
                      
                        temp = g.listOfNodes.Find(x => x.cityName == words[1]);
                        
                        e.Nodes.Add(temp);
                        temp.Edges.Add(e);
                        Console.WriteLine(e.Nodes[0].cityName + " " + e.Nodes[1].cityName + " " + e.Cost); 
                    }
                }
            }
        }
    }
}
