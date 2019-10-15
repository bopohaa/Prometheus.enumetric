using Prometheus.Client;
using Prometheus.Client.Abstractions;
using Prometheus.Client.SummaryImpl;
using PrometheusEnumetric.Internal;
using System;
using System.Collections.Generic;

namespace PrometheusEnumetric
{
    public class EnumSummary<TName> : BaseEnuMetric<TName, Summary, ISummary>
        where TName : Enum
    {
        public EnumSummary(string prefix, string suffix, string help, bool includeTimestamp, bool suppressEmptySamples, KeyValue[] const_labels, IReadOnlyList<QuantileEpsilonPair> objectives = null, TimeSpan? maxAge = null, int? ageBuckets = null, int? bufCap = null, MetricFactory factory = null)
            : base(MetricHelper.CreateSummaryFactory(prefix, suffix, help, includeTimestamp, suppressEmptySamples, objectives, maxAge, ageBuckets, bufCap, factory), const_labels)
        {
        }
    }

    public class EnumSummary<T1, TName> : BaseEnuMetric<T1, TName, Summary, ISummary>
        where T1 : Enum where TName : Enum
    {
        public EnumSummary(string prefix, string suffix, string help, bool includeTimestamp, bool suppressEmptySamples, KeyValue[] const_labels, IReadOnlyList<QuantileEpsilonPair> objectives = null, TimeSpan? maxAge = null, int? ageBuckets = null, int? bufCap = null, MetricFactory factory = null)
            : base(MetricHelper.CreateSummaryFactory(prefix, suffix, help, includeTimestamp, suppressEmptySamples, objectives, maxAge, ageBuckets, bufCap, factory), const_labels)
        {
        }
    }

    public class EnumSummary<T1, T2, TName> : BaseEnuMetric<T1, T2, TName, Summary, ISummary>
        where T1 : Enum where T2 : Enum where TName : Enum
    {
        public EnumSummary(string prefix, string suffix, string help, bool includeTimestamp, bool suppressEmptySamples, KeyValue[] const_labels, IReadOnlyList<QuantileEpsilonPair> objectives = null, TimeSpan? maxAge = null, int? ageBuckets = null, int? bufCap = null, MetricFactory factory = null)
            : base(MetricHelper.CreateSummaryFactory(prefix, suffix, help, includeTimestamp, suppressEmptySamples, objectives, maxAge, ageBuckets, bufCap, factory), const_labels)
        {
        }
    }

    public class EnumSummary<T1, T2, T3, TName> : BaseEnuMetric<T1, T2, T3, TName, Summary, ISummary>
        where T1 : Enum where T2 : Enum where T3 : Enum where TName : Enum
    {
        public EnumSummary(string prefix, string suffix, string help, bool includeTimestamp, bool suppressEmptySamples, KeyValue[] const_labels, IReadOnlyList<QuantileEpsilonPair> objectives = null, TimeSpan? maxAge = null, int? ageBuckets = null, int? bufCap = null, MetricFactory factory = null)
            : base(MetricHelper.CreateSummaryFactory(prefix, suffix, help, includeTimestamp, suppressEmptySamples, objectives, maxAge, ageBuckets, bufCap, factory), const_labels)
        {
        }
    }
}
