using System;
using System.Linq;
using System.Text;
using System.Windows;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Office.Core;
using Microsoft.Office.Interop.PowerPoint;
using PresenterClient.NoteConverter;
using PresenterClient.Services;
using PresenterClient.SignalR;
using PresenterClient.SignalR.Messages;
using PresenterClient.Views;
using Prism.Ioc;
using Shape = Microsoft.Office.Interop.PowerPoint.Shape;

namespace PresenterClient
{
    /// <summary>
    ///     Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        private readonly IPowerPointService _powerPointService;
        private readonly ISignalRService _signalRService;

        public App()
        {
            _powerPointService = new PowerPointService();
            _signalRService = new SignalRService();

            // The initial start of the signalR service.  Start/stop will later be invoked by UI control only.
            _signalRService.StartAsync().ConfigureAwait(false);

            ConnectServices();
        }

        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterInstance(_powerPointService);
            containerRegistry.RegisterInstance(_signalRService);
        }

        private void ConnectServices()
        {
            _signalRService.Started += SignalRServiceOnStarted;
        }

        private void ProcessSlideShowAction(SlideShowActionMsg slideShowActionMsg)
        {
            var wn = _powerPointService.ActiveSlideShowWindow;

            if (wn == null)
                return;

            switch (slideShowActionMsg.Code)
            {
                case 0:
                    wn.View.Next();
                    break;
                case 1:
                    wn.View.Previous();
                    break;
            }
        }

        private async void OnActiveSlideShowWindowChanged(object sender, SlideShowWindow e)
        {
            if (_signalRService.HubConnection?.State != HubConnectionState.Connected)
                return;

            await _signalRService.HubConnection.SendSetSlideShowDetailAsync(
                CreateSlideShowDetail(_powerPointService.ActiveSlideShowWindow));
        }

        private async void SignalRServiceOnStarted(object sender, EventArgs e)
        {
            _powerPointService.ActiveSlideShowWindowChanged += OnActiveSlideShowWindowChanged;
            _signalRService.HubConnection.OnSlideShowActionReceived(ProcessSlideShowAction);

            // Send the initial SlideShowDetailMsg if available
            if (_powerPointService.ActiveSlideShowWindow != null)
                await _signalRService.HubConnection.SendSetSlideShowDetailAsync(
                    CreateSlideShowDetail(_powerPointService.ActiveSlideShowWindow));
        }

        private static SlideShowDetailMsg CreateSlideShowDetail(SlideShowWindow slideShowWindow)
        {
            return new SlideShowDetailMsg
            {
                Name = slideShowWindow?.Presentation.Name ?? "",
                SlideShowEnabled = slideShowWindow != null,
                CurrentSlide = slideShowWindow?.View.CurrentShowPosition ?? 0,
                TotalSlides = slideShowWindow?.Presentation.Slides.Count ?? 0,
                CurrentSlideNotes =
                    GetSlideNotes(slideShowWindow?.Presentation.Slides[slideShowWindow.View.CurrentShowPosition]),
                Timestamp = DateTime.Now
            };
        }

        private static string GetSlideNotes(Slide slide)
        {
            if (slide == null || slide.HasNotesPage == MsoTriState.msoFalse)
                return null;

            var bodyRange = GetSlideNoteBodyRange(slide);

            if (bodyRange == null)
                return null;

            var noteBuilder = new NoteTreeBuilder();
            var tree = noteBuilder.Build(bodyRange);
            var stringBuilder = new StringBuilder();
            tree.WriteHtml(stringBuilder);
            return stringBuilder.ToString();
        }

        private static TextRange GetSlideNoteBodyRange(Slide slide)
        {
            // There are two placeholders in a notes page, the one is the note body and the other is the slide image.
            var shape = slide.NotesPage.Shapes.Placeholders.Cast<Shape>()
                .FirstOrDefault(s => s.TextFrame.HasText == MsoTriState.msoTrue);

            return shape?.TextFrame.TextRange;
        }

        public static Alignment ConvertAlignment(PpParagraphAlignment ppAlignment)
        {
            switch (ppAlignment)
            {
                case PpParagraphAlignment.ppAlignLeft:
                    return Alignment.Left;
                case PpParagraphAlignment.ppAlignCenter:
                    return Alignment.Center;
                case PpParagraphAlignment.ppAlignRight:
                    return Alignment.Right;
                case PpParagraphAlignment.ppAlignJustify:
                    return Alignment.Justify;
                default:
                    return Alignment.Left;
            }
        }

        public static List? ConvertList(PpBulletType ppBullet)
        {
            if (ppBullet == PpBulletType.ppBulletUnnumbered)
                return List.Bullet;
            if (ppBullet == PpBulletType.ppBulletNumbered)
                return List.Ordered;

            return null;
        }
    }
}