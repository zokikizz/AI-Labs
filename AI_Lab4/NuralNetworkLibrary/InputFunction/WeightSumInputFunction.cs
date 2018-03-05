using System.Collections.Generic;
using AI_Lab4.NuralNetworkLibrary.Synapse;

namespace AI_Lab4.NuralNetworkLibrary.InputFunction
{
    public class WeightSumInputFunction : IInputFunction
    {
        //computing net
        public double calculateInput(List<ISynapse> listOfSynapse)
        {
            double total = 0;
            foreach(ISynapse el in listOfSynapse)
            {
                total += el.Weight * el.Input;
            }

            return total;

        }
    }
}