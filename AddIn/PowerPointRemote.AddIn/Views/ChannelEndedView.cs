using System;
using PowerPointRemote.AddIn.Presenters;

namespace PowerPointRemote.AddIn.Views
{
    public partial class ChannelEndedView : BaseView, IChannelEndedView
    {
        public ChannelEndedView()
        {
            InitializeComponent();
        }

        public ChannelEndedViewPresenter EndedViewPresenter { private get; set; }

        private void OnNewRemoteClick(object sender, EventArgs e)
        {
            EndedViewPresenter.CreateNewChannel();
        }

        private void OnCloseAddInClick(object sender, EventArgs e)
        {
            EndedViewPresenter.CloseAddIn();
        }
    }
}