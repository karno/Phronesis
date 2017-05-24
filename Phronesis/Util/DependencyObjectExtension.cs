using System;
using JetBrains.Annotations;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;

namespace Phronesis.Util
{
    public static class DependencyObjectExtension
    {
        public static T FindVisualChild<T>([NotNull] this DependencyObject obj) where T : DependencyObject
        {
            if (obj == null) throw new ArgumentNullException(nameof(obj));
            var cnum = VisualTreeHelper.GetChildrenCount(obj);
            for (var i = 0; i < cnum; i++)
            {
                var child = VisualTreeHelper.GetChild(obj, i);
                var cTyped = child as T;
                if (cTyped != null)
                {
                    return cTyped;
                }
                var descendant = FindVisualChild<T>(child);
                if (descendant != null)
                {
                    return descendant;
                }
            }
            return null;
        }
    }
}
