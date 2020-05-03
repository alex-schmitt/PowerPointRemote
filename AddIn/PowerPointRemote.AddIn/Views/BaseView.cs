using System;
using System.Windows.Forms;

namespace PowerPointRemote.AddIn.Views
{
    public class BaseView : UserControl
    {
        public void InvokeSafe(Action action)
        {
            if (InvokeRequired)
                Invoke(action);
            else
                action();
        }
    }
}