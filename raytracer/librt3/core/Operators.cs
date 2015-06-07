using System;
using System.Linq.Expressions;

namespace librt3.core
{
    internal static class Operators
    {
        public static T Add<T>(T x, T y)
        {
            return OperatorCache<T>.Add(x, y);
        }

        public static T Multiply<T>(T x, T y)
        {
            return OperatorCache<T>.Multiply(x, y);
        }

        public static T Multiply<T, TU>(T x, TU y)
        {
            return OperatorCache<T, TU>.Multiply(x, y);
        }

        public static T Subtract<T>(T x, T y)
        {
            return OperatorCache<T>.Subtract(x, y);
        }

        private static class OperatorCache<T>
        {
            public static readonly Func<T, T, T> Multiply;
            public static readonly Func<T, T, T> Subtract;
            public static readonly Func<T, T, T> Add;

            static OperatorCache()
            {
                Multiply = MakeBinaryOperator(ExpressionType.Multiply);
                Subtract = MakeBinaryOperator(ExpressionType.Subtract);
                Add = MakeBinaryOperator(ExpressionType.Add);
            }

            private static Func<T, T, T> MakeBinaryOperator(ExpressionType type)
            {
                var x = Expression.Parameter(typeof (T), "x");
                var y = Expression.Parameter(typeof (T), "y");
                var body = Expression.MakeBinary(type, x, y);
                var expr = Expression.Lambda<Func<T, T, T>>(body, x, y);
                return expr.Compile();
            }
        }

        private static class OperatorCache<T, TU>
        {
            public static readonly Func<T, TU, T> Multiply;
            public static readonly Func<T, TU, T> Subtract;

            static OperatorCache()
            {
                Multiply = MakeBinaryOperator(ExpressionType.Multiply);
                Subtract = MakeBinaryOperator(ExpressionType.Subtract);
            }

            private static Func<T, TU, T> MakeBinaryOperator(ExpressionType type)
            {
                var x = Expression.Parameter(typeof (T), "x");
                var y = Expression.Parameter(typeof (TU), "y");
                var body = Expression.MakeBinary(type, x, y);
                var expr = Expression.Lambda<Func<T, TU, T>>(body, x, y);
                return expr.Compile();
            }
        }
    }
}