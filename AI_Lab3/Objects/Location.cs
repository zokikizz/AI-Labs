using System;
using System.Collections.Generic;
using System.Linq;

namespace CustomObjects 
{
    //genom
    public class Location 
    {
        public int ID { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
        
        public bool isVisited { get; set; }

        public Location() { this.ID = 0; this.X = 0; this.Y = 0; this.isVisited = false; }

        public Location(int id, double x, double y) { this.ID = id; this.X = x; this.Y = y; this.isVisited = false; }

        public override string ToString()
        {
            return ID + ". X = " + this.X + " Y = " + this.Y;
        }
    }


    //individuals
    public class Trevel : IComparable
    {
        public List<Location> listOfCities { get; set; }

        public double fitness { get; set; }

        public Trevel() { 
            this.listOfCities = new List<Location>(); 
            this.fitness = 0;
        }


        public void calculateFitness()
        {
            this.fitness = 0;
            for(int i = 0; i < listOfCities.Count; i++)
            {
                this.fitness += Math.Sqrt(Math.Pow(listOfCities[i].X - listOfCities[i+1].X,2) 
                + Math.Pow(listOfCities[i].Y - listOfCities[i+1].Y,2));
            }
        }


        public int CompareTo(Object n)
        {
            Trevel par = (Trevel)n;

            return (int)(Math.Round(this.fitness,0) - Math.Round(par.fitness,0));
        }

        public override string ToString()
        {
            string listOfLocations = string.Empty;
            int i = 0;
            foreach(Location el in listOfCities)
            {
                listOfLocations  += (++i).ToString() + ".   " + el.ToString() + "\n";
            }

            return listOfLocations;
        }

    }

    public class Generation
    {
        //population
        public List<Trevel> population { get; set; }


        public Generation()
        {
            population = new List<Trevel>();
        }


        public Generation(List<Trevel> Population, int maxInOnPopulation)
        {
            this.population = Population;
        }

        public Trevel getTheFittestIndiviual()
        {
            if( this.population.Count > 0)
            {
                Trevel theFittest = this.population[0];

                for(int i = 1; i < this.population.Count; i++)
                {
                    if(theFittest.fitness > this.population[i].fitness)
                    {
                        theFittest = this.population[i];
                    }
                }

                return theFittest;
            }
            
            return null;
        }
    }

    // TSP => trevel sellman problem
    public class GenericAlgorithmForTSP
    {
        private Trevel places;

        private Location start;
        public List<Generation> Generations { get; set; }


        public int MaxInOneGeneration { get; set; }

        public int MaxGeneration { get; set; }

        public GenericAlgorithmForTSP(int maxgeneration, int maxInOneGeneration, Trevel listOfCities, Location s) 
        {
            this.Generations = new List<Generation>();

            this.MaxInOneGeneration = maxInOneGeneration;
        
            this.MaxGeneration = maxgeneration;

            this.places = listOfCities;

            this.start = s;

            this.places.listOfCities.Remove(s);

        }

        public void generateFirstgeneration()
        {

            Generation init = new Generation();
            // new population
            Trevel pop = new Trevel();

            Random random = new Random();
            List<KeyValuePair<int, int>> listOfAddedPlaces = new List<KeyValuePair<int, int>>();



            for( int i = 0; i < MaxInOneGeneration; i ++)
            {
                //generate some random id
                foreach(Location item in places.listOfCities)
                {
                    listOfAddedPlaces.Add(new KeyValuePair<int, int>(random.Next(), item.ID));
                }
                
                var sortedByRandomValue = from item in listOfAddedPlaces 
                            orderby item.Key 
                            select item;

                //pop.listOfCities.Add(this.start);

                foreach(KeyValuePair<int,int> item in sortedByRandomValue)
                {
                    pop.listOfCities.Add(places.listOfCities.Find(x => x.ID == item.Value));
                }

                //pop.listOfCities.Add(this.start);

                init.population.Add(pop);
                listOfAddedPlaces = new List<KeyValuePair<int, int>>();
                pop = new Trevel();
            }

            this.Generations.Add(init);
        }

        public void printGeneration(int numOfGeneraiton)
        {
            foreach(Trevel item in Generations[0].population)
            {
                Console.WriteLine(item.ToString());
            }
        }

        public void ExecuteAlgorithm()
        {

        }


        //works
        public void Mutate(Trevel individual)
        {

            Random rand = new Random();
            int first = (int)(rand.NextDouble()*individual.listOfCities.Count);

            int second = (int)(rand.NextDouble()*individual.listOfCities.Count);


            Console.WriteLine("Start: " + first + " end: " + second);

            while(first == second)
            {
                first = (int)(rand.NextDouble()*individual.listOfCities.Count);

                second = (int)(rand.NextDouble()*individual.listOfCities.Count);

            }


            Location[] arr = individual.listOfCities.ToArray();
            Location temp = arr[first];

            arr[first] = arr[second];
            arr[second] = temp;
            individual.listOfCities = arr.ToList();
        }


        //works
        public Trevel crossOver(Trevel parent1, Trevel parent2)
        {
            Trevel child = new Trevel();
            Random rand = new Random();

            int start = (int)(rand.NextDouble()*parent1.listOfCities.Count);
            int end = (int)(rand.NextDouble()*parent1.listOfCities.Count);

         
            Location[] childLoc = new Location[parent1.listOfCities.Count];

            for(int i = 0; i < childLoc.Length; i++)
                childLoc[i] = null;

            for(int i = 0 ; i < parent1.listOfCities.Count; i++)
            {
                if((start < end) && (i > start) && (i < end))
                {
                    childLoc[i] = parent1.listOfCities[i];
                }
                else if( (end < start) && (i > end) && (i < start))
                {
                    childLoc[i] = parent1.listOfCities[i];
                    
                }
            }

            for(int i = 0; i < parent2.listOfCities.Count; i++)
            {
                if(!childLoc.Contains(parent2.listOfCities[i]))
                {
                    for(int j = 0; j < childLoc.Length; j++)
                    {
                        if(childLoc[j]==null)
                        {
                            childLoc[j] = parent2.listOfCities[i];
                            break;
                        }
                    }
                }
            }

            child.listOfCities = childLoc.ToList();

            return child;

        }

        public void FindFitnesInGeneration()
        {

        }

    }
}