using System;
using System.Collections.Generic;
using System.Linq;
using AI_Lab4.NuralNetworkLibrary.Neuron;
using AI_Lab4.NuralNetworkLibrary.NeuronLayerNamespace;
using AI_Lab4.NuralNetworkLibrary.Synapse;

namespace AI_Lab4.NuralNetworkLibrary
{
    public class NuralNetwork
    {
        double learningRate { get; set; }
        double [] inputs;
        double output;

        public List<NeuronLayer> listOfLayers;


        public void Train()
        {

        }

        public void BackpropagationForHiddenLayer(NeuronLayer underLayer)
        {

            List<SimpleNeuron> listOfHiddenNeurons = null; 
            if(!underLayer.IsItFirstLayer)
                listOfHiddenNeurons = underLayer.listOfNeurons;

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

            if(underLayer.IsItFirstLayer)
            {
                
                    //u apdejtu treba sigma * input value
            }
            else
            {
                foreach (INeuron item in listOfHiddenNeurons)
                {
                    foreach (ISynapse synapse in item.listOfInputs)
                    {
                        synapse.UpdateWeigth(this.learningRate, item.Output * item.Sigma);
                    }
                }
            }
           
        }

        //in this structure we have one output in ANN
        public void BackpropagationForOutputLayer(double targer)
        {
            if(this.listOfLayers.Count == 0)
            {
                Console.WriteLine("Network is empty.");
                return;
            }
            
            NeuronLayer outputLayer = ((List<NeuronLayer>)this.listOfLayers.Select(x => x.IsItLast)).First();
            outputLayer.listOfNeurons.First().Sigma = outputLayer.listOfNeurons.First().Output * (1-outputLayer.listOfNeurons.First().Output)
             *(targer - outputLayer.listOfNeurons.First().Output);

            if(outputLayer == null)
            {
                Console.WriteLine("There is no layer with one neuron which should be output layer.");
                return;
            }

            NeuronLayer underLayer = null;
            List<INeuron> hiddenLayer = new List<INeuron>(); //to create list for first hiddenLayer after output layer
            foreach(ISynapse s in outputLayer.listOfNeurons.First().listOfInputs)
            {
                s.UpdateWeigth(this.learningRate, outputLayer.listOfNeurons.First().Sigma* s.fromNeuron.Output);
                hiddenLayer.Add(s.fromNeuron);

                //to find underlayer
                if(underLayer == null)
                    foreach(NeuronLayer el in this.listOfLayers)
                    {
                        foreach(SimpleNeuron neuron in el.listOfNeurons)
                        {
                            if(neuron == s)
                                underLayer = el;
                        }
                    }
            }

            

            BackpropagationForHiddenLayer(underLayer);
        }


    }
}