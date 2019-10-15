using Prometheus.Client;
using Prometheus.Client.Abstractions;
using PrometheusEnumetric.Internal;
using System;

namespace PrometheusEnumetric
{

    public class EnumCounterInt64<TName> : BaseEnuMetric<TName, CounterInt64, ICounter<long>>
        where TName : Enum
    {
        public EnumCounterInt64(string prefix, string suffix, string help, bool includeTimestamp, bool suppressEmptySamples, KeyValue[] const_labels, MetricFactory factory = null)
            : base(MetricHelper.CreateCounterInt64Factory(prefix, suffix, help, includeTimestamp, suppressEmptySamples, factory), const_labels)
        {
        }
    }

    public class EnumCounterInt64<T1, TName> : BaseEnuMetric<T1, TName, CounterInt64, ICounter<long>>
        where T1 : Enum where TName : Enum
    {
        public EnumCounterInt64(string prefix, string suffix, string help, bool includeTimestamp, bool suppressEmptySamples, KeyValue[] const_labels, MetricFactory factory = null)
            : base(MetricHelper.CreateCounterInt64Factory(prefix, suffix, help, includeTimestamp, suppressEmptySamples, factory), const_labels)
        {
        }
    }

    public class EnumCounterInt64<T1, T2, TName> : BaseEnuMetric<T1, T2, TName, CounterInt64, ICounter<long>>
        where T1 : Enum where T2 : Enum where TName : Enum
    {
        public EnumCounterInt64(string prefix, string suffix, string help, bool includeTimestamp, bool suppressEmptySamples, KeyValue[] const_labels, MetricFactory factory = null)
            : base(MetricHelper.CreateCounterInt64Factory(prefix, suffix, help, includeTimestamp, suppressEmptySamples, factory), const_labels)
        {
        }
    }

    public class EnumCounterInt64<T1, T2, T3, TName> : BaseEnuMetric<T1, T2, T3, TName, CounterInt64, ICounter<long>>
        where T1 : Enum where T2 : Enum where T3 : Enum where TName : Enum
    {
        public EnumCounterInt64(string prefix, string suffix, string help, bool includeTimestamp, bool suppressEmptySamples, KeyValue[] const_labels, MetricFactory factory = null)
            : base(MetricHelper.CreateCounterInt64Factory(prefix, suffix, help, includeTimestamp, suppressEmptySamples, factory), const_labels)
        {
        }
    }
}
