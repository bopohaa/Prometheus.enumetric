# Summary
Enum-based Prometheus metric factory.
This library helps to create and receive prometheus metrics by enum values.

# Features
* Enums can be defined as part of a metric name and/or up to three labels.
* The mechanism for getting and creating is combined into one method (thread-safe).
* The process of getting metrics by enum values is optimized for taking values by index from an array of already created metrics, so there is no need to store the received metrics separately from the general container.
* You can create scopes for a partially defined metric for transfer to another application layer
* Const labeling
* Customizing enum name and values with DisplayNameAttribute

# Usage

## Metric container without labels
```C#
var counter = new EnumCounter<MetricName>("namespace_subsystem_app_", "_total", "test count metric", false, false, Array.Empty<KeyValue>());

// Increment metric without labels
counter[MetricName.Foo].Inc(0.1);
// Increment a labeled metric, but we did not specify a dynamic or constant label in the definition or constructor of the container, this method will return a metric without labels 
counter.Get(MetricName.Foo).Inc();
counter.Get(MetricName.Bar).Inc();

Assert.Equal(1.1D, counter[MetricName.Foo].Value);
Assert.Equal(1, counter[MetricName.Bar].Value);
```

## Metric container with const labels
```C#
var counter = new EnumCounter<MetricName>("namespace_subsystem_app_", "_total", "test count metric", false, false, new KeyValue[] { ("const_label", "const_label_value") });

// Increment metric without labels
counter[MetricName.Foo].Inc();
// Increment metric with labels
counter.Get(MetricName.Bar).Inc();

Assert.Equal(1, counter[MetricName.Foo].Value);
Assert.Equal(0, counter.Get(MetricName.Foo).Value);
Assert.Equal(0, counter[MetricName.Bar].Value);
Assert.Equal(1, counter.Get(MetricName.Bar).Value);
```

## Dynamic label metrics container
```C#
var counter = new EnumCounter<MetricLabel, MetricName>("namespace_subsystem_app_", "_total", "test count metric", false, false, Array.Empty<KeyValue>());

counter[MetricName.Foo].Inc();
counter.Get(MetricLabel.SomeValue1, MetricName.Foo).Inc(0.1D);
counter.Get(MetricLabel.SomeValue2, MetricName.Foo).Inc(0.2D);

Assert.Equal(1, counter[MetricName.Foo].Value);
Assert.Equal(0.1D, counter.Get(MetricLabel.SomeValue1, MetricName.Foo).Value);
Assert.Equal(0.2D, counter.Get(MetricLabel.SomeValue2, MetricName.Foo).Value);
```

## Scope
```C#
var counter = new EnumCounter<MetricLabel, MetricName>("namespace_subsystem_app_", "_total", "test count metric", false, false, Array.Empty<KeyValue>(), factory);

using (counter.CreateScope(MetricLabel.SomeValue1))
{
    Assert.True(EnumCounter<MetricName>.TryGetScoped(MetricName.Foo, out var scoped));
    scoped.Inc(0.1D);
}
Assert.Equal(0.1D, counter.Get(MetricLabel.SomeValue1, MetricName.Foo).Value);
```
