using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.AspNetCore.SignalR.Client;
using PowerPointRemote.DesktopClient.ViewModels;
using PowerPointApplication = Microsoft.Office.Interop.PowerPoint.Application;

namespace PowerPointRemote.DesktopClient
{
    public partial class App : Application
    {
        private readonly MainWindow _mainWindow;
        private ChannelService _channelService;
        private PowerPointApplication _powerPointApplication;

        public App()
        {
            _mainWindow = new MainWindow();
            _mainWindow.Closing += MainWindowOnClosing;
            _mainWindow.Show();
        }

        private async void MainWindowOnClosing(object sender, CancelEventArgs e)
        {
            _mainWindow.Hide();

            if (_channelService == null)
                return;

            if (_channelService.State == HubConnectionState.Disconnected)
                return;

            e.Cancel = true;
            await _channelService.EndChannel();
            _mainWindow.Close();
        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            await LoadAsync();
        }

        private async Task LoadAsync()
        {
            _channelService = new ChannelService();
            _mainWindow.DataContext = new MainWindowViewModel(_channelService);

            _powerPointApplication = await CreateApplicationAsync();
            await _channelService.StartChannel();

            var slideShowManager = new SlideShowManager(_powerPointApplication);

            if (slideShowManager.SlideShowDetail.SlideShowEnabled)
                await _channelService.UpdateSlideShowDetail(slideShowManager.SlideShowDetail);

            // Link ShowSlideManager and ChannelService via events
            slideShowManager.OnSlideShowDetailChange += async (sender, slideShowDetail) =>
                await _channelService.UpdateSlideShowDetail(slideShowDetail);
            _channelService.SlideShowCommandReceived += (sender, command) => slideShowManager.ProcessCommand(command);
        }

        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);
            Marshal.ReleaseComObject(_powerPointApplication);
            _channelService?.Dispose();
        }

        private async Task<PowerPointApplication> CreateApplicationAsync()
        {
            return await Task.Run(() => new PowerPointApplication());
        }
    }
}