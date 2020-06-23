using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows;
using PowerPointRemote.DesktopClient.ViewModels;

namespace PowerPointRemote.DesktopClient
{
    public partial class App : Application
    {
        private readonly MainWindow _mainWindow;
        private Microsoft.Office.Interop.PowerPoint.Application _application;

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
            await CreateApplicationAsync();
            var slideShowManager = new SlideShowManager(_application);
            var channelService = new ChannelService();

            var mainWindowViewModel = new MainWindowViewModel(channelService, slideShowManager);
            _mainWindow.DataContext = mainWindowViewModel;

            await mainWindowViewModel.LoadAsync();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);
            Marshal.ReleaseComObject(_application);
        }

        private async Task CreateApplicationAsync()
        {
            await Task.Run(() => _application = new Microsoft.Office.Interop.PowerPoint.Application());
        }
    }
}