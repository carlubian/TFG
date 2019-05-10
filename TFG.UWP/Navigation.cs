using System.Linq;
using Windows.UI.Xaml.Controls;

namespace TFG.UWP
{
    internal static class Navigation
    {
        internal static void GoBack(Page source)
        {
            var history = source.Frame.BackStack;
            for (var i = 0; i < history.Count(); i++)
            {
                if (history[i].SourcePageType.Equals(source.GetType()))
                    history.Remove(history[i]);
            }

            source.Frame.GoBack();
        }
    }
}
