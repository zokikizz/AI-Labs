using System.Collections.Generic;
using AI_Lab4.NuralNetworkLibrary.Neuron;
using AI_Lab4.NuralNetworkLibrary.Synapse;

namespace AI_Lab4.NuralNetworkLibrary.NeuronLayerNamespace
{
    public class NeuronLayer
    {
        public List<SimpleNeuron> listOfNeurons;

        public bool IsItFirstLayer { get; set; }

        public bool IsItLast { get; set;}

        public NeuronLayer()
        {
            this.listOfNeurons = new List<SimpleNeuron>();
        }
        public void addNeuron(SimpleNeuron newNeuron)
        {
            this.listOfNeurons.Add(newNeuron);
        }

        public void ConnectToInput(NeuronLayer inputLayer)
        {
            ISynapse temp;
                
            if(!inputLayer.IsItFirstLayer)
            {
                foreach(SimpleNeuron inputNeuron in inputLayer.listOfNeurons)
                    foreach(SimpleNeuron current in this.listOfNeurons)
                    {
                        temp = new HiddenSynapse(inputNeuron, current);
                        inputNeuron.listOfOutputs.Add(temp);
                        current.listOfInputs.Add(temp);
                    }
            }
            else
            {
                //for first layer think
            }
        }
    }
}