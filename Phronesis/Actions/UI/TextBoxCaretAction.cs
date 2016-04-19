using Windows.UI.Xaml.Controls;
using Phronesis.Messaging.UI;

namespace Phronesis.Actions.UI
{
    public class TextBoxCaretAction : MessageActionBase<TextBoxCaretMessage, TextBox>
    {
        protected override bool Execute(TextBox associatedObject, TextBoxCaretMessage message)
        {
            try
            {
                if (message.SelectionStart != null)
                {
                    associatedObject.SelectionStart = message.SelectionStart.Value;
                }
                if (message.SelectionLength != null)
                {
                    associatedObject.SelectionLength = message.SelectionLength.Value;
                }
                if (message.SelectionReplacingText != null)
                {
                    associatedObject.SelectedText = message.SelectionReplacingText;
                }
            }
            catch
            {
                // ignored
            }
            return true;
        }
    }
}
