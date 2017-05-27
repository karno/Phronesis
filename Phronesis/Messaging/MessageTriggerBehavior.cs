using System;
using Windows.ApplicationModel;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Markup;
using Microsoft.Xaml.Interactivity;
using Phronesis.Messaging.Core;

namespace Phronesis.Messaging
{
    [ContentProperty(Name = "Actions")]
    public class MessageTriggerBehavior : DependencyObject, IBehavior
    {
        #region Dependency Properties and Change Handlers

        public static readonly DependencyProperty ActionsProperty = DependencyProperty.Register("Actions",
            typeof(ActionCollection), typeof(MessageTriggerBehavior), new PropertyMetadata(null));

        public static readonly DependencyProperty MessageKeyProperty = DependencyProperty.Register(
            "MessageKey", typeof(string), typeof(MessageTriggerBehavior), new PropertyMetadata(default(string)));

        public static readonly DependencyProperty MessengerProperty = DependencyProperty.Register("Messenger",
            typeof(Messenger), typeof(MessageTriggerBehavior), new PropertyMetadata(null, MessengerChanged));

        private static void MessengerChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var messenger = e.NewValue as Messenger;
            if (e.NewValue != null && messenger == null)
            {
                throw new InvalidOperationException(
                    "Bound object is not a Messenger in MessageTriggerProperty.Messenger.");
            }
            ((MessageTriggerBehavior)sender).MessengerChanged(messenger);
        }

        #endregion Dependency Properties and Change Handlers

        // this variable is NOT synchronized with Messenger property.
        private Messenger _boundMessenger;

        private IDisposable _messengerSubscription;

        public ActionCollection Actions
        {
            get
            {
                var actions = (ActionCollection)GetValue(ActionsProperty);
                if (actions == null)
                {
                    actions = new ActionCollection();
                    SetValue(ActionsProperty, actions);
                }
                return actions;
            }
        }

        public string MessageKey
        {
            get => (string)GetValue(MessageKeyProperty);
            set => SetValue(MessageKeyProperty, value);
        }

        public Messenger Messenger
        {
            get => (Messenger)GetValue(MessengerProperty);
            set => SetValue(MessengerProperty, value);
        }

        public DependencyObject AssociatedObject { get; private set; }

        public void Attach(DependencyObject associatedObject)
        {
            if (AssociatedObject == associatedObject || DesignMode.DesignModeEnabled)
            {
                return;
            }
            if (AssociatedObject != null)
            {
                throw new InvalidOperationException(
                    "Could not attach the behavior for multiple times.");
            }
            AssociatedObject = associatedObject;
        }

        public void Detach()
        {
            AssociatedObject = null;
        }

        private void MessengerChanged(Messenger sender)
        {
            if (sender == _boundMessenger) return;
            if (_boundMessenger != null)
            {
                _messengerSubscription.Dispose();
                _messengerSubscription = null;
            }
            _boundMessenger = sender;
            if (_boundMessenger != null)
            {
                _messengerSubscription = _boundMessenger.MessageRaiseEvent.RegisterHandler(MessengerOnRaiseMessage);
            }
        }

        private async void MessengerOnRaiseMessage(object sender, MessageEventArgs e)
        {
            var msg = e.Message;
            if (MessageKey != null && MessageKey != e.Message.Key)
            {
                return;
            }
            try
            {
                if (Dispatcher.HasThreadAccess)
                {
                    InvokeActions(msg);
                }
                else
                {
                    // queue on dispatcher
                    await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => InvokeActions(msg));
                }
                e.CompletionSource.TrySetResult(msg);
            }
            catch (Exception ex)
            {
                e.CompletionSource.TrySetException(ex);
            }
        }

        private void InvokeActions(MessageBase message)
        {
            var associated = AssociatedObject;
            if (associated != null)
            {
                Interaction.ExecuteActions(associated, Actions, message);
            }
        }
    }
}