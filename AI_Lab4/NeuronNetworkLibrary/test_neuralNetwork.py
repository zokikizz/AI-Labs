from unittest import TestCase
import NeuronNetworkLibrary.NeuralNetwork as ann


class TestNeuralNetwork(TestCase):
    def test_create_NeuronNetwork(self):
        test = [3, 3, 2, 1]
        n = ann.NeuralNetwork(4, test)
        number_in_layers = []
        for i in n.layers:
            number_in_layers.append(len(i.list_of_neurons))

        self.assertEqual(number_in_layers, test)

    def test_set_inputs(self):
        test_inputs = [1, 2, 3]
        n = ann.NeuralNetwork(4, [3, 3, 2, 1])
        n.set_input(test_inputs)
        self.assertEqual(n.layers[0].get_outputs(), test_inputs)