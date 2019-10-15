using Prometheus.Client;
using Prometheus.Client.Collectors;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PrometheusEnumetric.Internal
{
    public abstract class BaseMetric<TName, T, TChild>
        where T : TChild
        where TName : Enum
    {
        private readonly Func<string, string[], T> _valueFactory;
        private readonly string[] _names;
        private readonly string[] _labelNames;
        private readonly T[] _metrics;

        protected Func<string[], TName, TChild> ChildFactory { get; }

        internal BaseMetric((Func<string, string[], T>, Func<T, string[], TChild>) factories, IEnumerable<string> label_names)
        {
            _valueFactory = factories.Item1;
            _labelNames = label_names.ToArray();
            _names = EnumHelper.GetLabelValues<TName>();
            _metrics = new T[_names.Length];

            ChildFactory = _labelNames.Length > 0 ? (values, name) => factories.Item2(this[name], values) :
                (Func<string[], TName, TChild>)((_, name) => this[name]);
        }

        public T this[TName name]
        {
            get
            {
                var idx = EnumHelper.ToInt(name);
                var metric = _metrics[idx];
                if (metric == null)
                {
                    metric = _valueFactory(_names[idx], _labelNames);
                    _metrics[idx] = metric;
                }
                return metric;
            }
        }
    }

    public abstract class BaseEnuMetric<TName, T, TChild> : BaseMetric<TName, T, TChild>
        where T : TChild
        where TName : Enum
    {
        private readonly EnuMetricContainer<TName, TChild> _metrics;

        internal BaseEnuMetric((Func<string, string[], T>, Func<T, string[], TChild>) metric_factory, KeyValue[] const_labels)
            : base(metric_factory, const_labels.ToKeys())
        {
            _metrics = new EnuMetricContainer<TName, TChild>(const_labels.ToValues(), ChildFactory);
        }

        public TChild Get(TName name) => _metrics.Get(name);

        public static bool TryGetScoped(TName name, out TChild value) => EnuMetricContainer<TName, TChild>.TryGetScoped(name, out value);
    }

    public abstract class BaseEnuMetric<T1, TName, T, TChild> : BaseMetric<TName, T, TChild>
        where T : TChild
        where T1 : Enum where TName : Enum
    {
        private readonly EnuMetricContainer<T1, TName, TChild> _metrics;

        internal BaseEnuMetric((Func<string, string[], T>, Func<T, string[], TChild>) metric_factory, KeyValue[] const_labels)
            : base(metric_factory, EnumHelper.GetLabelNames<T1>(const_labels))
        {
            _metrics = new EnuMetricContainer<T1, TName, TChild>(const_labels.ToValues(), ChildFactory);
        }

        public TChild Get(T1 label1, TName name) => _metrics.Get(label1).Get(name);

        public static bool TryGetScoped(T1 label1, TName name, out TChild value) => (value = EnuMetricContainer<T1, TName, TChild>.TryGetScoped(label1, out var c) ? c.Get(name) : default) != default;

        public EnuMetricContainerScope<EnuMetricContainer<TName, TChild>> CreateScope(T1 label1) => _metrics.Get(label1).CreateScope();
    }


    public abstract class BaseEnuMetric<T1, T2, TName, T, TChild> : BaseMetric<TName, T, TChild>
        where T : TChild
        where T1 : Enum where T2 : Enum where TName : Enum
    {
        private readonly EnuMetricContainer<T1, T2, TName, TChild> _metrics;

        internal BaseEnuMetric((Func<string, string[], T>, Func<T, string[], TChild>) metric_factory, KeyValue[] const_labels)
            : base(metric_factory, EnumHelper.GetLabelNames<T1, T2>(const_labels))
        {
            _metrics = new EnuMetricContainer<T1, T2, TName, TChild>(const_labels.ToValues(), ChildFactory);
        }

        public TChild Get(T1 label1, T2 label2, TName name) => _metrics.Get(label1).Get(label2).Get(name);

        public static bool TryGetScoped(T1 label1, T2 label2, TName name, out TChild value) => (value = EnuMetricContainer<T1, T2, TName, TChild>.TryGetScoped(label1, out var c) ? c.Get(label2).Get(name) : default) != default;

        public EnuMetricContainerScope<BaseEnuMetricContainer<T2, EnuMetricContainer<TName, TChild>>> CreateScope(T1 label1) => _metrics.Get(label1).CreateScope();
        public EnuMetricContainerScope<EnuMetricContainer<TName, TChild>> CreateScope(T1 label1, T2 label2) => _metrics.Get(label1).Get(label2).CreateScope();
    }


    public abstract class BaseEnuMetric<T1, T2, T3, TName, T, TChild> : BaseMetric<TName, T, TChild>
        where T : TChild
        where T1 : Enum where T2 : Enum where T3 : Enum where TName : Enum
    {
        private readonly EnuMetricContainer<T1, T2, T3, TName, TChild> _metrics;

        internal BaseEnuMetric((Func<string, string[], T>, Func<T, string[], TChild>) metric_factory, KeyValue[] const_labels)
            : base(metric_factory, EnumHelper.GetLabelNames<T1, T2, T3>(const_labels))
        {
            _metrics = new EnuMetricContainer<T1, T2, T3, TName, TChild>(const_labels.ToValues(), ChildFactory);
        }

        public TChild Get(T1 label1, T2 label2, T3 label3, TName name) => _metrics.Get(label1).Get(label2).Get(label3).Get(name);

        public static bool TryGetScoped(T1 label1, T2 label2, T3 label3, TName name, out TChild value) =>
            (value = EnuMetricContainer<T1, T2, T3, TName, TChild>.TryGetScoped(label1, out var c) ? c.Get(label2).Get(label3).Get(name) : default) != default;

        public EnuMetricContainerScope<BaseEnuMetricContainer<T2, EnuMetricContainer<T3, TName, TChild>>> CreateScope(T1 label1) => _metrics.Get(label1).CreateScope();

        public EnuMetricContainerScope<BaseEnuMetricContainer<T3, EnuMetricContainer<TName, TChild>>> CreateScope(T1 label1, T2 label2) => _metrics.Get(label1).Get(label2).CreateScope();

        public EnuMetricContainerScope<EnuMetricContainer<TName, TChild>> CreateScope(T1 label1, T2 label2, T3 label3) => _metrics.Get(label1).Get(label2).Get(label3).CreateScope();
    }

}
