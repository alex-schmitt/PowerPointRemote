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
        private readonly ChannelView _channelView;
        private readonly IPowerPointService _powerPointService;
        private readonly IRegionManager _regionManager;
        private readonly ISignalRService _signalRService;

        public MainWindow(ChannelView channelView, IRegionManager regionManager, ISignalRService signalRService,
            IPowerPointService powerPointService)
        {
            _channelView = channelView;
            _regionManager = regionManager;
            _signalRService = signalRService;
            _powerPointService = powerPointService;

            InitializeComponent();

            Loaded += OnLoaded;
            Closing += OnClosing;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            _regionManager.Regions["MainRegion"].Add(_channelView);
        }

        private async void OnClosing(object sender, CancelEventArgs e)
        {
            Hide();
            _powerPointService.Dispose();

            if (_signalRService.IsDisposed) return;

            // Prevents deadlock 
            e.Cancel = true;
            await _signalRService.DisposeAsync();
            Close();
        }
    }
}