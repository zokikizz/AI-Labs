import NeuronNetworkLibrary.NeuronLayer as nl


class NeuralNetwork:

    def __init__(self, number_layers, list_of_neurons, learningrate = 0.2):
        """
        number of layers [int] -  how many layers we want to create
        list_of_neurons [int] - list of int in which every int present number of neurons in that same layer
        example => list_of_neurons[0] is number of neurons in 0 (first/input) layer
        learning_rate [float] - default value is 0.2
        """

        self.layers = []
        self.learing_rate = learningrate

        # creating layers and neurons in them
        previous_layer = []
        for i in range(number_layers):
            if i == 0:
                under_layer = None
            else:
                under_layer = previous_layer

            self.layers.append(nl.NeuronLayer(list_of_neurons[i], under_layer))
            previous_layer = self.layers[i]

        self.connect_layers_with_upper_layer()
        return

    def connect_layers_with_upper_layer(self):
        """ every layer after this will know what layer is upper layer"""
        for index, layer in enumerate(self.layers):
            if index != len(self.layers):
                if layer != self.layers[-1]:
                    layer.set_upper_layer(self.layers[index+1])
        return

    def set_input(self, inputs):
        """ inputs is list of elements which will be output for first/input layer """

        if len(inputs) != len(self.layers[0].list_of_neurons):
            raise Exception("Length of input array and input layer is not same!")
        else:
            for index, inp in enumerate(inputs):
                self.layers[0].set_output(index, inp)

    def think(self, inputs):
        """ feedforward function"""
        self.set_input(inputs)
        for layer in self.layers:
            layer.compute_output()
        return

    def get_output(self):
        """ to get output of last layer"""
        return self.layers[-1].get_outputs()

    def trening(self, inputs, target):
        """ trening function """
        self.think(inputs)
        if len(self.layers[-1].list_of_neurons) == 1:
            output = self.get_output()
            error = target - output[0]
            # compute and set delta for last layer
            self.layers[-1].list_of_neurons[0].set_delta(output[0]*(1-output[0])*error)
            # to compute and set data for other layers
            self.layers.reverse()
            for layer in self.layers:
                if layer != self.layers[0]:
                    layer.compute_delta()

            self.layers.reverse()
            # update weights
            for layer in self.layers:
                layer.update_weights(self.learing_rate)

        else:
            raise Exception("You can't have more than one output from network. It's not implemented yet.")
