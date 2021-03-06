﻿using System;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace Phronesis.Messaging.Core
{
    public sealed class MessageEventArgs : EventArgs
    {
        [NotNull]
        public TaskCompletionSource<MessageBase> CompletionSource { get; }

        [NotNull]
        public MessageBase Message { get; }

        public MessageEventArgs([NotNull] MessageBase message)
        {
            Message = message ?? throw new ArgumentNullException(nameof(message));
            CompletionSource = new TaskCompletionSource<MessageBase>();
        }
    }
}