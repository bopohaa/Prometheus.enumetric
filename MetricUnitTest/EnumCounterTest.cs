using System;
using Xunit;
using PrometheusEnumetric;
using Prometheus.Client;
using Prometheus.Client.Collectors;
using System.Linq;
using System.Reflection;
using System.ComponentModel;

namespace MetricUnitTest
{
    public class EnumCounterTest
    {
        enum MetricName
        {
            [DisplayName("foo")] Foo, [DisplayName("bar")] Bar, [DisplayName("baz")] Baz
        }

        [DisplayName("label_one")]
        enum MetricLabel
        {
            [DisplayName("value_one")] SomeValue1, [DisplayName("value_two")] SomeValue2
        }

        [Fact]
        public void simple_level1_wo_const_labels_test()
        {
            var factory = new MetricFactory(new CollectorRegistry());
            var counter = new EnumCounter<MetricName>("namespace_subsystem_app_", "_total", "test count metric", false, false, Array.Empty<KeyValue>(), factory);

            counter[MetricName.Foo].Inc(0.1);
            counter.Get(MetricName.Foo).Inc();
            counter.Get(MetricName.Bar).Inc();

            Assert.Equal(1.1D, counter[MetricName.Foo].Value);
            Assert.Equal(1, counter[MetricName.Bar].Value);
        }


        [Fact]
        public void simple_level1_const_labels_test()
        {
            var factory = new MetricFactory(new CollectorRegistry());
            var counter = new EnumCounter<MetricName>("namespace_subsystem_app_", "_total", "test count metric", false, false, new KeyValue[] { ("const_label", "const_label_value") }, factory);

            counter[MetricName.Foo].Inc();
            counter.Get(MetricName.Bar).Inc();

            Assert.Equal(1, counter[MetricName.Foo].Value);
            Assert.Equal(0, counter.Get(MetricName.Foo).Value);
            Assert.Equal(0, counter[MetricName.Bar].Value);
            Assert.Equal(1, counter.Get(MetricName.Bar).Value);
        }

        [Fact]
        public void simple_level1_labels_test()
        {
            var factory = new MetricFactory(new CollectorRegistry());
            var counter = new EnumCounter<MetricLabel, MetricName>("namespace_subsystem_app_", "_total", "test count metric", false, false, Array.Empty<KeyValue>(), factory);

            counter[MetricName.Foo].Inc();
            counter.Get(MetricLabel.SomeValue1, MetricName.Foo).Inc(0.1D);
            counter.Get(MetricLabel.SomeValue2, MetricName.Foo).Inc(0.2D);

            Assert.Equal(1, counter[MetricName.Foo].Value);
            Assert.Equal(0.1D, counter.Get(MetricLabel.SomeValue1, MetricName.Foo).Value);
            Assert.Equal(0.2D, counter.Get(MetricLabel.SomeValue2, MetricName.Foo).Value);
        }

        [Fact]
        public void simple_level1_scope_test()
        {
            var factory = new MetricFactory(new CollectorRegistry());
            var counter = new EnumCounter<MetricLabel, MetricName>("namespace_subsystem_app_", "_total", "test count metric", false, false, Array.Empty<KeyValue>(), factory);

            counter[MetricName.Foo].Inc();

            using (counter.CreateScope(MetricLabel.SomeValue1))
            {
                Assert.True(EnumCounter<MetricName>.TryGetScoped(MetricName.Foo, out var scoped));
                scoped.Inc(0.1D);
            }
            Assert.Equal(1, counter[MetricName.Foo].Value);
            Assert.Equal(0.1D, counter.Get(MetricLabel.SomeValue1, MetricName.Foo).Value);
        }



        [Fact]
        public void level1_const_labels_test()
        {
            var factory = new MetricFactory(new CollectorRegistry());
            var counter1 = new EnumCounter<Level1Enum>("namespace_subsystem_system_", "_total", "test count metric", false, false, new KeyValue[] { ("const_label1", "const_value1"), ("const_label2", "const_value2") }, factory);
            var counter2 = new EnumCounter<Level1Enum>("namespace_subsystem_system_", "_total", "test count metric", false, false, new KeyValue[] { ("const_label1", "const_value2"), ("const_label2", "const_value2") }, factory);

            counter1[Level1Enum.One1].Inc();
            counter1.Get(Level1Enum.One1).Inc();
            counter1.Get(Level1Enum.two1).Inc();
            counter1.Get(Level1Enum.two1).Inc();
            counter1.Get(Level1Enum.three1).Inc();
            counter1.Get(Level1Enum.three1).Inc();
            counter1.Get(Level1Enum.three1).Inc();

            counter2[Level1Enum.One1].Inc();
            counter2.Get(Level1Enum.One1).Inc();
            counter2.Get(Level1Enum.two1).Inc();
            counter2.Get(Level1Enum.two1).Inc();
            counter2.Get(Level1Enum.three1).Inc();
            counter2.Get(Level1Enum.three1).Inc();
            counter2.Get(Level1Enum.three1).Inc();

            Assert.Equal(2.0D, counter1[Level1Enum.One1].Value);
            Assert.Equal(1.0D, counter1.Get(Level1Enum.One1).Value);
            Assert.Equal(2.0D, counter1.Get(Level1Enum.two1).Value);
            Assert.Equal(3.0D, counter1.Get(Level1Enum.three1).Value);

            Assert.Equal(2.0D, counter2[Level1Enum.One1].Value);
            Assert.Equal(1.0D, counter2.Get(Level1Enum.One1).Value);
            Assert.Equal(2.0D, counter2.Get(Level1Enum.two1).Value);
            Assert.Equal(3.0D, counter2.Get(Level1Enum.three1).Value);
        }
        [Fact]
        public void level2_const_labels_test()
        {
            var factory = new MetricFactory(new CollectorRegistry());
            var counter1 = new EnumCounter<Level1Enum, Level2Enum>("namespace_subsystem_system_", "_total", "test count metric", false, false, new KeyValue[] { ("const_label1", "const_value1"), ("const_label2", "const_value2") }, factory);
            var counter2 = new EnumCounter<Level1Enum, Level2Enum>("namespace_subsystem_system_", "_total", "test count metric", false, false, new KeyValue[] { ("const_label1", "const_value2"), ("const_label2", "const_value2") }, factory);

            counter1[Level2Enum.one2].Inc();
            counter1.Get(Level1Enum.One1, Level2Enum.one2).Inc();
            counter1.Get(Level1Enum.two1, Level2Enum.two2).Inc();
            counter1.Get(Level1Enum.two1, Level2Enum.two2).Inc();
            counter1.Get(Level1Enum.three1, Level2Enum.three2).Inc();
            counter1.Get(Level1Enum.three1, Level2Enum.three2).Inc();
            counter1.Get(Level1Enum.three1, Level2Enum.three2).Inc();

            counter2[Level2Enum.one2].Inc();
            counter2.Get(Level1Enum.One1, Level2Enum.one2).Inc();
            counter2.Get(Level1Enum.two1, Level2Enum.two2).Inc();
            counter2.Get(Level1Enum.two1, Level2Enum.two2).Inc();
            counter2.Get(Level1Enum.three1, Level2Enum.three2).Inc();
            counter2.Get(Level1Enum.three1, Level2Enum.three2).Inc();
            counter2.Get(Level1Enum.three1, Level2Enum.three2).Inc();

            Assert.Equal(2.0D, counter1[Level2Enum.one2].Value);
            Assert.Equal(1.0D, counter1.Get(Level1Enum.One1, Level2Enum.one2).Value);
            Assert.Equal(2.0D, counter1.Get(Level1Enum.two1, Level2Enum.two2).Value);
            Assert.Equal(3.0D, counter1.Get(Level1Enum.three1, Level2Enum.three2).Value);

            Assert.Equal(2.0D, counter2[Level2Enum.one2].Value);
            Assert.Equal(1.0D, counter2.Get(Level1Enum.One1, Level2Enum.one2).Value);
            Assert.Equal(2.0D, counter2.Get(Level1Enum.two1, Level2Enum.two2).Value);
            Assert.Equal(3.0D, counter2.Get(Level1Enum.three1, Level2Enum.three2).Value);
        }

        [Fact]
        public void level3_const_labels_test()
        {
            var factory = new MetricFactory(new CollectorRegistry());
            var counter1 = new EnumCounter<Level1Enum, Level2Enum, Level3Enum>("namespace_subsystem_system_", "_total", "test count metric", false, false, new KeyValue[] { ("const_label1", "const_value1"), ("const_label2", "const_value2") }, factory);
            var counter2 = new EnumCounter<Level1Enum, Level2Enum, Level3Enum>("namespace_subsystem_system_", "_total", "test count metric", false, false, new KeyValue[] { ("const_label1", "const_value2"), ("const_label2", "const_value2") }, factory);

            counter1[Level3Enum.one3].Inc();
            counter1.Get(Level1Enum.One1, Level2Enum.one2, Level3Enum.one3).Inc();
            counter1.Get(Level1Enum.two1, Level2Enum.two2, Level3Enum.two3).Inc();
            counter1.Get(Level1Enum.two1, Level2Enum.two2, Level3Enum.two3).Inc();
            counter1.Get(Level1Enum.three1, Level2Enum.three2, Level3Enum.three3).Inc();
            counter1.Get(Level1Enum.three1, Level2Enum.three2, Level3Enum.three3).Inc();
            counter1.Get(Level1Enum.three1, Level2Enum.three2, Level3Enum.three3).Inc();

            counter2[Level3Enum.one3].Inc();
            counter2.Get(Level1Enum.One1, Level2Enum.one2, Level3Enum.one3).Inc();
            counter2.Get(Level1Enum.two1, Level2Enum.two2, Level3Enum.two3).Inc();
            counter2.Get(Level1Enum.two1, Level2Enum.two2, Level3Enum.two3).Inc();
            counter2.Get(Level1Enum.three1, Level2Enum.three2, Level3Enum.three3).Inc();
            counter2.Get(Level1Enum.three1, Level2Enum.three2, Level3Enum.three3).Inc();
            counter2.Get(Level1Enum.three1, Level2Enum.three2, Level3Enum.three3).Inc();

            Assert.Equal(2.0D, counter1[Level3Enum.one3].Value);
            Assert.Equal(1.0D, counter1.Get(Level1Enum.One1, Level2Enum.one2, Level3Enum.one3).Value);
            Assert.Equal(2.0D, counter1.Get(Level1Enum.two1, Level2Enum.two2, Level3Enum.two3).Value);
            Assert.Equal(3.0D, counter1.Get(Level1Enum.three1, Level2Enum.three2, Level3Enum.three3).Value);

            Assert.Equal(2.0D, counter2[Level3Enum.one3].Value);
            Assert.Equal(1.0D, counter2.Get(Level1Enum.One1, Level2Enum.one2, Level3Enum.one3).Value);
            Assert.Equal(2.0D, counter2.Get(Level1Enum.two1, Level2Enum.two2, Level3Enum.two3).Value);
            Assert.Equal(3.0D, counter2.Get(Level1Enum.three1, Level2Enum.three2, Level3Enum.three3).Value);
        }

        [Fact]
        public void level3_scope_test()
        {
            var factory = new MetricFactory(new CollectorRegistry());
            var counter1 = new EnumCounter<Level1Enum, Level2Enum, Level3Enum>("namespace_subsystem_system_", "_total", "test count metric", false, false, Array.Empty<KeyValue>(), factory);

            counter1[Level3Enum.one3].Inc();
            using (counter1.CreateScope(Level1Enum.One1))
            {
                Assert.True(EnumCounter<Level2Enum, Level3Enum>.TryGetScoped(Level2Enum.one2, Level3Enum.one3, out var subCounter));
                subCounter.Inc(0.1);
                Assert.False(EnumCounter<Level3Enum>.TryGetScoped(Level3Enum.one3, out var _));
            }
            Assert.False(EnumCounter<Level2Enum, Level3Enum>.TryGetScoped(Level2Enum.one2, Level3Enum.one3, out var _));
            using (counter1.CreateScope(Level1Enum.One1, Level2Enum.one2))
            {
                Assert.True(EnumCounter<Level3Enum>.TryGetScoped(Level3Enum.one3, out var subCounter));
                subCounter.Inc(0.1);
                Assert.False(EnumCounter<Level2Enum, Level3Enum>.TryGetScoped(Level2Enum.one2, Level3Enum.one3, out var _));
            }
            Assert.False(EnumCounter<Level3Enum>.TryGetScoped(Level3Enum.one3, out var _));

            counter1.Get(Level1Enum.One1, Level2Enum.one2, Level3Enum.one3).Inc();

            Assert.Equal(1.0D, counter1[Level3Enum.one3].Value);
            Assert.Equal(1.2D, counter1.Get(Level1Enum.One1, Level2Enum.one2, Level3Enum.one3).Value);
        }
    }
}
