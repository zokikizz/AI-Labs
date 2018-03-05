using AI_Lab4.Objects.Neurons;
using AI_Lab4.Objects.NuarlLayer;
using AI_Lab4.Objects;

namespace AI_Lab4.Objects.SimpleNuralNetwork
{
    /// <summary>
    /// Factory used to create layers.
    /// </summary>
    public class NeuralLayerFactory
    {
        public NeuralLayer CreateNeuralLayer(int numberOfNeurons, IActivationFunction activationFunction, IInputFunction inputFunction)
        {
            var layer = new NeuralLayer();

            for (int i = 0; i < numberOfNeurons; i++)
            {
                var neuron = new Neuron(activationFunction, inputFunction);
                layer.Neurons.Add(neuron);
            }

            return layer;
        }
    }

}