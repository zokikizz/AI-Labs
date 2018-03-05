using System;
using System.IO;
using AI_Lab4.Objects;
using AI_Lab4.Objects.Neurons;
using AI_Lab4.Objects.SimpleNuralNetwork;
using AI_Lab4.Objects.Synapses;

namespace AI_Lab4
{
    class Program
    {
        static void Main(string[] args)
        {
            //readDataFromFile("./assignment 4 titanic.dat");

            var network = new SimpleNeuralNetwork(3);

            var layerFactory = new NeuralLayerFactory();
            network.AddLayer(layerFactory.CreateNeuralLayer(3, new SigmoidActivationFunction(0.7), new WeightedSumFunction()));
            //network.AddLayer(layerFactory.CreateNeuralLayer(1, new SigmoidActivationFunction(0.7), new WeightedSumFunction()));
            network.AddLayer(layerFactory.CreateNeuralLayer(2, new SigmoidActivationFunction(0.7), new WeightedSumFunction()));
            network.AddLayer(layerFactory.CreateNeuralLayer(1, new SigmoidActivationFunction(0.7), new WeightedSumFunction()));
            

        
            using(StreamReader read = new StreamReader("./assignment 4 titanic.dat"))
            {
                    
                double[][] expVal = new double[700][];
                double[][] trainingInput = new double[700][];
            
                int num = 0;

                string line;
                for(int i= 0; i < 8; i++)
                    Console.WriteLine(read.ReadLine());
                
                //reading input and training data
                while((line = read.ReadLine())!= null && num < 700)
                 {
                    expVal[num] = new double[] { Double.Parse(line.Split(",")[3]) };
                    trainingInput[num] = new double[] {
                        Double.Parse(line.Split(",")[0]),
                        Double.Parse(line.Split(",")[1]),
                        Double.Parse(line.Split(",")[2]) };
                    num++;
                 }   

                //for(int i = 0; i < num; i++)
                //    Console.WriteLine(i+". " + expVal[i][0]);
                network.PushExpectedValues(expVal);

                // network.PushExpectedValues(
                //     new double[][] {
                //         new double[] { 0 },
                //         new double[] { 1 },
                //         new double[] { 1 },
                //         new double[] { 0 },
                //         new double[] { 1 },
                //         new double[] { 0 },
                //         new double[] { 0 },s
                //     });

                network.Train(trainingInput, num);
                // network.Train(
                //     new double[][] {
                //         new double[] { 150, 2, 0 },
                //         new double[] { 1002, 56, 1 },
                //         new double[] { 1060, 59, 1 },
                //         new double[] { 200, 3, 0 },
                //         new double[] { 300, 3, 1 },
                //         new double[] { 120, 1, 0 },
                //         new double[] { 80, 1, 0 },
                //     }, 10000);

                while(num < 710)
                {
                    line = read.ReadLine();

                    //reading next input
                    network.PushInputValues(new double[] {
                        Double.Parse(line.Split(",")[0]),
                        Double.Parse(line.Split(",")[1]),
                        Double.Parse(line.Split(",")[2]) }
                        );
                    var outputs = network.GetOutput();

       

                    Console.WriteLine("Start " + num + ".");
                    Console.Write("Expeted: ");
                    if( Double.Parse(line.Split(",")[3]) >= 0.5)
                        Console.WriteLine("Survived");
                    else
                        Console.WriteLine("Died");
                        
                    foreach(double el in outputs)
                    {
                       // Console.WriteLine("\t Output" + el);
                        if( el >= 0.5)
                            Console.WriteLine("Survived");
                        else
                            Console.WriteLine("Died");

                    }
                    Console.WriteLine("End " + num + ".");
                    num++;
                    Console.WriteLine();
                }
             }
        }

        
        public static void readDataFromFile(string file)
        {
            using(StreamReader read = new StreamReader(file))
            {
                string line;
                for(int i= 0; i < 8; i++)
                    Console.WriteLine(read.ReadLine());
                
                int num = 0;
                while((line = read.ReadLine())!= null)
                    Console.WriteLine(num++ + "." + line);
            }
        }
    }

}
