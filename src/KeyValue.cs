using System;
using System.Collections.Generic;
using System.Text;

namespace PrometheusEnumetric
{
    public readonly struct KeyValue
    {
        public readonly string Key;
        public readonly string Value;

        public KeyValue(string key, string value)
        {
            Key = key;
            Value = value;
        }

        public static implicit operator KeyValue((string, string) value) => new KeyValue(value.Item1, value.Item2);
    }
}
