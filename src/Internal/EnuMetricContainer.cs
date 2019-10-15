using Prometheus.Client;
using Prometheus.Client.Collectors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace PrometheusEnumetric.Internal
{
    public class EnuMetricContainer<TName, TChild>
    where TName : Enum
    {
        private readonly TChild[] _metrics;
        private readonly string[] _parentLabelValues;
        private readonly Func<string[], TName, TChild> _factory;
        private readonly static AsyncLocal<EnuMetricContainer<TName, TChild>> _scope = new AsyncLocal<EnuMetricContainer<TName, TChild>>();

        internal EnuMetricContainer(IEnumerable<string> perent_label_values, Func<string[], TName, TChild> factory)
        {
            _parentLabelValues = perent_label_values.ToArray();
            _factory = factory;
            _metrics = new TChild[EnumHelper.GetMaxValue<TName>() + 1];
        }

        internal TChild Get(TName name)
        {
            var idx = EnumHelper.ToInt(name);
            var metric = _metrics[idx];

            if (metric != null)
                return metric;

            metric = _factory(_parentLabelValues, name);
            _metrics[idx] = metric;

            return metric;
        }

        internal EnuMetricContainerScope<EnuMetricContainer<TName, TChild>> CreateScope() => new EnuMetricContainerScope<EnuMetricContainer<TName, TChild>>(_scope, this);

        internal static bool TryGetScoped(TName value, out TChild result)
        {
            var target = _scope.Value;
            if (target == null)
            {
                result = default;
                return false;
            }
            result = target.Get(value);
            return true;
        }
    }

    public abstract class BaseEnuMetricContainer<T1, TChild>
        where T1 : Enum
    {
        private readonly TChild[] _metrics;
        private readonly string[] _parentLabelValues;
        private readonly string[] _labelValues;
        private readonly Func<string[], TChild> _factory;
        private readonly static AsyncLocal<BaseEnuMetricContainer<T1, TChild>> _scope = new AsyncLocal<BaseEnuMetricContainer<T1, TChild>>();

        internal BaseEnuMetricContainer(IEnumerable<string> parent_label_values, Func<string[], TChild> factory)
        {
            _parentLabelValues = parent_label_values.ToArray();
            _labelValues = EnumHelper.GetLabelValues<T1>();
            _metrics = new TChild[_labelValues.Length];
            _factory = factory;
        }

        internal TChild Get(T1 label1)
        {
            var idx = EnumHelper.ToInt(label1);
            var metric = _metrics[idx];

            if (metric == null)
            {
                var parentLabelValues = new string[_parentLabelValues.Length + 1];
                Array.Copy(_parentLabelValues, parentLabelValues, _parentLabelValues.Length);
                parentLabelValues[_parentLabelValues.Length] = _labelValues[idx];

                metric = _factory(parentLabelValues);
                _metrics[idx] = metric;
            }

            return metric;
        }

        internal EnuMetricContainerScope<BaseEnuMetricContainer<T1, TChild>> CreateScope() => new EnuMetricContainerScope<BaseEnuMetricContainer<T1, TChild>>(_scope, this);

        internal static bool TryGetScoped(T1 label1, out TChild value)
        {
            var target = _scope.Value;
            if (target == null)
            {
                value = default;
                return false;
            }
            value = target.Get(label1);
            return true;
        }
    }

    public class EnuMetricContainer<T1, TName, TChild> : BaseEnuMetricContainer<T1, EnuMetricContainer<TName, TChild>>
    where T1 : Enum where TName : Enum
    {
        internal EnuMetricContainer(IEnumerable<string> parent_label_values, Func<string[], TName, TChild> factory)
            : base(parent_label_values, values => new EnuMetricContainer<TName, TChild>(values, factory))
        {
        }
    }

    public class EnuMetricContainer<T1, T2, TName, TChild> : BaseEnuMetricContainer<T1, EnuMetricContainer<T2, TName, TChild>>
        where T1 : Enum where T2 : Enum where TName : Enum
    {
        internal EnuMetricContainer(IEnumerable<string> parent_label_values, Func<string[], TName, TChild> factory)
            : base(parent_label_values, values => new EnuMetricContainer<T2, TName, TChild>(values, factory))
        {
        }
    }

    public class EnuMetricContainer<T1, T2, T3, TName, TChild> : BaseEnuMetricContainer<T1, EnuMetricContainer<T2, T3, TName, TChild>>
    where T1 : Enum where T2 : Enum where T3 : Enum where TName : Enum
    {
        internal EnuMetricContainer(IEnumerable<string> parent_label_values, Func<string[], TName, TChild> factory)
            : base(parent_label_values, values => new EnuMetricContainer<T2, T3, TName, TChild>(values, factory))
        {
        }
    }
}
