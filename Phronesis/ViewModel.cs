using System;
using System.Collections.Generic;
using Phronesis.Messaging.Core;

namespace Phronesis
{
    public abstract class ViewModel : NotificationObject, IDisposable
    {
        private bool _disposed;

        private readonly List<IDisposable> _disposables;

        public Messenger Messenger { get; } = new Messenger();

        public ViewModel()
        {
            _disposables = new List<IDisposable> { Messenger };
        }

        public void AddDisposable(IDisposable disposable)
        {
            lock (_disposables)
            {
                _disposables.Add(disposable);
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~ViewModel()
        {
            Dispose(false);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) return;
            if (disposing)
            {
                lock (_disposables)
                {
                    foreach (var disposable in _disposables)
                    {
                        disposable.Dispose();
                    }
                }
            }
            _disposed = true;
        }
    }
}