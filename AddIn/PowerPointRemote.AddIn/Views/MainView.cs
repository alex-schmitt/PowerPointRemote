using System.Windows.Forms;

namespace PowerPointRemote.AddIn.Views
{
    public class MainView : BaseView, IMainView
    {
        private readonly UserControl _channelEndedView;
        private readonly UserControl _channelView;
        private readonly UserControl _errorView;
        private readonly UserControl _spinnerView;

        public MainView(IChannelView channelView, IErrorView errorView, ISpinnerView spinnerView,
            IChannelEndedView channelEndedView)
        {
            _channelView = (UserControl) channelView;
            _errorView = (UserControl) errorView;
            _spinnerView = (UserControl) spinnerView;
            _channelEndedView = (UserControl) channelEndedView;

            _channelView.Dock = DockStyle.Fill;
            _errorView.Dock = DockStyle.Fill;
            _spinnerView.Dock = DockStyle.Fill;
            _channelEndedView.Dock = DockStyle.Fill;

            AddSpinnerView();
        }

        public void ClearViews()
        {
            Controls.Clear();
        }

        public void AddChannelView()
        {
            Controls.Add(_channelView);
        }

        public void AddErrorView()
        {
            Controls.Add(_errorView);
        }

        public void AddSpinnerView()
        {
            Controls.Add(_spinnerView);
        }

        public void AddChannelEndedView()
        {
            Controls.Add(_channelEndedView);
        }
    }
}