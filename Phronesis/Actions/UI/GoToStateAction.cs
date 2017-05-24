using System;
using Microsoft.Xaml.Interactivity;
using Phronesis.Messaging.UI;
using Windows.UI.Xaml.Controls;

namespace Phronesis.Actions.UI
{
    public class GoToStateAction : MessageActionBase<GoToStateMessage, Control>
    {
        protected override bool Execute(Control associatedObject, GoToStateMessage message)
        {
            try
            {
                message.CompletionSource.TrySetResult(
                    VisualStateUtilities.GoToState(associatedObject,
                    message.StateName, message.UseTransitions));
            }
            catch (Exception ex)
            {
                message.CompletionSource.TrySetException(ex);
            }
            return true;
        }
    }
}
