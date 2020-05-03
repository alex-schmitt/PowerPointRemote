using PowerPointRemote.AddIn.Services;
using PowerPointRemote.AddIn.Views;

namespace PowerPointRemote.AddIn.Presenters
{
    public class ChannelEndedViewPresenter
    {
        private readonly IChannelService _channelService;
        private readonly TaskPaneCloseService _taskPaneCloseService;

        public ChannelEndedViewPresenter(IChannelEndedView channelEndedView, IChannelService channelService,
            TaskPaneCloseService taskPaneCloseService)
        {
            _channelService = channelService;
            _taskPaneCloseService = taskPaneCloseService;
            channelEndedView.EndedViewPresenter = this;
        }

        public async void CreateNewChannel()
        {
            await _channelService.StartChannel();
        }

        public void CloseAddIn()
        {
            _taskPaneCloseService.CloseTaskPane();
        }
    }
}