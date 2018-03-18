import unittest
import numpy as np
import NeuronNetworkLibrary.test_neuralNetwork as anntest
import NeuronNetworkLibrary.NeuralNetwork as ann

ann_test_names = [
    "test_create_NeuronNetwork",
    "test_set_inputs"
]


def suite():
    suite = unittest.TestSuite()
    for i in ann_test_names:
        suite.addTest(anntest.TestNeuralNetwork(i))
    return suite


def read_from_file(fileName):
    inputs = []
    targets = []
    with open(fileName, 'r') as f:
        counter = 0
        for line in f:
            inputs.append([])
            targets.append([])
            for num in line.split(","):
                if float(num) != 1.0 and float(num) != -1.0:
                    inputs[counter].append(float(num))
                else:
                    if float(num) == 1.0:
                        targets[counter].append(0.75)
                    else:
                        targets[counter].append(0.25)
            counter = counter + 1

    return inputs, targets


if __name__ == '__main__':
    runner = unittest.TextTestRunner()
    runner.run(suite())

    np.random.seed(0)

    net = ann.NeuralNetwork(4, [3, 3, 2, 1])
    inputs, targets = read_from_file('data.dat')
    for j in range(50):
        for i in range(1500):
            net.trening(inputs[i], targets[i][0])

    correct = 0
    survived = 0
    ann_survived = 0

    gues = ''
    shouldBe = 'p'

    for i in range(700):
        net.think(inputs[1500+i])
        output = net.get_output()[0]

        if output >= 0.5:
            gues = 's'
        else:
            gues = 'd'

        if targets[1500+i][0] == 0.75:
            shouldBe = 's'
        else:
            shouldBe = 'd'

        if targets[1500+i][0] == 0.75:
            survived = survived + 1
            if gues == shouldBe:
                ann_survived = ann_survived + 1

        if gues == shouldBe:
            correct = correct + 1

    print(float(correct)/700)
    print(ann_survived/survived)

