using System;
using AI_Lab4.NuralNetworkLibrary.Neuron;

namespace AI_Lab4.NuralNetworkLibrary.Synapse
{
    public class InputSynapse : ISynapse
    {
        //to which inpur neuron goes this synapse
        public double Weight { get; set; }
        public double Input { get; set; }
        public double previousWeight { get; set; }
        public INeuron fromNeuron {get;set;}
        public INeuron toNeuron{get; set;}

        public InputSynapse(INeuron toNeur)
        {
            this.fromNeuron = null;
            this.toNeuron = toNeur;
            this.initWeight();
        }
        public void initWeight()
        {
            Random r = new Random();
            this.Weight = r.NextDouble();
            previousWeight = 0.5;
        }

        public bool IsFromNeuron(INeuron n)
        {
            return false;
        }

        public void UpdateWeigth(double learningRate, double delta)
        {
            this.previousWeight = this.Weight;
            this.Weight += learningRate * delta;
        }
    }
}