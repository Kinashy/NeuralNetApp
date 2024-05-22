using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using KinashyTensor.Bases;
using KinashyTensor.Helpers;

namespace KinashyTensor
{
    public class SimpleVector<T> : Vector<T> where T : unmanaged
    {
        public readonly static ITensorFactory _factory = SimpleFactory.Instance;
        public SimpleVector(int size)
        {
            _values = new List<T>(new T[size]);
        }

        public SimpleVector(IList<T> values)
        {
            _values = values;
        }

        public override Vector<T> Add(Vector<T> left, Vector<T> right)
        {
            if (left.Count() != right.Count())
                throw new Exception("Vectors sizes for Add operation should be equal.");
            int size = left.Count();
            var add = from i in Enumerable.Range(0, size) select
                    GenericMath.Add(left[i], right[i]);
            return _factory.CreateVector<T>(add.ToList());
        }

        public override object Clone()
        {
            var clone = _factory.CreateVector<T>(_values.Count);
            for (int i = 0; i < _values.Count; i++)
                clone[i] = _values[i];
            return clone;
        }


        public override T Mul(Vector<T> left, Vector<T> right)
        {
            if (left.Count() != right.Count())
                throw new Exception("Vectors sizes for Mul operation should be equal.");
            T res = default;
            for (int i = 0; i < right.Count(); i++)
            {
                T mul = GenericMath.Mul<T>(left[i], right[i]);
                res = GenericMath.Add<T>(res, mul);
            }
            return res;
        }

        public override Vector<T> Mul(Vector<T> left, Matrix<T> right)
        {
            if (left.Count() != right.Count())
                throw new Exception("Vector size for Mul operation should be equal matrixes rows count.");
            int size = left.Count();
            var transed = right.Trans();
            var mul = from i in Enumerable.Range(0, size)
                    select transed[i] * left;
            return _factory.CreateVector<T>(mul.ToList());
        }

        public override Vector<T> Mul(Vector<T> vector, T value)
        {
            int size = vector.Count();
            var mul = from i in Enumerable.Range(0, size)
                    select GenericMath.Mul(vector[i], value);
            return _factory.CreateVector<T>(mul.ToList());
        }

        public override Vector<T> Subtract(Vector<T> left, Vector<T> right)
        {
            if (left.Count() != right.Count())
                throw new Exception("Vectors sizes for Add operation should be equal.");
            int size = left.Count();
            var subtruct = from i in Enumerable.Range(0, size)
                      select GenericMath.Subtract(left[i], right[i]);
            return _factory.CreateVector<T>(subtruct.ToList());
        }
    }
}
