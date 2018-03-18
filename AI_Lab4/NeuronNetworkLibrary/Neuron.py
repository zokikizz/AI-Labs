import numpy as np
import math


def sigmoid(net):
    return 1 / (1 + math.exp(-net))


class Neuron:
    def __init__(self, down_layer):
        self.output = 0
        self.downlayer = down_layer
        self.weights = None
        if down_layer is not None:
            self.weights = []
            for i in down_layer.list_of_neurons:
                self.weights.append(np.random.random())
        self.net = 0
        self.delta = 0

    def get_output(self):
        """ returns output of this neuron """
        return self.output

    def set_output(self, out):
        """ just for input layer, to set inputs"""
        self.output = out

    def get_delta(self):
        """ returns delta of this neuron """
        return self.delta

    def set_delta(self, value=0):
        """ to change delta value, default is delta = 0"""
        self.delta = value

    def get_weights(self):
        """ :return all weights of this neuron"""
        weights_array = []
        for i in self.weights:
            weights_array.append(i)
        return weights_array

    def compute_net(self):
        """ :returns computed net = sum(w[i]*down_layer.list_of_neurons[0].output"""
        list_of_outputs = self.downlayer.get_outputs()
        list_of_weights = self.get_weights()
        # mul list_of_outputs[i] * list_of_weights[i], res is new list
        res = [list_of_outputs * list_of_weights for list_of_outputs, list_of_weights
               in zip(list_of_outputs, list_of_weights)]

        self.net = sum(res)
        return self.net

    def compute_output(self):
        """ compute and return output for this neuron"""
        if self.downlayer is not None:
            self.output = sigmoid(self.compute_net())
        return self.output

    def compute_delta(self, list_of_upper_delta, list_of_upper_weights):

        temp = self.output * (1 - self.output)
        res = [list_of_upper_delta * list_of_upper_weights for list_of_upper_delta, list_of_upper_weights
               in zip(list_of_upper_delta, list_of_upper_weights)]
        self.delta = temp * sum(res)
        return

    def update_weights(self, learning_rate, outputs_of_down_layer):
        """ to update weights for this neuron"""

        # print("1." + str(self.weights))
        temp = learning_rate * self.delta
        for index, we in enumerate(self.weights):
            # print(we)
            # print(temp * outputs_of_down_layer[index])
            # print(we + (temp * outputs_of_down_layer[index]))
            self.weights[index] = we + (temp * outputs_of_down_layer[index])

        # print("2." + str(self.weights))
        return
