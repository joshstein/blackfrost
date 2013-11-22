using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Blackfrost.ViewModels;

namespace Blackfrost.Infrastructure.UI.Commands
{
    public class GoToPageCommand : DependencyObject, ICommand
    {
        public UserControl Page
        {
            get { return (UserControl)GetValue(PageProperty); }
            set { SetValue(PageProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Page.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PageProperty =
            DependencyProperty.Register("Page", typeof(UserControl), typeof(GoToPageCommand), new PropertyMetadata(null));

        

        #region ICommand
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }
        
        public void Execute(object parameter)
        {
            (parameter as MainWindowViewModel).GoToPage(Page);
        }
        #endregion
    }
}