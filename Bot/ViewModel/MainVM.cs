using System;
using System.ComponentModel;
using Bot.ViewModel.Helpers;
using Xamarin.Forms;

namespace Bot.ViewModel
{
    public class MainVM : INotifyPropertyChanged
    {
        BotServiceHelper botHelper;

        public Command SendCommand { get; set; }


        private string messages;

        public event PropertyChangedEventHandler PropertyChanged;
        public string Messages 
        { 
            get { return messages; }
            set 
            {
                messages = value;
                OnPropertyChanged("Messages");

            }
        }

        public MainVM()
        {
            botHelper = new BotServiceHelper();
            SendCommand = new Command(SubmitActivity);

        }

        void SubmitActivity() 
        {
            botHelper.SubmitActivity(Messages);
        }

        private void OnPropertyChanged(string propertyName) 
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
