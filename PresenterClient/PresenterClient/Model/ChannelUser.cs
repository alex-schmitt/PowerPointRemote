using System;
using Prism.Mvvm;

namespace PresenterClient.Model
{
    public class ChannelUser : BindableBase
    {
        private bool _allowControl;
        private Guid _id;
        private string _name;

        public Guid Id
        {
            get => _id;
            set => SetProperty(ref _id, value);
        }

        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        public bool AllowControl
        {
            get => _allowControl;
            set => SetProperty(ref _allowControl, value);
        }
    }
}