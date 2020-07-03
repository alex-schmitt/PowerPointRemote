using Prism.Mvvm;

namespace PresenterClient.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private string _title = "PowerPoint Remote";

        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }
    }
}