using System;
using PowerPointRemote.AddIn.Presenters;

namespace PowerPointRemote.AddIn.Views
{
    public interface IChannelEndedView : IDisposable
    {
        ChannelEndedViewPresenter EndedViewPresenter { set; }
    }
}