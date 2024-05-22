// See https://aka.ms/new-console-template for more information
using KinashyNN;
using KinashyTensor;
using KinashyTensor.Bases;
using KinashyTensor.Helpers;
using NeuralNetApp.Core.Database;

ITensorFactory factory = SimpleFactory.Instance;
var knowledge = KnowledgeRepository.ReadData<double>("test.xlsx", factory);
PerceptronBuilder<double> builder = new();
var perceptron = builder.Build(factory, knowledge.X, knowledge.Y, GenericThresholdFunctionFamily.Default, 0.1, 1000, 
    (epoch, test, weights) =>
    {
        Console.WriteLine($"Epoch #{epoch}\nRight answers:\t{test}\t{(double)test * 100.00f/(double)knowledge.Y.Count()}\nWeights:\t{string.Join(" ", weights.Select(x => x.ToString("F7")))}\n");
    }, true);
Console.ReadKey();