using Microsoft.Xaml.Interactivity;
using Phronesis.Util;
using Windows.UI.Xaml;

namespace Phronesis.Actions.Extended
{
    public class NavigateBackAction : DependencyObject, IAction
    {
        public object Execute(object sender, object parameter)
        {
            return NavigationUtil.GoBack();
        }
    }
}
