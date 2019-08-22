using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Netsoft.Wpf.Samples
{
    public interface IShowWindowService<TViewModel>
    {
        void Show(TViewModel context);
    }

    public class ShowWindowService<TWindow, TViewModel> : IShowWindowService<TViewModel>
        where TWindow : Window, new()
    {
        public Window Owner { get; set; }

        public void Show(TViewModel context)
        {
            var dlg = new TWindow()
            {
                Owner = Owner,
                DataContext = context,
            };

            dlg.Show();
        }
    }
}
