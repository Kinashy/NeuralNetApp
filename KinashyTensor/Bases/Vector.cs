using System.Collections;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace KinashyTensor.Bases
{
    public abstract class Vector<T> : IEnumerable<T>, ICloneable, IDisposable where T : unmanaged
    {
        protected IList<T> _values = null!;
        public IEnumerator<T> GetEnumerator() => _values.AsEnumerable().GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => _values.GetEnumerator();
        public virtual void Dispose()
        {
            _values?.Clear();
            _values = null!;
        }
        public T this[int column]
        {
            get => _values[column];
            set => _values[column] = value;
        }
        public abstract object Clone();
        public static T operator *(Vector<T> left, Vector<T> right) => left.Mul(left, right);
        public static Vector<T> operator *(Vector<T> vector, T value) => vector.Mul(vector, value);
        public static Vector<T> operator *(Vector<T> left, Matrix<T> right) => left.Mul(left, right);
        public static Vector<T> operator +(Vector<T> left, Vector<T> right) => left.Add(left, right);
        public static Vector<T> operator -(Vector<T> left, Vector<T> right) => left.Subtract(left, right);
        public abstract T Mul(Vector<T> left, Vector<T> right);
        public abstract Vector<T> Mul(Vector<T> left, Matrix<T> right);
        public abstract Vector<T> Mul(Vector<T> vector, T value);
        public abstract Vector<T> Add(Vector<T> left, Vector<T> right);
        public abstract Vector<T> Subtract(Vector<T> left, Vector<T> right);
    }
}
