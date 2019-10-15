using Prometheus.Client;
using Prometheus.Client.Abstractions;
using PrometheusEnumetric.Internal;
using System;

namespace PrometheusEnumetric
{
    public class EnumHistogram<TName> : BaseEnuMetric<TName, Histogram, IHistogram>
        where TName : Enum
    {
        public EnumHistogram(string prefix, string suffix, string help, bool includeTimestamp, bool suppressEmptySamples, double[] buckets, KeyValue[] const_labels, MetricFactory factory = null)
            : base(MetricHelper.CreateHistogramFactory(prefix, suffix, help, includeTimestamp, suppressEmptySamples, buckets, factory), const_labels)
        {
        }
    }

    public class EnumHistogram<T1, TName> : BaseEnuMetric<T1, TName, Histogram, IHistogram>
        where T1 : Enum where TName : Enum
    {
        public EnumHistogram(string prefix, string suffix, string help, bool includeTimestamp, bool suppressEmptySamples, double[] buckets, KeyValue[] const_labels, MetricFactory factory = null)
            : base(MetricHelper.CreateHistogramFactory(prefix, suffix, help, includeTimestamp, suppressEmptySamples, buckets, factory), const_labels)
        {
        }
    }

    public class EnumHistogram<T1, T2, TName> : BaseEnuMetric<T1, T2, TName, Histogram, IHistogram>
        where T1 : Enum where T2 : Enum where TName : Enum
    {
        public EnumHistogram(string prefix, string suffix, string help, bool includeTimestamp, bool suppressEmptySamples, double[] buckets, KeyValue[] const_labels, MetricFactory factory = null)
            : base(MetricHelper.CreateHistogramFactory(prefix, suffix, help, includeTimestamp, suppressEmptySamples, buckets, factory), const_labels)
        {
        }
    }

    public class EnumHistogram<T1, T2, T3, TName> : BaseEnuMetric<T1, T2, T3, TName, Histogram, IHistogram>
        where T1 : Enum where T2 : Enum where T3 : Enum where TName : Enum
    {
        public EnumHistogram(string prefix, string suffix, string help, bool includeTimestamp, bool suppressEmptySamples, double[] buckets, KeyValue[] const_labels, MetricFactory factory = null)
            : base(MetricHelper.CreateHistogramFactory(prefix, suffix, help, includeTimestamp, suppressEmptySamples, buckets, factory), const_labels)
        {
        }
    }
}
