using Prism.Mvvm;

namespace PresenterClient.ViewModels
{
    public class ConnectionDetailViewModel : BindableBase
    {
        private string _uri;

        public string Uri
        {
            get => _uri;
            set => SetProperty(ref _uri, value);
        }
    }
}