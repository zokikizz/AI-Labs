using System.Collections.Generic;
using AI_Lab4.NuralNetworkLibrary.ActivationFunction;
using AI_Lab4.NuralNetworkLibrary.InputFunction;
using AI_Lab4.NuralNetworkLibrary.Synapse;

namespace AI_Lab4.NuralNetworkLibrary.Neuron
{
    public interface INeuron
    {
        // some Id maybe?
         List<ISynapse> listOfInputs {get;set;}
         List<ISynapse> listOfOutputs {get;set;}


        double Sigma {get; set;}
         IActivationFunction activationFunction {get;set;}
         IInputFunction inputFunction {get;set;}

         double Output{ get; set;}
         
         
    }
}