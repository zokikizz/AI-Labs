using System;
using AI_Lab4.NuralNetworkLibrary.Neuron;

namespace AI_Lab4.NuralNetworkLibrary.Synapse
{
    public interface ISynapse
    {
        double Weight { get; set; }

        double previousWeight { get; set; }
        double Input { get; set; }


        INeuron fromNeuron {get; set; }
        INeuron toNeuron {get; set;}
        void initWeight();

        bool IsFromNeuron(INeuron n);
        void UpdateWeigth(double learningRate, double delta);
    }
}