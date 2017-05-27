using System;
using JetBrains.Annotations;

namespace Phronesis.Util
{
    public static class Disposable
    {
        public static IDisposable Create([NotNull] Action disposeAction)
        {
            if (disposeAction == null) throw new ArgumentNullException(nameof(disposeAction));
            return new AnonymousDisposable(disposeAction);
        }

        private sealed class AnonymousDisposable : IDisposable
        {
            private bool _disposed;
            private readonly Action _disposer;

            public AnonymousDisposable([NotNull] Action disposer)
            {
                _disposer = disposer ?? throw new ArgumentNullException(nameof(disposer));
            }

            public void Dispose()
            {
                if (!_disposed)
                {
                    _disposed = true;
                    _disposer();
                }
            }
        }
    }
}