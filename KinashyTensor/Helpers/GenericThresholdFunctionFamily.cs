using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinashyTensor.Helpers
{
    public class GenericThresholdFunctionFamily
    {
        public static T Default<T>(T value) where T : unmanaged
            => GenericMath.GreaterThanOrEqual(value, default) ? GenericMath.Increment(default(T)) : default;
    }
}
