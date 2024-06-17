using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinashyTensor.Bases
{
    public abstract class Matrix<T> : IEnumerable<Vector<T>>, ICloneable, IDisposable where T : unmanaged
    {
        protected IList<Vector<T>> _values = null!;
        public int RowsCount { get => _values.Count(); }
        public int ColumnsCount { get => _values.First().Count(); }
        public Vector<T> this[int row]
        {
            get => _values[row];
        }
        public abstract object Clone();
        IEnumerator<Vector<T>> IEnumerable<Vector<T>>.GetEnumerator() => _values.AsEnumerable().GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => _values.GetEnumerator();
        public virtual void Dispose()
        {
            for (int i = 0; i < _values.Count; i++)
            {
                _values[i].Dispose();
                _values[i] = null!;
            }
        }
        public abstract Matrix<T> Trans();
        public abstract Vector<T> ToVector();
        public abstract Matrix<T> SubMatrix(int rowIndex , int rowCount, int columnIndex, int columnCount);

    }
}
