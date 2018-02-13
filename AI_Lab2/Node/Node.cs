using System;
using System.Collections.Generic;

namespace Structures 
{
    public class Edge
    {
        public List<Node> Nodes { get; set; }
        
        public Edge() {
            this.Nodes = new List<Node>();
        }
        public int Cost { get; set; }
    }
    public class Node : IComparable
    {
        public List<Edge> Edges { get; set; }
        
        public int totalCost = 0;

        public bool isItBFSAlgorithm;

        public Node cameFrom { get; set; }

        public Node() { this.Edges = new List<Edge>(); this.cameFrom = null; }

        public string cityName { get; set; }

        public int heurisitc { get; set; }

        public int CompareTo(Object n)
        {
            Node j = (Node) n;
            if(this.isItBFSAlgorithm)
            {
                return this.heurisitc - j.heurisitc;
            }
            else 
            {
                return (totalCost + this.heurisitc) - (j.totalCost + j.heurisitc);
            }
        }

        public override string ToString()
        {
            return cityName + " " + heurisitc;
        }
    }

    public class PriorityQueue <T>
    {
        public List <T> data { get; set; }

        public PriorityQueue()
        {
            this.data = new List <T>();
        }

        public T Peek()
        {
            T frontItem = data[0];
            return frontItem;
        }

        public int Count()
        {
            return this.data.Count;
        }

        public void Enqueue(T obj)
        {
            this.data.Add(obj);
            this.data.Sort();
        }

        public void Dequeue()
        {
            if(data.Count > 0)
                data.RemoveAt(0);
        }

        public void print()
        {

            Console.WriteLine();
            Console.WriteLine("Queue:");
            foreach(T element in data)
            {
                Console.WriteLine(element.ToString());
            }
        }
    }

    public class Graph
    {
        private PriorityQueue<Node> queue;

        public List<Node> listOfNodes { get; set; }

        public Graph() { listOfNodes = new List<Node>(); queue = new PriorityQueue<Node>(); } 
        
        public Node Start { get; set; }

        public Node Destination { get; set; }

        public void Clean() 
        {
            while(this.queue.Count() > 0)
            {
                this.queue.Dequeue();
            }

            foreach(Node el in this.listOfNodes)
            {
                el.cameFrom = null;
                el.totalCost = 0;
            }

        }

        public void GreedyBFS()
        {

            foreach(Node n in this.listOfNodes)
                n.isItBFSAlgorithm = true;

            queue.Enqueue(this.Start);
            this.Start.cameFrom = this.Start;
            this.Start.totalCost = 0;

            Node current;
            while( queue.Count() > 0)
            {
                current = queue.Peek();
                queue.Dequeue();

                if( current == this.Destination)
                    break;

                foreach(Edge el in current.Edges)
                {
                    Node next = el.Nodes.Find(x => x.cityName != current.cityName);
                    if(next.cameFrom == null)
                    {
                        next.totalCost = current.totalCost + el.Cost;
                        next.cameFrom = current;
                        queue.Enqueue(next);
                    }
                }

                //queue.print();
            }



        }


        // for next in graph.neighbors(current):
        //     new_cost = cost_so_far[current] + graph.cost(current, next)
        //     if next not in cost_so_far or new_cost < cost_so_far[next]:
        //         cost_so_far[next] = new_cost
        //         priority = new_cost + heuristic(goal, next)
        //         frontier.put(next, priority)
        //         came_from[next] = current
                
        public void AStar()
        {

            foreach(Node n in this.listOfNodes)
                n.isItBFSAlgorithm = false;

            queue.Enqueue(this.Start);
            this.Start.cameFrom = this.Start;
            this.Start.totalCost = 0;

            Node current;
            while( queue.Count() > 0)
            {
                current = queue.Peek();
                queue.Dequeue();

                if( current == this.Destination)
                    break;

                foreach(Edge el in current.Edges)
                {
                    Node next = el.Nodes.Find(x => x.cityName != current.cityName);
                    int newCost = current.totalCost + el.Cost;
                    if(next.cameFrom == null || (newCost < next.totalCost))
                    {
                        next.totalCost = newCost;
                        next.cameFrom = current;
                        queue.Enqueue(next);
                    }
                }

                //queue.print();
            }

        }
    }

}