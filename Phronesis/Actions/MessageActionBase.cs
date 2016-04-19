using Microsoft.Xaml.Interactivity;
using Phronesis.Messaging;
using Windows.UI.Xaml;

namespace Phronesis.Actions
{
    public abstract class MessageActionBase : DependencyObject, IAction
    {
        public object Execute(object sender, object parameter)
        {
            var associatedObject = sender as DependencyObject;
            var message = parameter as MessageBase;
            if (associatedObject == null || message == null)
            {
                return false;
            }
            return Execute(associatedObject, message);
        }

        protected abstract bool Execute(DependencyObject associatedObject, MessageBase message);
    }

    public abstract class MessageActionBase<T> : MessageActionBase where T : MessageBase
    {
        protected sealed override bool Execute(DependencyObject associatedObject, MessageBase message)
        {
            var typedMessage = message as T;
            return typedMessage != null && Execute(associatedObject, typedMessage);
        }

        protected abstract bool Execute(DependencyObject associatedObject, T message);
    }

    public abstract class MessageActionBase<TMessage, TDependencyObject> : MessageActionBase
        where TMessage : MessageBase where TDependencyObject : DependencyObject
    {
        protected sealed override bool Execute(DependencyObject associatedObject, MessageBase message)
        {
            var typedMessage = message as TMessage;
            var typedobject = associatedObject as TDependencyObject;
            return typedMessage != null && typedobject != null && Execute(typedobject, typedMessage);
        }

        protected abstract bool Execute(TDependencyObject associatedObject, TMessage message);
    }
}
