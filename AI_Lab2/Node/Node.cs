using System;
using System.Collections.Generic;

namespace Structures 
{
    public class Edge
    {
        public Node[] Nodes { get; set; }
        public int Cost { get; set; }
    }
    public class Node
    {
        public Edge[] Edges { get; set; }

        public int heurisitc { get; set; }
    }

    public class Graph
    {

        public List<Node> MyProperty { get; set; }
        
        public Node Start { get; set; }

        public int Destination { get; set; }

        public void GreedyBFS()
        {

        }

        public void AStar()
        {
            
        }
    }

}