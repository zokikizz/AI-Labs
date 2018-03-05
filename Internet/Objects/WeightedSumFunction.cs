using System;
using System.Collections.Generic;
using System.Linq;
using AI_Lab4.Objects.Synapses;

namespace AI_Lab4.Objects
{
    public class WeightedSumFunction : IInputFunction
    {
        public double CalculateInput(List<ISynapse> input)
        {
            return input.Select(x => x.Weight * x.GetOutput()).Sum();
        }
    }
}