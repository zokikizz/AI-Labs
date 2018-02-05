using System; 


namespace Item 
{
    public class item
    {
        public item() 
        {
        }

        public int Weigth { get; set; }
        
        public int Benefit { get; set; }
        
        public int Id { get; set; }
    }

    public class Backpack
    {
        public int MaxWeight { get; set; }

        public Backpack () {
            MaxWeight = 0;
            Dimensions = 0;
        }
        
        public int Dimensions { get; set; }
    }
}