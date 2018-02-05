using System;
using System.IO;
using System.Collections.Generic;
using Item;


namespace UserIO
{
    public class IO 
    {
        public static void Reader(string fileName, List<item> itemsLoader, Backpack backpack)
        {
            try
            {
                StreamReader stream = new StreamReader(fileName);

                if( itemsLoader == null)
                    itemsLoader = new List<item>();
                if( backpack == null)
                    backpack = new Backpack();

                string line = string.Empty;
                
                for(int i = 0; i < 3; i++) { line = stream.ReadLine(); /*Console.WriteLine(line);*/ }
                char [] separators = {' '};
                
                //Console.WriteLine((line).Split(separators, StringSplitOptions.None)[1]);
                int numberOfItems = Int32.Parse((line).Split(separators, StringSplitOptions.None)[1]);
                
                line = stream.ReadLine();
                //Console.WriteLine((line).Split(separators, StringSplitOptions.None)[1]);
                backpack.Dimensions = Int32.Parse((line).Split(separators, StringSplitOptions.None)[1]);
                
                line = stream.ReadLine();
                //Console.WriteLine((line).Split(separators, StringSplitOptions.None)[2]);
                backpack.MaxWeight = Int32.Parse((line).Split(separators, StringSplitOptions.None)[2]);

                       
                for(int i = 0; i < 2; i++) { line = stream.ReadLine(); /*Console.WriteLine(line);*/ } 

                string [] temp;
                //Console.WriteLine("************************************************************");
                while((line = stream.ReadLine()) != "EOF")
                {
                    //Console.WriteLine(line);
                    temp = line.Split(separators, StringSplitOptions.None);

                    itemsLoader.Add(new item(){ Id = Int32.Parse(temp[0]), Benefit = Int32.Parse(temp[1]), Weigth = Int32.Parse(temp[2]) });

                }
                
            } 
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}