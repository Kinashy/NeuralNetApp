using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KinashyTensor.Bases;

namespace KinashyTensor
{
    public class SimpleFactory : ITensorFactory
    {
        private static readonly Lazy<SimpleFactory> _instance = new Lazy<SimpleFactory>(() => new SimpleFactory());
        private SimpleFactory() { }
        public static SimpleFactory Instance { get => _instance.Value; }
        public Matrix<T> CreateMatrix<T>(int rows, int columns) where T : unmanaged 
            => new SimpleMatrix<T>(rows, columns);

        public Vector<T> CreateVector<T>(IList<T> values) where T : unmanaged
            => new SimpleVector<T>(values);

        public Vector<T> CreateVector<T>(int size) where T : unmanaged
            => new SimpleVector<T>(size);
    }
}
