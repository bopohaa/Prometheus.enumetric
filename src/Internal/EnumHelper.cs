using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace PrometheusEnumetric.Internal
{
    static class EnumHelper
    {
        public static string[] GetLabelNames<T1>(IReadOnlyList<KeyValue> const_labels)
            where T1 : Enum
        {
            return const_labels.Select(e => e.Key)
                .Concat(Enumerable.Repeat(GetLabelName<T1>(), 1)).ToArray();
            //.Distinct(const_labels.Count + 1);
        }
        public static string[] GetLabelNames<T1, T2>(IReadOnlyList<KeyValue> const_labels)
            where T1 : Enum
            where T2 : Enum
        {
            return const_labels.Select(e => e.Key)
                .Concat(Enumerable.Repeat(GetLabelName<T1>(), 1))
                .Concat(Enumerable.Repeat(GetLabelName<T2>(), 1)).ToArray();
            //.Distinct(const_labels.Count + 2);
        }
        public static string[] GetLabelNames<T1, T2, T3>(IReadOnlyList<KeyValue> const_labels)
            where T1 : Enum
            where T2 : Enum
            where T3 : Enum
        {
            return const_labels.Select(e => e.Key)
                .Concat(Enumerable.Repeat(GetLabelName<T1>(), 1))
                .Concat(Enumerable.Repeat(GetLabelName<T2>(), 1))
                .Concat(Enumerable.Repeat(GetLabelName<T3>(), 1)).ToArray();
            //.Distinct(const_labels.Count + 3);
        }

        //private static string[] Distinct(this IEnumerable<string> values, int expected)
        //{
        //    var res = values.Distinct().ToArray();
        //    if (res.Length < expected)
        //        throw new NotSupportedException("Found duplicates in label names");
        //    return res;
        //}


        public static string GetLabelName<T1>()
            where T1 : Enum
        {
            return typeof(T1).GetCustomAttribute<DisplayNameAttribute>()?.DisplayName ?? typeof(T1).Name;
        }

        public static string[] GetLabelValues<T1>()
            where T1 : Enum
        {
            return typeof(T1).GetFields(BindingFlags.Static | BindingFlags.Public).Select(m => m.GetCustomAttribute<DisplayNameAttribute>()?.DisplayName ?? m.Name).ToArray();
        }

        public static int GetMaxValue<T>() where T : Enum => Enum.GetValues(typeof(T)).Cast<int>().Max();

        public static int ToInt<S>(S s)
        {
            return Cache<int, S>.caster(s);
        }

        private static class Cache<T, S>
        {
            public static readonly Func<S, T> caster = Get();

            private static Func<S, T> Get()
            {
                var p = Expression.Parameter(typeof(S));
                var c = Expression.Convert(p, typeof(T));
                return Expression.Lambda<Func<S, T>>(c, p).Compile();
            }
        }
    }
}
