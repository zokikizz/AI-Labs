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

        public double probability;

        public double fitness { get; set; }

        public Trevel() { 
            this.listOfCities = new List<Location>(); 
            this.fitness = 0;
        }


        public double calculateFitness(Location startAndEnd)
        {
            this.fitness = 0;
            this.fitness +=  Math.Sqrt(Math.Pow(startAndEnd.X - listOfCities[0].X,2) 
                + Math.Pow(startAndEnd.Y - listOfCities[0].Y,2));
            for(int i = 0; i < listOfCities.Count-1; i++)
            {
                this.fitness += Math.Sqrt(Math.Pow(listOfCities[i].X - listOfCities[i+1].X,2) 
                + Math.Pow(listOfCities[i].Y - listOfCities[i+1].Y,2));
            }


            return (this.fitness +=  Math.Sqrt(Math.Pow(listOfCities[listOfCities.Count-1].X - startAndEnd.X,2) 
                + Math.Pow(listOfCities[listOfCities.Count-1].Y - startAndEnd.Y,2)));
        }


        public int CompareTo(Object n)
        {
            Trevel par = (Trevel)n;

            return (int)(Math.Round(this.fitness,0) - Math.Round(par.fitness,0));
        }

        public override string ToString()
        {
            // string listOfLocations = string.Empty;
            // int i = 0;
            // foreach(Location el in listOfCities)
            // {
            //     listOfLocations  += (++i).ToString() + ".   " + el.ToString() + "\n";
            // }

            return "Distance: " + this.fitness + " km"; //+ "\n" +listOfLocations;
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


        public Generation(List<Trevel> Population)
        {
            this.population = Population;
        }

    }

    // TSP => trevel sellman problem
    public class GenericAlgorithmForTSP
    {
        private Trevel places;

        double mutationRate = 0.015;
        public Trevel theBest = null;

        public Location start;
        public List<Generation> Generations { get; set; }


        public int MaxInOneGeneration { get; set; }

        public int MaxGenerations { get; set; }

        public GenericAlgorithmForTSP(int maxgeneration, int maxInOneGeneration, Trevel listOfCities, Location s) 
        {
            this.Generations = new List<Generation>();

            this.MaxInOneGeneration = maxInOneGeneration;
        
            this.MaxGenerations = maxgeneration;

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
            this.generateFirstgeneration();

            for(int currentGeneration = 0; currentGeneration < this.MaxGenerations; currentGeneration++)
            {

                //roullete selection and crossovering for creating new generation
                Selection_CreationOfOffspring_And_MutateNewIndividuals(currentGeneration);

                if(currentGeneration % 250 == 0)
                {

                    Console.WriteLine();
                    Console.WriteLine("Current generation: " + currentGeneration + " ");
                    this.printTheBest();
                } 
            }
        }


        //works
        public void Mutate(Trevel individual)
        {
            
            Random rand = new Random();

            if(rand.NextDouble() < this.mutationRate)
            {
                int first = (int)(rand.NextDouble()*individual.listOfCities.Count);

                int second = (int)(rand.NextDouble()*individual.listOfCities.Count);


                // Console.WriteLine("Start: " + first + " end: " + second);

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

        public void Selection_CreationOfOffspring_And_MutateNewIndividuals(int numberOfGeneration) 
        {

            //we will create new generation
            Generation newGeneration = new Generation();

            List<Trevel> currentPopulation = this.Generations[numberOfGeneration].population;

            double totalFitness = 0.0;
            //calculate fitness for every indiviual
            foreach(Trevel trevel in currentPopulation)
            {
                totalFitness += 1/trevel.calculateFitness(this.start);
            }


            double previous = 0;
            //calculate probability
            foreach(Trevel temp in currentPopulation)
            {
                temp.probability =  ((1/temp.fitness/totalFitness)) + previous;
                previous = temp.probability;
            }


            Random roullete = new Random();
            List<Trevel> parents = new List<Trevel>();
            while(newGeneration.population.Count < this.MaxInOneGeneration -(int)((currentPopulation.Count/100.0) * 5))
            {
                while(parents.Count < 2)
                {
                    Trevel temp = this.roullete(numberOfGeneration);
                    if(temp != null)
                        parents.Add(temp);
                }

                if(parents.Count == 2 && parents[0] != parents[1])
                {
                    
                    //create offspring
                    //and add new individual to new generation
                    newGeneration.population.Add(crossOver(parents[0], parents[1]));
                }

                previous = 0;
                //calculate probability
                foreach(Trevel temp in currentPopulation)
                {
                    temp.probability =  ((1/temp.fitness/totalFitness)) + previous;
                    previous = temp.probability;
                }
                
                parents = new List<Trevel>();
            }



            Trevel bestInGen = findTheFittiestInGeneration(currentPopulation);
            //to save the best if it is better than someone form earlyer solution
            this.saveTheBest(bestInGen);
            
            //mutation on new individuals
            foreach(Trevel ind in newGeneration.population)
            {
                Mutate(ind);
            }


            //add beast 5 % offparents into newgeneration
            for(int i = 0; i < (int)((currentPopulation.Count/100.0) * 5); i++)
            {
                currentPopulation.Remove(bestInGen);
                newGeneration.population.Add(bestInGen);
                bestInGen = findTheFittiestInGeneration(currentPopulation);
            }

            this.Generations.Add(newGeneration);

        }


        public void saveTheBest(Trevel bestInGen)
        {
            if((this.theBest == null))
            {
                this.theBest = bestInGen;
            }
            else if(this.theBest.fitness > bestInGen.fitness)
            {
                this.theBest = bestInGen;
            }
        }

        public Trevel findTheFittiestInGeneration(List<Trevel> pop)
        {
            Trevel best = pop[0];
            foreach(Trevel temp in pop)
            {
                if(temp.fitness < best.fitness)
                {
                    best = temp;  
                } 
            }

            return best;
        }


        public Trevel roullete(int generationNumber)
        {
            Random roullete = new Random();
            double num;
    
            num = roullete.NextDouble();
            for(int j = 0; j < this.Generations[generationNumber].population.Count; j++)
            {
                if(num < this.Generations[generationNumber].population[j].probability)
                {
                    //take it as parent
                    return this.Generations[generationNumber].population[j];
                }
            }

            return null;
        }

        public void printTheBest()
        {
            Console.WriteLine("The best trevel is:");
            Console.WriteLine(this.theBest);
        }

    }
}