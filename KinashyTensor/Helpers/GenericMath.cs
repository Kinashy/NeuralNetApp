using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace KinashyTensor.Helpers
{
    public static class GenericMath
    {
        private static readonly Dictionary<(Type Type, string Op), Delegate> Cache =
            new Dictionary<(Type Type, string Op), Delegate>();
        private static Delegate GetBinaryFunction<T, TOut>(T left, T right, string funcName, BinaryExpressionCreator bodyCreator) where T : unmanaged
        {
            var t = typeof(T);
            // If op is cached by type and function name, use cached version
            if (Cache.TryGetValue((t, funcName), out var del))
                return del is Func<T, T, TOut> specificFunc
                    ? del
                    : throw new InvalidOperationException(funcName);
            var leftPar = Expression.Parameter(t, nameof(left));
            var rightPar = Expression.Parameter(t, nameof(right));
            var body = bodyCreator(leftPar, rightPar);

            var func = Expression.Lambda<Func<T, T, TOut>>(body, leftPar, rightPar).Compile();
            Cache[(t, funcName)] = func;
            return func;
        }

        private static Delegate GetUnaryFunction<T, TOut>(T value, string funcName, UnaryExpressionCreator bodyCreator) where T : unmanaged
        {
            var t = typeof(T);
            // If op is cached by type and function name, use cached version
            if (Cache.TryGetValue((t, funcName), out var del))
                return del is Func<T, TOut> specificFunc
                    ? del
                    : throw new InvalidOperationException(funcName);
            var parameter = Expression.Parameter(t, nameof(value));
            var body = bodyCreator(parameter);
            var func = Expression.Lambda<Func<T, TOut>>(body, parameter).Compile();
            Cache[(t, funcName)] = func;
            return func;
        }

        private delegate BinaryExpression BinaryExpressionCreator(Expression left, Expression right);
        private delegate UnaryExpression UnaryExpressionCreator(Expression expression);

        public static T Add<T>(T left, T right) where T : unmanaged
        {
            Func<T, T, T> add = (GetBinaryFunction<T, T>(left, right, nameof(Add), Expression.Add) as Func<T, T, T>)!;
            return add(left, right);
        }

        public static T Mul<T>(T left, T right) where T : unmanaged
        {
            Func<T, T, T> mul = (GetBinaryFunction<T, T>(left, right, nameof(Mul), Expression.Multiply) as Func<T, T, T>)!;
            return mul(left, right);
        }

        public static bool GreaterThanOrEqual<T>(T left, T right) where T : unmanaged
        {
            Func<T, T, bool> greaterThanOrEqual = (GetBinaryFunction<T, bool>(left, right, nameof(GreaterThanOrEqual), Expression.GreaterThanOrEqual) as Func<T, T, bool>)!;
            return greaterThanOrEqual(left, right);
        }

        public static bool Equal<T>(T left, T right) where T : unmanaged
        {
            Func<T, T, bool> equal = (GetBinaryFunction<T, bool>(left, right, nameof(Equal), Expression.Equal) as Func<T, T, bool>)!;
            return equal(left, right);
        }

        public static bool LessThanOrEqual<T>(T left, T right) where T : unmanaged
        {
            Func<T, T, bool> lessThanOrEqual = (GetBinaryFunction<T, bool>(left, right, nameof(LessThanOrEqual), Expression.LessThanOrEqual) as Func<T, T, bool>)!;
            return lessThanOrEqual(left, right);
        }

        public static T Abs<T>(T value) where T : unmanaged
        {
            T abs;
            if (LessThanOrEqual(value, default))
                abs = Mul(value, Decrement<T>(default));
            else
                abs = value;
            return abs;
        }

        public static T Increment<T>(T value) where T : unmanaged
        {
            Func<T, T> increment = (GetUnaryFunction<T, T>(value, nameof(Increment), Expression.Increment) as Func<T, T>)!;
            return increment(value);
        }

        public static T Decrement<T>(T value) where T : unmanaged
        {
            Func<T, T> decrement = (GetUnaryFunction<T, T>(value, nameof(Decrement), Expression.Decrement) as Func<T, T>)!;
            return decrement(value);
        }

        public static void InitializeDefault<T>(ref T[] values) where T : unmanaged
        {
            for (int i = 0; i < values.Count(); i++)
                values[i] = default;
        }

        public static T Subtract<T>(T left, T right) where T : unmanaged
        {
            Func<T, T, T> sub = (GetBinaryFunction<T, T>(left, right, nameof(Subtract), Expression.Subtract) as Func<T, T, T>)!;
            return sub(left, right);
        }

    }
}
