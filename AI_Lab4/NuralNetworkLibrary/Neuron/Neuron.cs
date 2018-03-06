using System.Collections.Generic;
using AI_Lab4.NuralNetworkLibrary.ActivationFunction;
using AI_Lab4.NuralNetworkLibrary.InputFunction;
using AI_Lab4.NuralNetworkLibrary.Synapse;

namespace AI_Lab4.NuralNetworkLibrary.Neuron
{
    public class SimpleNeuron : INeuron
    {
        public List<ISynapse> listOfInputs { get; set; }
        public List<ISynapse> listOfOutputs { get; set; }
        public IActivationFunction activationFunction  { get; set; }
        public IInputFunction inputFunction  { get; set; }

        public double Sigma {get; set;}

        public double Output { get; set;}
        
        public SimpleNeuron(IActivationFunction actFun, IInputFunction inpFun)
        {
            this.activationFunction = actFun;
            this.inputFunction = inpFun;

            this.listOfOutputs = new List<ISynapse>();
            this.listOfInputs = new List<ISynapse>();
        }
        public void CalculateOutput()
        {
            this.Output = this.activationFunction.calculateOutput(this.inputFunction.calculateInput(this.listOfInputs));

            //to say what is output to others
            foreach(ISynapse s in this.listOfOutputs)
            {
                s.Input = this.Output;
            }
        }

    }
}