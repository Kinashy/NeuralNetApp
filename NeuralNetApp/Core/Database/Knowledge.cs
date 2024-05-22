using KinashyTensor.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetApp.Core.Database
{
    public class Knowledge<T> where T : unmanaged
    {
        public Matrix<T> X { get; init; }
        public Vector<T> Y { get; init; }
        public Knowledge(Matrix<T> x, Vector<T> y)
        {
            X = x;
            Y = y;
        }
    }
}
