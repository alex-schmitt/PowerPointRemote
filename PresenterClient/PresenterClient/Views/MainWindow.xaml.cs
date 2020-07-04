using System.ComponentModel;
using System.Windows;
using PresenterClient.Services;
using Prism.Regions;

namespace PresenterClient.Views
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly ChannelControlsView _channelControlsView;
        private readonly ChannelUsersView _channelUsersView;
        private readonly ConnectionDetailView _connectionDetailView;
        private readonly IPowerPointService _powerPointService;
        private readonly IRegionManager _regionManager;
        private readonly ISignalRService _signalRService;

        public MainWindow(ConnectionDetailView connectionDetailView,
            ChannelUsersView channelUsersView,
            ChannelControlsView channelControlsView,
            IRegionManager regionManager,
            ISignalRService signalRService,
            IPowerPointService powerPointService)
        {
            _connectionDetailView = connectionDetailView;
            _channelUsersView = channelUsersView;
            _channelControlsView = channelControlsView;
            _regionManager = regionManager;
            _signalRService = signalRService;
            _powerPointService = powerPointService;

            InitializeComponent();

            Loaded += OnLoaded;
            Closing += OnClosing;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            _regionManager.Regions["MainRegion"].Add(_connectionDetailView);
            _regionManager.Regions["MainRegion"].Add(_channelUsersView);
            _regionManager.Regions["ControlsRegion"].Add(_channelControlsView);
        }

        private async void OnClosing(object sender, CancelEventArgs e)
        {
            Hide();
            _powerPointService.Dispose();

            if (!_signalRService.IsStarted) return;

            // Prevents deadlock 
            e.Cancel = true;
            await _signalRService.DisposeAsync();
            Close();
        }
    }
}