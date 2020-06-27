using System;
using System.Windows;
using System.Windows.Input;

namespace PowerPointRemote.DesktopClient.Commands
{
    public class CopyTextCommand : ICommand
    {
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            Clipboard.SetText((string) parameter);
        }

        public event EventHandler CanExecuteChanged;
    }
}