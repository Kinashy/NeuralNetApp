using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using KinashyTensor.Bases;

namespace KinashyTensor
{
    public class SimpleMatrix<T> : Matrix<T> where T : unmanaged
    {
        public readonly ITensorFactory _factory = SimpleFactory.Instance;
        public SimpleMatrix(int rows, int columns)
        {
            _values = (from i in Enumerable.Range(0, rows)
                      select _factory.CreateVector<T>(columns)).ToList();
        }
        public override object Clone()
        {
            var clone = _factory.CreateMatrix<T>(ColumnsCount, RowsCount);
            for (int i = 0; i < RowsCount; i++)
                for (int j = 0; j < ColumnsCount; j++)
                    clone[i][j] = _values[i][j];
            return clone;
        }

        public override Vector<T> ToVector() => _factory.CreateVector(_values.SelectMany(x => x).ToList());

        public override Matrix<T> Trans()
        {
            var trans = _factory.CreateMatrix<T>(ColumnsCount, RowsCount);
            for (int i = 0; i < RowsCount; i++)
                for (int j = 0; j < ColumnsCount; j++)
                    trans[j][i] = _values[i][j];
            return trans;
        }

        public override Matrix<T> SubMatrix(int rowIndex, int rowCount, int columnIndex, int columnCount)
        {
            var sub = _factory.CreateMatrix<T>(rowCount, columnCount);
            for (int i = rowIndex; i < rowCount; i++)
                for (int j = columnIndex; j < columnCount; j++)
                    sub[i][j] = _values[i][j];
            return sub;
        }
    }
}
