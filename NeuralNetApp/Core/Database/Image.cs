using KinashyTensor.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetApp.Core.Database
{
    public class Image<T> where T : unmanaged
    {
        public Matrix<T> Value { get; init; }
        public Image(Matrix<T> matrix)
        {
            Value = matrix;
        }
    }
}
