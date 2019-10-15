using Prometheus.Client;
using Prometheus.Client.Abstractions;
using PrometheusEnumetric.Internal;
using System;

namespace PrometheusEnumetric
{
    public class EnumGauge<TName> : BaseEnuMetric<TName, Gauge, IGauge>
        where TName : Enum
    {
        public EnumGauge(string prefix, string suffix, string help, bool includeTimestamp, bool suppressEmptySamples, KeyValue[] const_labels, MetricFactory factory = null)
            : base(MetricHelper.CreateGaugeFactory(prefix, suffix, help, includeTimestamp, suppressEmptySamples, factory), const_labels)
        {
        }
    }

    public class EnumGauge<T1, TName> : BaseEnuMetric<T1,TName, Gauge, IGauge>
        where T1 : Enum where TName : Enum
    {
        public EnumGauge(string prefix, string suffix, string help, bool includeTimestamp, bool suppressEmptySamples, KeyValue[] const_labels, MetricFactory factory = null)
            : base(MetricHelper.CreateGaugeFactory(prefix, suffix, help, includeTimestamp, suppressEmptySamples, factory), const_labels)
        {
        }
    }

    public class EnumGauge<T1, T2, TName> : BaseEnuMetric<T1,T2,TName, Gauge, IGauge>
        where T1 : Enum where T2 : Enum where TName : Enum
    {
        public EnumGauge(string prefix, string suffix, string help, bool includeTimestamp, bool suppressEmptySamples, KeyValue[] const_labels, MetricFactory factory = null)
            : base(MetricHelper.CreateGaugeFactory(prefix, suffix, help, includeTimestamp, suppressEmptySamples, factory), const_labels)
        {
        }
    }

    public class EnumGauge<T1, T2, T3, TName> : BaseEnuMetric<T1, T2, T3, TName, Gauge, IGauge>
    where T1 : Enum where T2 : Enum where T3 : Enum where TName : Enum
    {
        public EnumGauge(string prefix, string suffix, string help, bool includeTimestamp, bool suppressEmptySamples, KeyValue[] const_labels, MetricFactory factory = null)
            : base(MetricHelper.CreateGaugeFactory(prefix, suffix, help, includeTimestamp, suppressEmptySamples, factory), const_labels)
        {
        }
    }

}
