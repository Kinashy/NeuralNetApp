using KinashyTensor.Bases;
using MiniExcelLibs;

namespace NeuralNetApp.Core.Database
{
    public static class KnowledgeRepository
    {
        public static Knowledge<T> ReadData<T>(string path, ITensorFactory factory) where T : unmanaged
        {
            List<Vector<T>> values = new List<Vector<T>>();
            var rows = MiniExcel.Query(path);
            Matrix<T> x = factory.CreateMatrix<T>(rows.Count(), ((IDictionary<string, object>)rows.First()).Count - 1);
            Vector<T> y = factory.CreateVector<T>(rows.Count());
            for (int i = 0; i < rows.Count(); i++)
            {
                var casted = ((IDictionary<string, object>)rows.ElementAt(i)).Values.Cast<T>();
                for (int j = 0; j < x.ColumnsCount - 1; j++)
                    x[i][j] = casted.ElementAt(j);
                y[i] = casted.Last();
            }
            return new Knowledge<T>(x, y);
        }
    }
}
