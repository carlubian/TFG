using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace TFG.UWP
{
    internal static class Navigation
    {
        internal static void GoBack(Page source)
        {
            var history = source.Frame.BackStack;
            for (int i = 0; i < history.Count(); i++)
            {
                if (history[i].SourcePageType.Equals(source.GetType()))
                    history.Remove(history[i]);
            }

            source.Frame.GoBack();
        }
    }
}
