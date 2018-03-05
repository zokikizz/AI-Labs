using System;

namespace AI_Lab4.NuralNetworkLibrary.ActivationFunction
{
    public class SigmoidFunction : IActivationFunction
    {
        public double calculateOutput(double net)
        {
            return (1/(1+Math.Exp(-net)));
        }
    }
}