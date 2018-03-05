using System;
using System.Collections.Generic;
using AI_Lab4.Objects.Synapses;

namespace AI_Lab4.Objects
{
    public interface IInputFunction
    {
        double CalculateInput(List<ISynapse> input);
    }
}