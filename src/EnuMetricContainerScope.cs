using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace PrometheusEnumetric
{
    public struct EnuMetricContainerScope<T> : IDisposable
        where T:class
    {
        private readonly AsyncLocal<T> _scope;
        private readonly T _parent;

        internal EnuMetricContainerScope(AsyncLocal<T> scope, T value)
        {
            _scope = scope;
            _parent = scope.Value;
            scope.Value = value;
        }

        public void Dispose()
        {
            _scope.Value = _parent;
        }
    }
}
