using System;
using System.Collections.Generic;
using Item;

namespace TREE 
{
        public class Node 
        {
            public int Depth { get; set; }
            public int restOfWigth { get; set; }

            public int totalBenefit { get; set; }

            public int Dimension { get; set; }

            public item Item { get; set; }

            public Node parent { get; set; }

            public bool Included { get; set; }

            public Node() {}

            public List<item> listOfChildren = new List<item>();

            public void copyListOfChildren(Node parent, Backpack backpack)
            {
                if(this.Depth < backpack.Dimensions) {
                    foreach(item i in parent.listOfChildren) {
                        if(i.Id != this.Item.Id && ((this.restOfWigth - i.Weigth) > 0))
                        {
                            this.listOfChildren.Add(new item() { Id = i.Id, Weigth = i.Weigth, Benefit = i.Benefit });
                        }
                    }
                }
            }

            public void printChildren() {
                Console.WriteLine("__________________________________");

                Console.WriteLine(this.Item.Id + ". ITEM CHILDREN:");
                foreach( item i in this.listOfChildren) {
                    Console.WriteLine(i.Id + " " + i.Benefit + " " + i.Weigth);
                }
        }
    }
}