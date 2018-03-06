using System;
using System.Collections.Generic;
using System.IO;
using AI_Lab4.NuralNetworkLibrary;
using AI_Lab4.NuralNetworkLibrary.ActivationFunction;
using AI_Lab4.NuralNetworkLibrary.InputFunction;
using AI_Lab4.NuralNetworkLibrary.NeuronLayerNamespace;

namespace AI_Lab4
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("Hello World!");
            NuralNetwork network = new NuralNetwork();
            //NeuronLayerFactory factory = new Neur(); 

            for(int i = 3; i > 0; i--)
                network.createNewLayer(i, new SigmoidFunction(), new WeightSumInputFunction());

           
                //store current line
                string line = string.Empty;
                //splited line
                string[] values;
                List<double> inputs = new List<double>();
                double target;


                int step = 0;
                int interations = 0;

                while(interations < 200)
                {
                    using( StreamReader reader = new StreamReader("./assignment 4 titanic.dat"))
                    { 
                        
                        for(int i= 0; i < 8; i++)  
                            { reader.ReadLine(); }//Console.WriteLine(reader.ReadLine());   
                        for(step = 0; step < 2200; step++)
                        {
                            line = reader.ReadLine();
                        // Console.WriteLine(line);
                            values = line.Split(",");
                            //trening
                            for(int i = 0; i < 3; i++)
                            {
                                inputs.Add(Double.Parse(values[i]));
                            }

                            if(Double.Parse(values[3]) == -1)
                                target = 0.25;
                            else
                                target = 0.75;

                            network.Trening(inputs, target);

                            inputs = new List<double>();

                            //see result
                           // Console.WriteLine("Error: " + (target - network.listOfLayers[network.listOfLayers.Count-1].listOfNeurons[0].Output));
                        }
                    }
                    interations++;
                }

                int correct = 0;
                int error = 0;

                using( StreamReader reader = new StreamReader("./assignment 4 titanic.dat"))
                { 
                    string outputOfANN;
                    string shouldBe;
                    for(int i= 0; i < 8; i++)  
                        { reader.ReadLine(); }//Console.WriteLine(reader.ReadLine()); 
                    int j = 0; 
                    while((line =reader.ReadLine()) != null && j < 700)
                    {
                    // Console.WriteLine(step++ +". " + line);
                        
                        values = line.Split(",");

                        for(int i = 0; i < 3; i++)
                        {
                            inputs.Add(Double.Parse(values[i]));
                        }

                        network.Execute();

                        if(network.listOfLayers[network.listOfLayers.Count-1].listOfNeurons[0].Output >= 0.5 )
                        {
                            outputOfANN = "S";
                        }
                        else
                        {
                            outputOfANN = "D";
                        }

                        if(Double.Parse(values[3]) == -1)
                        {
                            shouldBe = "D";
                        }
                        else
                        {
                            shouldBe = "S";
                        }

                        if(outputOfANN == shouldBe)
                        {
                            correct++;
                        }
                        else
                        {
                            error++;
                        }

                        j++;
                    }

                    Console.WriteLine(((double)correct/(correct+error)) * 100 + "%");
                }
                
        }   
    }
}
