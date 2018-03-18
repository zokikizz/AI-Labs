import numpy as np
import math
import NeuronNetworkLibrary.Neuron as neuron


class NeuronLayer:
    def __init__(self, number_of_neurons, down_layer):
        """ number_of_neurons - number of neurons that this layer should have
        down_layer - layer that should be below this layer, for first(input) layer it is None"""
        self.list_of_neurons = []
        self.upper_layer = None
        # to create list of neurons
        self.downLayer = down_layer
        for index in range(number_of_neurons):
            self.list_of_neurons.append(neuron.Neuron(down_layer))

    def set_upper_layer(self, layer):
        self.upper_layer = layer
        return

    def get_outputs(self):
        """ returns output of neurons in this layer"""
        outputs = []

        for ne in self.list_of_neurons:
            outputs.append(ne.get_output())
        return outputs

    def set_output(self, index, out):
        """ just for input layer, to set inputs"""
        self.list_of_neurons[index].set_output(out)

    def get_upper_deltas(self):
        deltas = []
        for i in self.upper_layer.list_of_neurons:
            deltas.append(i.get_delta())
        return deltas

    def get_upper_weights(self, neuron_no):
        weights = []
        for i in self.upper_layer.list_of_neurons:
            temp = i.get_weights()
            weights.append(temp[neuron_no])
        return weights

    def get_outputs_of_down_layer(self):
        return self.downLayer.get_outputs()

    def compute_output(self):
        """ for upper layers (hidden and output), not for first(input) layer"""
        for n in self.list_of_neurons:
            n.compute_output()

    def compute_delta(self):
        """ computing and setting delta for all neurons in this layer """

        for index, n in enumerate(self.list_of_neurons):
            upper_delta = self.get_upper_deltas()
            upper_weights = self.get_upper_weights(index)
            n.compute_delta(upper_delta, upper_weights)
        return

    def update_weights(self, learning_rate):
        """ updating weights """
        if self.downLayer is not None:
            for n in self.list_of_neurons:
                outputs_of_down_layer = self.get_outputs_of_down_layer()
                n.update_weights(learning_rate, outputs_of_down_layer)
        return
