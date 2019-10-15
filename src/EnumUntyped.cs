using Prometheus.Client;
using Prometheus.Client.Abstractions;
using PrometheusEnumetric.Internal;
using System;

namespace PrometheusEnumetric
{
    public class EnumUntyped<TName> : BaseEnuMetric<TName, Untyped, IUntyped>
        where TName : Enum
    {
        public EnumUntyped(string prefix, string suffix, string help, bool includeTimestamp, bool suppressEmptySamples, KeyValue[] const_labels, MetricFactory factory = null)
            : base(MetricHelper.CreateUntypedFactory(prefix, suffix, help, includeTimestamp, suppressEmptySamples, factory), const_labels)
        {
        }
    }

    public class EnumUntyped<T1, TName> : BaseEnuMetric<T1, TName, Untyped, IUntyped>
        where T1 : Enum where TName : Enum
    {
        public EnumUntyped(string prefix, string suffix, string help, bool includeTimestamp, bool suppressEmptySamples, KeyValue[] const_labels, MetricFactory factory = null)
            : base(MetricHelper.CreateUntypedFactory(prefix, suffix, help, includeTimestamp, suppressEmptySamples, factory), const_labels)
        {
        }
    }

    public class EnumUntyped<T1, T2, TName> : BaseEnuMetric<T1, T2, TName, Untyped, IUntyped>
        where T1 : Enum where T2 : Enum where TName : Enum
    {
        public EnumUntyped(string prefix, string suffix, string help, bool includeTimestamp, bool suppressEmptySamples, KeyValue[] const_labels, MetricFactory factory = null)
            : base(MetricHelper.CreateUntypedFactory(prefix, suffix, help, includeTimestamp, suppressEmptySamples, factory), const_labels)
        {
        }
    }

    public class EnumUntyped<T1, T2, T3, TName> : BaseEnuMetric<T1, T2, T3, TName, Untyped, IUntyped>
    where T1 : Enum where T2 : Enum where T3 : Enum where TName : Enum
    {
        public EnumUntyped(string prefix, string suffix, string help, bool includeTimestamp, bool suppressEmptySamples, KeyValue[] const_labels, MetricFactory factory = null)
            : base(MetricHelper.CreateUntypedFactory(prefix, suffix, help, includeTimestamp, suppressEmptySamples, factory), const_labels)
        {
        }
    }

}
