using System.Windows;
using Prism.Ioc;
using Prism.Regions;

namespace PresenterClient.Views
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly IContainerExtension _container;
        private readonly IRegionManager _regionManager;

        public MainWindow(IContainerExtension container, IRegionManager regionManager)
        {
            _container = container;
            _regionManager = regionManager;
            InitializeComponent();

            Loaded += OnLoaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            var region = _regionManager.Regions["MainRegion"];
            region.Add(_container.Resolve<ConnectionDetailView>());
        }
    }
}