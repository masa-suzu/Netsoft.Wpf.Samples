using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Netsoft.Wpf.Samples
{
    public class Commit // INotifyPropertyChanged must be implemented in production code.
    {
        private readonly string _message;

        public Commit(string message, IShowWindowService<SubWindowViewModel> allMessage)
        {
            _message = message;
            Trimmed = message?.Length > 25
                ? System.Windows.Visibility.Visible
                : System.Windows.Visibility.Collapsed;

            ShowMessage = new RelayCommand(() => allMessage.Show(new SubWindowViewModel() { Text = _message }));
        }

        public string Message
        {
            get
            {
                if (_message?.Length > 25)
                {
                    return _message.Substring(0, 25) + "...";
                }
                return _message;
            }
        }

        public System.Windows.Visibility Trimmed { get; }
        public ICommand ShowMessage { get; set; }
    }

    public class SubWindowViewModel // INotifyPropertyChanged must be implemented in production code.
    {
        public string Text { get; set; }
    }

    public class RelayCommand : ICommand
    {
        private readonly  Action _action;
        private bool _running;

        public RelayCommand(Action action)
        {
            _action = action;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return !_running;
        }

        public void Execute(object parameter)
        {
            if (_running)
            {
                return;
            }

            _running = true;
            CanExecuteChanged(parameter, new EventArgs());
            try
            {
                _action?.Invoke();
            }
            finally
            {
                _running = false;
                CanExecuteChanged(parameter, new EventArgs());
            }
        }
    }
}
