using KinashyTensor.Bases;
using MiniExcelLibs;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace NeuralNetApp.Core.Database
{
    public static class ImageRepository
    {
        public static IEnumerable<Image<T>> ReadAll<T>(string path, ITensorFactory factory) where T : unmanaged
        {
            var rows = MiniExcel.Query(path);
            var matrix = factory.CreateMatrix<T>(rows.Count(), ((IDictionary<string, object>)rows.First()).Count);
            for (int i = 0; i < matrix.RowsCount; i++)
            {
                for (int j = 0; j < matrix.ColumnsCount; j++)
                {
                    matrix[i][j] = (T)((IDictionary<string, object>)rows.ElementAt(i)).ElementAt(j).Value;
                }
            }
            throw new NotImplementedException();
        }
    }
}
