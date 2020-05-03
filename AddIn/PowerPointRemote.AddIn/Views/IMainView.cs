using System;

namespace PowerPointRemote.AddIn.Views
{
    public interface IMainView : IDisposable
    {
        void ClearViews();
        void AddChannelView();
        void AddErrorView();
        void AddSpinnerView();
        void AddChannelEndedView();
        void InvokeSafe(Action action);
    }
}