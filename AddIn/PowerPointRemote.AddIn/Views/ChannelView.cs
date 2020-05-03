using System;
using System.Collections.Generic;
using System.Linq;
using PowerPointRemote.AddIn.Presenters;
using PowerPointRemote.AddIn.Services;

namespace PowerPointRemote.AddIn.Views
{
    public partial class ChannelView : BaseView, IChannelView
    {
        public ChannelView()
        {
            InitializeComponent();

            listBoxConnections.DisplayMember = nameof(User.Name);
        }

        public ChannelViewPresenter Presenter { private get; set; }

        public string ChannelId
        {
            set => channelId.Text = value;
        }

        public string ChannelUri
        {
            get => txtChannelUri.Text;
            set => txtChannelUri.Text = value;
        }

        public string StatusMessage
        {
            set => lblStatusValue.Text = value;
        }

        public string ClientAddress
        {
            set => txtClientAddress.Text = value;
        }

        public IEnumerable<User> UserConnections => listBoxConnections.Items.Cast<User>();

        public void AddChannelUser(User user)
        {
            listBoxConnections.Items.Add(user);
            AdjustUserConnectionListHeight();
            RefreshConnectionCount();
        }

        public void RemoveChannelUser(User user)
        {
            listBoxConnections.Items.Remove(user);
            AdjustUserConnectionListHeight();
            RefreshConnectionCount();
        }

        public void ClearChannelUserList()
        {
            listBoxConnections.Items.Clear();
            AdjustUserConnectionListHeight();
            RefreshConnectionCount();
        }

        private void AdjustUserConnectionListHeight()
        {
            listBoxConnections.Height = listBoxConnections.PreferredSize.Height;
        }

        private void RefreshConnectionCount()
        {
            lblRemoteConnectionsValue.Text = listBoxConnections.Items.Count.ToString();
        }

        private void UriCopyClick(object sender, EventArgs e)
        {
            Presenter.CopyUriToClipboard();
        }

        private void EndRemoteClick(object sender, EventArgs e)
        {
            Presenter.EndChannel();
        }
    }
}