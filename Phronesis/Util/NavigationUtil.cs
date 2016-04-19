using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Phronesis.Util
{
    public static class NavigationUtil
    {
        public static bool GoBack()
        {
            var root = Window.Current.Content as Frame;
            if (root == null || !root.CanGoBack) return false;
            root.GoBack();
            return true;
        }
    }
}
