using KinashyTensor.Bases;

namespace KinashyNN
{
    public class Perceptron<T> where T : unmanaged
    {
        public Vector<T> Weights { get; init; }
        public Func<T, T> Activate { get; init; }
        public T Compute(Vector<T> X) => Activate(X * Weights);
        public Perceptron(Vector<T> weight, Func<T, T> activate)
        {
            Weights = weight;
            Activate = activate;
        }
    }
}