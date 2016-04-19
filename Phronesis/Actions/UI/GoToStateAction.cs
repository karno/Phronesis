using System;
using Microsoft.Xaml.Interactivity;
using Phronesis.Messaging.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Phronesis.Actions.UI
{
    public class GoToStateAction : MessageActionBase<GoToStateMessage>
    {
        protected override bool Execute(DependencyObject associatedObject, GoToStateMessage message)
        {
            try
            {
                message.CompletionSource.TrySetResult(
                    VisualStateUtilities.GoToState((Control)associatedObject, message.StateName, message.UseTransitions));
            }
            catch (Exception ex)
            {
                message.CompletionSource.TrySetException(ex);
            }
            return true;
        }
    }
}
