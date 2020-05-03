using System;
using PowerPointRemote.AddIn.Presenters;

namespace PowerPointRemote.AddIn.Views
{
    public interface IErrorView : IDisposable
    {
        ErrorViewPresenter Presenter { set; }
        string Message { set; }
        void InvokeSafe(Action action);
    }
}