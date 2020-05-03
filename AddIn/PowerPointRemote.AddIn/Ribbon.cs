using System;
using System.Windows.Forms;
using Autofac;
using Microsoft.Office.Core;
using Microsoft.Office.Tools.Ribbon;
using PowerPointRemote.AddIn.Services;
using PowerPointRemote.AddIn.Views;
using CustomTaskPane = Microsoft.Office.Tools.CustomTaskPane;

namespace PowerPointRemote.AddIn
{
    public partial class Ribbon
    {
        private IContainer _container;
        private CustomTaskPane _customTaskPane;
        private bool _initialized;

        private void Ribbon_Load(object sender, RibbonUIEventArgs e)
        {
        }

        private void BtnOpen_Click(object sender, RibbonControlEventArgs e)
        {
            if (!_initialized)
                Initialize();
            else
                _customTaskPane.Visible = true;
        }

        private void Initialize()
        {
            _initialized = true;

            _container = new Bootstrapper().Bootstrap();
            _container.Resolve<TaskPaneCloseService>().TaskPaneClose += OnTaskPaneClose;

            var mainView = _container.Resolve<IMainView>();
            var channelService = _container.Resolve<IChannelService>();
            var slideShowControlService = _container.Resolve<ISlideShowControlService>();

            StartSlideShowControlListeners(channelService, slideShowControlService);

            _customTaskPane = Globals.ThisAddIn.CustomTaskPanes.Add((UserControl) mainView, "Slide Show Remote");
            _customTaskPane.DockPositionRestrict = MsoCTPDockPositionRestrict.msoCTPDockPositionRestrictNoHorizontal;

            // Start in the floating position to set that positions default size.
            _customTaskPane.DockPosition = MsoCTPDockPosition.msoCTPDockPositionFloating;
            _customTaskPane.Width = 450;
            _customTaskPane.Height = 950;

            _customTaskPane.DockPosition = MsoCTPDockPosition.msoCTPDockPositionRight;
            _customTaskPane.Width = 450;

            _customTaskPane.Visible = true;
        }

        private void StartSlideShowControlListeners(IChannelService channelService,
            ISlideShowControlService slideShowControlService)
        {
            channelService.SlideShowCommandReceived += (sender, command) =>
            {
                slideShowControlService.InvokeSlideShowCommand(command);
            };
        }

        private void OnTaskPaneClose(object sender, EventArgs e)
        {
            _customTaskPane.Dispose();
            _container.Dispose();
            _initialized = false;
        }
    }
}