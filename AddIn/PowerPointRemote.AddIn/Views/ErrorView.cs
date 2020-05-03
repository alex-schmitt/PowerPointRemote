using System;
using PowerPointRemote.AddIn.Presenters;

namespace PowerPointRemote.AddIn.Views
{
    public partial class ErrorView : BaseView, IErrorView
    {
        public ErrorView()
        {
            InitializeComponent();
        }

        public ErrorViewPresenter Presenter { private get; set; }

        public string Message
        {
            set => lblMessage.Text = value;
        }

        private void OnCloseClick(object sender, EventArgs e)
        {
            Presenter.Close();
        }

        private void OnRetryClick(object sender, EventArgs e)
        {
            Presenter.RetryConnection();
        }
    }
}