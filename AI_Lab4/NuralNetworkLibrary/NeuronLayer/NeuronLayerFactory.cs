using AI_Lab4.NuralNetworkLibrary.ActivationFunction;
using AI_Lab4.NuralNetworkLibrary.InputFunction;
using AI_Lab4.NuralNetworkLibrary.Neuron;

namespace AI_Lab4.NuralNetworkLibrary.NeuronLayerNamespace
{
    public class NeuronLayerFactory
    {
        
        public static NeuronLayer CreateLayer(int numberOfNeuronsInLayer, IActivationFunction activationFunction, IInputFunction inputFunction)
        {

            NeuronLayer temp = new NeuronLayer();

            SimpleNeuron neuron;
            
            for(int i = 0; i < numberOfNeuronsInLayer; i++)
            {
                neuron = new SimpleNeuron(activationFunction, inputFunction);
                temp.addNeuron(neuron);    
            }

            return temp; 
        }
    }
}