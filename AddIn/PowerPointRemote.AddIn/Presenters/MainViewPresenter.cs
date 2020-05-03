using System;
using Autofac;
using PowerPointRemote.AddIn.Services;
using PowerPointRemote.AddIn.Views;

namespace PowerPointRemote.AddIn.Presenters
{
    public class MainViewPresenter : IStartable
    {
        private readonly IMainView _mainView;

        public MainViewPresenter(IMainView mainView, IChannelService channelService)
        {
            _mainView = mainView;

            channelService.ChannelStartRequest += ChannelModelOnChannelStartRequest;
            channelService.ChannelStartSuccess += ChannelModelOnChannelStartSuccess;
            channelService.ChannelStartFailure += ChannelModelOnChannelStartFailure;

            channelService.ChannelEndRequest += ChannelModelOnChannelEndRequest;
            channelService.ChannelEndSuccess += ChannelModelOnChannelEndSuccess;
        }

        public void Start()
        {
            _mainView.AddSpinnerView();
        }

        private void ChannelModelOnChannelEndSuccess(object sender, EventArgs e)
        {
            DisplayChannelEndedView();
        }

        private void ChannelModelOnChannelEndRequest(object sender, EventArgs e)
        {
            DisplaySpinnerView();
        }

        private void DisplayChannelView()
        {
            _mainView.InvokeSafe(() =>
            {
                _mainView.ClearViews();
                _mainView.AddChannelView();
            });
        }

        private void DisplayErrorView()
        {
            _mainView.InvokeSafe(() =>
            {
                _mainView.ClearViews();
                _mainView.AddErrorView();
            });
        }

        private void DisplaySpinnerView()
        {
            _mainView.InvokeSafe(() =>
            {
                _mainView.ClearViews();
                _mainView.AddSpinnerView();
            });
        }

        private void DisplayChannelEndedView()
        {
            _mainView.InvokeSafe(() =>
            {
                _mainView.ClearViews();
                _mainView.AddChannelEndedView();
            });
        }

        private void ChannelModelOnChannelStartFailure(object sender, string e)
        {
            DisplayErrorView();
        }

        private void ChannelModelOnChannelStartSuccess(object sender, Channel e)
        {
            DisplayChannelView();
        }

        private void ChannelModelOnChannelStartRequest(object sender, EventArgs e)
        {
            DisplaySpinnerView();
        }
    }
}