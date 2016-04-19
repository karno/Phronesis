namespace Phronesis.Util
{
    internal static class DesignTimeUtil
    {
        internal static bool IsInDesignTime()
        {
            return Windows.ApplicationModel.DesignMode.DesignModeEnabled;
        }
    }
}
