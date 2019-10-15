using Prometheus.Client;
using Prometheus.Client.Abstractions;
using PrometheusEnumetric.Internal;
using System;

namespace PrometheusEnumetric
{
    public class EnumCounter<TName> : BaseEnuMetric<TName, Counter, ICounter>
        where TName : Enum
    {
        public EnumCounter(string prefix, string suffix, string help, bool includeTimestamp, bool suppressEmptySamples, KeyValue[] const_labels, MetricFactory factory = null)
            : base(MetricHelper.CreateCounterFactory(prefix, suffix, help, includeTimestamp, suppressEmptySamples, factory), const_labels)
        {
        }
    }

    public class EnumCounter<T1, TName> : BaseEnuMetric<T1, TName, Counter, ICounter>
        where T1 : Enum where TName : Enum
    {
        public EnumCounter(string prefix, string suffix, string help, bool includeTimestamp, bool suppressEmptySamples, KeyValue[] const_labels, MetricFactory factory = null)
            : base(MetricHelper.CreateCounterFactory(prefix, suffix, help, includeTimestamp, suppressEmptySamples, factory), const_labels)
        {
        }
    }

    public class EnumCounter<T1, T2, TName> : BaseEnuMetric<T1, T2, TName, Counter, ICounter>
        where T1 : Enum where T2 : Enum where TName : Enum
    {
        public EnumCounter(string prefix, string suffix, string help, bool includeTimestamp, bool suppressEmptySamples, KeyValue[] const_labels, MetricFactory factory = null)
            : base(MetricHelper.CreateCounterFactory(prefix, suffix, help, includeTimestamp, suppressEmptySamples, factory), const_labels)
        {
        }
    }

    public class EnumCounter<T1, T2, T3, TName> : BaseEnuMetric<T1, T2, T3, TName, Counter, ICounter>
    where T1 : Enum where T2 : Enum where T3 : Enum where TName : Enum
    {
        public EnumCounter(string prefix, string suffix, string help, bool includeTimestamp, bool suppressEmptySamples, KeyValue[] const_labels, MetricFactory factory = null)
            : base(MetricHelper.CreateCounterFactory(prefix, suffix, help, includeTimestamp, suppressEmptySamples, factory), const_labels)
        {
        }
    }

}
