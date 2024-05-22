using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinashyTensor.Bases
{
    public interface ITensorFactory
    {
        Vector<T> CreateVector<T>(IList<T> values) where T : unmanaged;
        Vector<T> CreateVector<T>(int size) where T : unmanaged;
        Matrix<T> CreateMatrix<T>(int rows, int columns) where T : unmanaged;
    }
}