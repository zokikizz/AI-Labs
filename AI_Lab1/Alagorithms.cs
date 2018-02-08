using System;
using System.Collections.Generic;
using TREE;
using Item;


namespace SearchingAlgorithms
{
    public class Algorithms
    {
        public List<item> listOfItems = new List<item>();
        public Backpack backpack;

        public Stack<Node> stack;
        public Queue<Node> fifo;



        public Node bestBenefitBFS = null;
        public Node bestBenefitDFS = null;
        

        public Algorithms(List<item> lOfItems, Backpack UserBackPack) {
            this.listOfItems = lOfItems;

            this.backpack = UserBackPack;
            bestBenefitBFS = new Node() { Depth = 0, totalBenefit = 0, restOfWigth = backpack.MaxWeight,
             Item = null, parent = null, listOfChildren = lOfItems, Dimension = this.backpack.Dimensions } ;

            fifo = new Queue<Node>();
            fifo.Enqueue(new Node() { Depth = 0, totalBenefit = 0, restOfWigth = backpack.MaxWeight,
             Item = null, parent = null, listOfChildren = lOfItems, Dimension = this.backpack.Dimensions } );

            stack = new Stack<Node>();
            stack.Push(new Node() { Depth = 0, totalBenefit = 0, restOfWigth = backpack.MaxWeight,
             Item = null, parent = null, listOfChildren = lOfItems, Dimension = this.backpack.Dimensions } );
        }

        public void executeAlgorithmBFS() 
        {
            Node child;
            Node root;
            item nextItem;
                
            while( fifo.Count > 0) 
            {
                root = fifo.Dequeue();

                if( root.Depth < backpack.Dimensions && listOfItems.Count > 0)
                {
                    nextItem = listOfItems[root.Depth];

                    child = new Node() { Item = nextItem, Depth = root.Depth+1, 
                        totalBenefit = nextItem.Benefit + root.totalBenefit,
                        restOfWigth = root.restOfWigth - nextItem.Weigth,
                        Dimension = root.Dimension - 1,
                        parent = root };

                    if(child.restOfWigth >= 0)
                    {
                        if(bestBenefitBFS.totalBenefit < child.totalBenefit)
                            bestBenefitBFS = child;


                        child.Included = true;
                        fifo.Enqueue(child);

                        child = new Node() { Item = nextItem, Depth = root.Depth + 1, 
                            totalBenefit = root.totalBenefit,
                            restOfWigth = root.restOfWigth,
                            Dimension = root.Dimension,
                            parent = root };

                        child.Included = false;

                        fifo.Enqueue(child);
                    }

                }

            }


            Console.WriteLine("BFS:");
            Node current = bestBenefitBFS;
            Console.WriteLine("Benefit: " + current.totalBenefit + " Depth: " + current.Depth + " Rest weight: " + current.restOfWigth);
            while(current.Item != null)
            {
                if(current.Included == true)
                    Console.WriteLine("Included: " + current.Item.Id + " " + current.Item.Benefit + " " + current.Item.Weigth);
                else
                    Console.WriteLine("Not included: " + current.Item.Id + " " + current.Item.Benefit + " " + current.Item.Weigth);
                

                current = current.parent;
            }
        }

        public void executeAlgorithmDFS() 
        {
            Node child;
            Node root;
            item nextItem;
                
            while( fifo.Count > 0) 
            {
                root = stack.Pop();

                if( root.Depth < backpack.Dimensions && listOfItems.Count > 0)
                {
                    nextItem = listOfItems[root.Depth];

                    child = new Node() { Item = nextItem, Depth = root.Depth+1, 
                        totalBenefit = nextItem.Benefit + root.totalBenefit,
                        restOfWigth = root.restOfWigth - nextItem.Weigth,
                        Dimension = root.Dimension - 1,
                        parent = root };

                    if(child.restOfWigth >= 0)
                    {
                        if(bestBenefitBFS.totalBenefit < child.totalBenefit)
                            bestBenefitBFS = child;


                        child.Included = true;
                        stack.Push(child);

                        child = new Node() { Item = nextItem, Depth = root.Depth + 1, 
                            totalBenefit = root.totalBenefit,
                            restOfWigth = root.restOfWigth,
                            Dimension = root.Dimension,
                            parent = root };

                        child.Included = false;

                        stack.Push(child);
                    }

                }

            }

            Console.WriteLine("DFS:");

            Node current = bestBenefitBFS;
            Console.WriteLine("Benefit: " + current.totalBenefit + " Depth: " + current.Depth + " Rest weight: " + current.restOfWigth);
            while(current.Item != null)
            {
                if(current.Included == true)
                    Console.WriteLine("Included: " + current.Item.Id + " " + current.Item.Benefit + " " + current.Item.Weigth);
                else
                    Console.WriteLine("Not included: " + current.Item.Id + " " + current.Item.Benefit + " " + current.Item.Weigth);
                

                current = current.parent;
            }
        }

    }
}