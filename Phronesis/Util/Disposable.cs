using System;
using JetBrains.Annotations;

namespace Phronesis.Util
{
    public static class Disposable
    {
        public static IDisposable Create(Action disposeAction)
        {
            return new AnonymousDisposable(disposeAction);
        }

        private sealed class AnonymousDisposable : IDisposable
        {
            private bool _disposed;
            private readonly Action _disposer;

            public AnonymousDisposable([NotNull] Action disposer)
            {
                if (disposer == null) throw new ArgumentNullException(nameof(disposer));
                _disposer = disposer;
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
