using System;
using AI_Lab4.NuralNetworkLibrary.Neuron;

namespace AI_Lab4.NuralNetworkLibrary.Synapse
{
    public class HiddenSynapse : ISynapse
    {
        //from neuron
        //to neuron
        public INeuron fromNeuron {get; set;}
        public INeuron toNeuron{get; set;}


        public double previousWeight { get; set; }
        public double Weight { get; set; }
        public double Input { get; set; }

        public HiddenSynapse(INeuron from, INeuron to)
        {
            this.fromNeuron = from;
            this.toNeuron = to;
            initWeight();
        }

        public bool IsFromNeuron(INeuron n)
        {
            return this.fromNeuron == n;
        }

        public void initWeight()
        {
            Random r = new Random();
            this.Weight = r.NextDouble();
            this.previousWeight = 0;
        }

        public void UpdateWeigth(double learningRate, double delta)
        {
            this.previousWeight = this.Weight;
            this.Weight += learningRate * delta;
        }
    }
}