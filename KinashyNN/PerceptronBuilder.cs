using KinashyTensor.Bases;
using KinashyTensor.Helpers;

namespace KinashyNN
{
    public class PerceptronBuilder<T> where T : unmanaged
    {
        public PerceptronBuilder() 
        {
            
        }

        public Perceptron<T> Build(ITensorFactory tensorFactory, Matrix<T> x, Vector<T> t, Func<T, T> activate, T speed, int maxEpoch, Action<int, int, IEnumerable<T>> reporter, bool skipOnStupor = false)
        {
            Perceptron<T> perceptron = new Perceptron<T>(tensorFactory.CreateVector<T>(x.ColumnsCount), activate);
            int epoch = 1;
            int lastTest = 0;
            IEnumerable<T> lastWeights = (IEnumerable<T>)perceptron.Weights.Clone();
            while (epoch <= maxEpoch)
            {
                Educate(ref perceptron, ref x, ref t, speed);
                int right = GetRightAnswerCount(perceptron, ref x, ref t);
                reporter(epoch, right, perceptron.Weights);
                if(skipOnStupor)
                if (!lastWeights.SequenceEqual<T>(perceptron.Weights, EqualityComparer<T>.Create(GenericMath.Equal)) || (lastTest != right))
                {
                    lastTest = right;
                    lastWeights = (IEnumerable<T>)perceptron.Weights.Clone();
                }
                else
                    break;
                epoch++;
            }
            return perceptron;
        }
        private void Educate(ref Perceptron<T> perceptron, ref Matrix<T> x, ref Vector<T> t, T speed)
        {
            for (int i = 0; i < x.RowsCount; i++)
            {
                var y = perceptron.Compute(x[i]);
                var delta = GenericMath.Subtract(t[i], y);
                delta = GenericMath.Mul(delta, speed);
                for (int j = 0; j < perceptron.Weights.Count(); j++)
                {
                    var deltaW = GenericMath.Mul(delta, x[i][j]);
                    perceptron.Weights[j] = GenericMath.Add(perceptron.Weights[j], deltaW);
                }
            }
        }
        private int GetRightAnswerCount(Perceptron<T> perceptron, ref Matrix<T> x, ref Vector<T> t)
        {
            int right = 0;
            for (int i = 0; i < x.RowsCount; i++)
            {
                var y = perceptron.Compute(x[i]);
                var delta = GenericMath.Subtract(t[i], y);
                if (GenericMath.Equal(delta, default))
                    right++;
            }
            return right;
        }
    }
}
