using System;
using System.Collections.Generic;
using System.Linq;
using AI_Lab4.NuralNetworkLibrary.ActivationFunction;
using AI_Lab4.NuralNetworkLibrary.InputFunction;
using AI_Lab4.NuralNetworkLibrary.Neuron;
using AI_Lab4.NuralNetworkLibrary.NeuronLayerNamespace;
using AI_Lab4.NuralNetworkLibrary.Synapse;

namespace AI_Lab4.NuralNetworkLibrary
{

    /// first layer in list of layers is input layer and the last is output layer which contain just one neuron !!!
    public class NuralNetwork
    {
        double learningRate { get; set; }

        public List<NeuronLayer> listOfLayers;

        public NuralNetwork()
        {
            this.listOfLayers = new List<NeuronLayer>();
            learningRate = 0.2;
        }


        public void Train(List<double> inputValues, double target)
        { 
            if(inputValues.Count == this.listOfLayers.First().listOfNeurons.Count)
            {
                SetUpInputValues(inputValues);

                Execute();

                BackpropagationForOutputLayer(target);
            }
            else
            {
                Console.WriteLine("Error,");
            }

        }

        public void SetUpInputValues(List<double> input)
        {
            if(input.Count == this.listOfLayers.First().listOfNeurons.Count)
            {
                for(int i=0; i < input.Count; i++)
                {
                    this.listOfLayers.First().listOfNeurons[0].listOfInputs[i].Input = input[i];
                }
            }
            else
            {
                Console.WriteLine("Error");
            }
        }

        public void Execute()
        {
            foreach(NeuronLayer layer in this.listOfLayers)
            {
                foreach(SimpleNeuron neuron in layer.listOfNeurons)
                {
                    neuron.CalculateOutput();
                }
            }
        }

        public double getOutput()
        {
            return this.listOfLayers.Last().listOfNeurons.First().Output;
        }

        public void createNewLayer(int numberOfNeurons, IActivationFunction activation, IInputFunction input)
        {
            NeuronLayer layer = NeuronLayerFactory.CreateLayer(numberOfNeurons, activation, input);

            if(this.listOfLayers.Count == 0)
            {
                layer.IsItFirstLayer = true;
            }
            else if(numberOfNeurons == 1)
            {
                layer.IsItLast = true;
            }
            
            if(this.listOfLayers.Count != 0)
                layer.ConnectToInput(this.listOfLayers[this.listOfLayers.Count-1]);
            else
                layer.ConnectToInput(null); //if it is first layer then we don't have input layer and we will create input synaps

            this.listOfLayers.Add(layer);
        }

        public void BackpropagationForHiddenLayer(NeuronLayer currentLayer, int layerNumber)
        {

            List<SimpleNeuron> listOfHiddenNeurons = null; 
            if(!currentLayer.IsItFirstLayer)
                listOfHiddenNeurons = currentLayer.listOfNeurons;

            //calculating sigma for every neuron
            int i = 0;
            foreach(INeuron current in listOfHiddenNeurons)
            {
                current.Sigma = current.Output * ( 1 - current.Output);

                double temp = 0;
                foreach(ISynapse synapse in current.listOfOutputs)
                {
                    temp += synapse.Weight * synapse.toNeuron.Sigma;
                }

                current.Sigma *= temp;
            }

            if(currentLayer.IsItFirstLayer)
            {
                    foreach(INeuron item in currentLayer.listOfNeurons)
                    {
                        foreach(InputSynapse syn in item.listOfInputs)
                        {
                            syn.UpdateWeigth(learningRate, item.Sigma * syn.Input);
                        }
                    }
            }
            else
            {

                NeuronLayer nextLayer = null;  
                foreach (INeuron item in listOfHiddenNeurons)
                {
                    foreach (ISynapse synapse in item.listOfInputs)
                    {
                        synapse.UpdateWeigth(this.learningRate, item.Output * item.Sigma);
                        
                        // if layers are not ordered
                        // if(nextLayer == null)
                        //     nextLayer = this.findUnderLayer(synapse);

                    }
                }

                if(nextLayer == null && (layerNumber-1) < 0)
                {
                    Console.WriteLine("There is no next layer. " );
                    if((layerNumber-1) < 0)
                    {
                        Console.WriteLine("End.");
                    }
                    return;
                }

                this.BackpropagationForHiddenLayer(nextLayer, layerNumber - 1);
            }


            
           
        }

        //in this structure we have one output in ANN
        public void BackpropagationForOutputLayer(double target)
        {
            if(this.listOfLayers.Count == 0)
            {
                Console.WriteLine("Network is empty.");
                return;
            }
            
            //just when last layer is output layer otherwise we must uncomment line under this one
            NeuronLayer outputLayer = this.listOfLayers[this.listOfLayers.Count-1];
            // NeuronLayer outputLayer = ((List<NeuronLayer>)this.listOfLayers.Select(x => x.IsItLast)).First();
            outputLayer.listOfNeurons.First().Sigma = outputLayer.listOfNeurons.First().Output * (1-outputLayer.listOfNeurons.First().Output)
             *(target - outputLayer.listOfNeurons.First().Output);

            if(outputLayer == null)
            {
                Console.WriteLine("There is no layer with one neuron which should be output layer.");
                return;
            }


            NeuronLayer underLayer = null;
            foreach(ISynapse s in outputLayer.listOfNeurons.First().listOfInputs)
            {
                s.UpdateWeigth(this.learningRate, outputLayer.listOfNeurons.First().Sigma* s.fromNeuron.Output);

                //when layers are not in order
                // if(underLayer == null)
                //     underLayer = this.findUnderLayer(s);
            }

            

            if(underLayer == null && this.listOfLayers.Count == 1)
            {
                Console.WriteLine("There is no underlayer!!!");
                return;
            }
            else
            {
                underLayer = this.listOfLayers[this.listOfLayers.Count -2];
            }

            BackpropagationForHiddenLayer(underLayer, this.listOfLayers.Count -2);
        }

        private NeuronLayer findUnderLayer(ISynapse synapse)
        {
            

            NeuronLayer underLayer = null;
           
            foreach(NeuronLayer el in this.listOfLayers)
            {
                foreach(SimpleNeuron neuron in el.listOfNeurons)
                {
                    if(neuron == (SimpleNeuron)(synapse.fromNeuron))
                        underLayer = el;
                }
            }
            
            return underLayer;
        }


    }
}