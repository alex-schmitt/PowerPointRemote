using Autofac;
using PowerPointRemote.AddIn.Presenters;
using PowerPointRemote.AddIn.Services;
using PowerPointRemote.AddIn.Views;

namespace PowerPointRemote.AddIn
{
    public class Bootstrapper
    {
        public IContainer Bootstrap()
        {
            var builder = new ContainerBuilder();

            RegisterServices(builder);
            RegisterPresenters(builder);
            RegisterViews(builder);

            return builder.Build();
        }

        private static void RegisterServices(ContainerBuilder builder)
        {
            builder.RegisterType<TaskPaneCloseService>().AsSelf().SingleInstance();
            builder.RegisterType<ChannelService>().As<IChannelService>().SingleInstance();

            builder.Register(c =>
            {
                var slideShowWindows = Globals.ThisAddIn.Application.SlideShowWindows;
                return new SlideShowControlService(slideShowWindows);
            }).As<ISlideShowControlService>();
        }

        private static void RegisterViews(ContainerBuilder builder)
        {
            builder.RegisterType<MainView>().As<IMainView>().SingleInstance();
            builder.RegisterType<SpinnerView>().As<ISpinnerView>().SingleInstance();
            builder.RegisterType<ChannelView>().As<IChannelView>().SingleInstance();
            builder.RegisterType<ErrorView>().As<IErrorView>().SingleInstance();
            builder.RegisterType<ChannelEndedView>().As<IChannelEndedView>().SingleInstance();
        }

        private static void RegisterPresenters(ContainerBuilder builder)
        {
            builder.RegisterType<MainViewPresenter>().AutoActivate();
            builder.RegisterType<ErrorViewPresenter>().AutoActivate();
            builder.RegisterType<ChannelViewPresenter>().AutoActivate();
            builder.RegisterType<ChannelEndedViewPresenter>().AutoActivate();
        }
    }
}