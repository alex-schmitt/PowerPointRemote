using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows;
using PowerPointRemote.DesktopClient.ViewModels;
using PowerPointApplication = Microsoft.Office.Interop.PowerPoint.Application;

namespace PowerPointRemote.DesktopClient
{
    public partial class App : Application
    {
        private readonly MainWindow _mainWindow;
        private PowerPointApplication _powerPointApplication;

        public App()
        {
            _mainWindow = new MainWindow();
            _mainWindow.Show();
        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            await LoadAsync();
        }

        private async Task LoadAsync()
        {
            var channelService = new ChannelService();
            _mainWindow.DataContext = new MainWindowViewModel(channelService);

            _powerPointApplication = await CreateApplicationAsync();
            await channelService.StartChannel();

            var slideShowManager = new SlideShowManager(_powerPointApplication);

            if (slideShowManager.SlideShowDetail.SlideShowEnabled)
                await channelService.SendSlideShowDetail(slideShowManager.SlideShowDetail);

            // Link ShowSlideManager and ChannelService via events
            slideShowManager.OnSlideShowDetailChange += async (sender, slideShowDetail) =>
                await channelService.SendSlideShowDetail(slideShowDetail);
            channelService.SlideShowCommandReceived += (sender, command) => slideShowManager.ProcessCommand(command);
        }

        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);
            Marshal.ReleaseComObject(_powerPointApplication);
        }

        private async Task<PowerPointApplication> CreateApplicationAsync()
        {
            return await Task.Run(() => new PowerPointApplication());
        }
    }
}