using PowerPointRemote.AddIn.Services;
using PowerPointRemote.AddIn.Views;

namespace PowerPointRemote.AddIn.Presenters
{
    public class ErrorViewPresenter
    {
        private readonly IChannelService _channelService;
        private readonly TaskPaneCloseService _taskPaneCloseService;
        private readonly IErrorView _view;

        public ErrorViewPresenter(IErrorView view, IChannelService channelService,
            TaskPaneCloseService taskPaneCloseService)
        {
            _view = view;
            _view.Presenter = this;

            _taskPaneCloseService = taskPaneCloseService;

            _channelService = channelService;
            channelService.ChannelStartFailure += ChannelModelOnChannelStartFailure;
        }

        private void ChannelModelOnChannelStartFailure(object sender, string message)
        {
            _view.InvokeSafe(() => _view.Message = message);
        }

        public async void RetryConnection()
        {
            await _channelService.StartChannel();
        }

        public void Close()
        {
            _taskPaneCloseService.CloseTaskPane();
        }
    }
}