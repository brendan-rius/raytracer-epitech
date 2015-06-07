using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace librt3.core
{
    static class Operators
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

        static class OperatorCache<T>
        {
            static OperatorCache()
            {
                Multiply = MakeBinaryOperator(type: ExpressionType.Multiply);
                Subtract = MakeBinaryOperator(type: ExpressionType.Subtract);
                Add = MakeBinaryOperator(type: ExpressionType.Add);
            }

            static Func<T, T, T> MakeBinaryOperator(ExpressionType type)
            {
                var x = Expression.Parameter(typeof(T), "x");
                var y = Expression.Parameter(typeof(T), "y");
                var body = Expression.MakeBinary(type, x, y);
                var expr = Expression.Lambda<Func<T, T, T>>(body, x, y);
                return expr.Compile();
            }

            public readonly static Func<T, T, T> Multiply;

            public readonly static Func<T, T, T> Subtract;

            public static readonly Func<T, T, T> Add;
        }

        static class OperatorCache<T, TU>
        {
            static OperatorCache()
            {
                Multiply = MakeBinaryOperator(type: ExpressionType.Multiply);
                Subtract = MakeBinaryOperator(type: ExpressionType.Subtract);
            }

            static Func<T, TU, T> MakeBinaryOperator(ExpressionType type)
            {
                var x = Expression.Parameter(typeof(T), "x");
                var y = Expression.Parameter(typeof(TU), "y");
                var body = Expression.MakeBinary(type, x, y);
                var expr = Expression.Lambda<Func<T, TU, T>>(body, x, y);
                return expr.Compile();
            }

            public readonly static Func<T, TU, T> Multiply;

            public readonly static Func<T, TU, T> Subtract;
        }
    }
}
