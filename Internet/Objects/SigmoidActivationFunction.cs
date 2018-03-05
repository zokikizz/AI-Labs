using System;

namespace AI_Lab4.Objects
{
    public class SigmoidActivationFunction : IActivationFunction
    {
        private double _coeficient;

        public SigmoidActivationFunction(double coeficient)
        {
            _coeficient = coeficient;
        }
        public double calculateOutput(double input)
        {
            //koef???
            return (1/(1+Math.Exp(-input))); //* _coeficient)));
        }
    }
}