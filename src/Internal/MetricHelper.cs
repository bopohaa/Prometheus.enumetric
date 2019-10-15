using Prometheus.Client;
using Prometheus.Client.Collectors;
using Prometheus.Client.SummaryImpl;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PrometheusEnumetric.Internal
{
    static class MetricHelper
    {
        public static (Func<string, string[], Counter>, Func<Counter, string[], Counter.LabelledCounter>) CreateCounterFactory(string prefix, string suffix, string help, bool includeTimestamp, bool suppressEmptySamples, MetricFactory factory = null) =>
            ((name, label_names) => factory.ThisOrDefault().CreateCounter(prefix + name + suffix, help, includeTimestamp, suppressEmptySamples, label_names),
             (metric, label_values) => metric.WithLabels(label_values));

        public static (Func<string, string[], CounterInt64>, Func<CounterInt64, string[], CounterInt64.LabelledCounter>) CreateCounterInt64Factory(string prefix, string suffix, string help, bool includeTimestamp, bool suppressEmptySamples, MetricFactory factory = null) =>
            ((name, label_names) => factory.ThisOrDefault().CreateCounterInt64(prefix + name + suffix, help, includeTimestamp, suppressEmptySamples, label_names),
             (metric, label_values) => metric.WithLabels(label_values));

        public static (Func<string, string[], Gauge>, Func<Gauge, string[], Gauge.LabelledGauge>) CreateGaugeFactory(string prefix, string suffix, string help, bool includeTimestamp, bool suppressEmptySamples, MetricFactory factory = null) =>
            ((name, label_names) => factory.ThisOrDefault().CreateGauge(prefix + name + suffix, help, includeTimestamp, suppressEmptySamples, label_names),
             (metric, label_values) => metric.WithLabels(label_values));

        public static (Func<string, string[], Histogram>, Func<Histogram, string[], Histogram.LabelledHistogram>) CreateHistogramFactory(string prefix, string suffix, string help, bool includeTimestamp, bool suppressEmptySamples, double[] buckets, MetricFactory factory = null) =>
            ((name, label_names) => factory.ThisOrDefault().CreateHistogram(prefix + name + suffix, help, includeTimestamp, suppressEmptySamples, buckets, label_names),
             (metric, label_values) => metric.WithLabels(label_values));

        public static (Func<string, string[], Summary>, Func<Summary, string[], Summary.LabelledSummary>) CreateSummaryFactory(string prefix, string suffix, string help, bool includeTimestamp, bool suppressEmptySamples, IReadOnlyList<QuantileEpsilonPair> objectives = null, TimeSpan? maxAge = null, int? ageBuckets = null, int? bufCap = null, MetricFactory factory = null) =>
            ((name, label_names) => factory.ThisOrDefault().CreateSummary(prefix + name + suffix, help, includeTimestamp, suppressEmptySamples, label_names, objectives, maxAge, ageBuckets, bufCap),
             (metric, label_values) => metric.WithLabels(label_values));

        public static (Func<string, string[], Untyped>, Func<Untyped, string[], Untyped.LabelledUntyped>) CreateUntypedFactory(string prefix, string suffix, string help, bool includeTimestamp, bool suppressEmptySamples, MetricFactory factory = null) =>
            ((name, label_names) => factory.ThisOrDefault().CreateUntyped(prefix + name + suffix, help, includeTimestamp, suppressEmptySamples, label_names),
             (metric, label_values) => metric.WithLabels(label_values));

        //public static Counter CreateCounter(this string[] label_names, string name, string help, bool includeTimestamp, bool suppressEmptySamples, MetricFactory factory = null) =>
        //    factory.ThisOrDefault().CreateCounter(name, help, includeTimestamp, suppressEmptySamples, label_names);

        //public static CounterInt64 CreateCounterInt64(this string[] label_names, string name, string help, bool includeTimestamp, bool suppressEmptySamples, MetricFactory factory = null) =>
        //    factory.ThisOrDefault().CreateCounterInt64(name, help, includeTimestamp, suppressEmptySamples, label_names);

        //public static Gauge CreateGauge(this string[] label_names, string name, string help, bool includeTimestamp, bool suppressEmptySamples, MetricFactory factory = null) =>
        //    factory.ThisOrDefault().CreateGauge(name, help, includeTimestamp, suppressEmptySamples, label_names);

        //public static Histogram CreateHistogram(this string[] label_names, string name, string help, bool includeTimestamp, bool suppressEmptySamples, double[] buckets, MetricFactory factory = null) =>
        //    factory.ThisOrDefault().CreateHistogram(name, help, includeTimestamp, suppressEmptySamples, buckets, label_names);

        //public static Summary CreateSummary(this string[] label_names, string name, string help, bool includeTimestamp, bool suppressEmptySamples, IReadOnlyList<QuantileEpsilonPair> objectives = null, TimeSpan? maxAge = null, int? ageBuckets = null, int? bufCap = null, MetricFactory factory = null) =>
        //    factory.ThisOrDefault().CreateSummary(name, help, includeTimestamp, suppressEmptySamples, label_names, objectives, maxAge, ageBuckets, bufCap);

        //public static Untyped CreateUntyped(this string[] label_names, string name, string help, bool includeTimestamp, bool suppressEmptySamples, MetricFactory factory = null) =>
        //    factory.ThisOrDefault().CreateUntyped(name, help, includeTimestamp, suppressEmptySamples, label_names);

        public static IEnumerable<string> ToValues(this (string, string)[] key_values) => key_values.Select(e => e.Item2);

        public static IEnumerable<string> ToValues(this KeyValue[] key_values) => key_values.Select(e => e.Value);
        public static IEnumerable<string> ToKeys(this KeyValue[] key_values) => key_values.Select(e => e.Key);

        private static MetricFactory ThisOrDefault(this MetricFactory factory) => factory ?? Metrics.DefaultFactory;
    }


}
