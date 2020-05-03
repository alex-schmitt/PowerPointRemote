using System;

namespace PowerPointRemote.AddIn.Services
{
    public class TaskPaneCloseService
    {
        public event EventHandler TaskPaneClose;

        public void CloseTaskPane()
        {
            OnTaskPaneClose();
        }

        private void OnTaskPaneClose()
        {
            TaskPaneClose?.Invoke(this, EventArgs.Empty);
        }
    }
}