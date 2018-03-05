using System.Collections.Generic;
using AI_Lab4.NuralNetworkLibrary.Synapse;

namespace AI_Lab4.NuralNetworkLibrary.InputFunction
{
    public interface IInputFunction
    {
        double calculateInput(List<ISynapse> listOfSynapse);
    }
}