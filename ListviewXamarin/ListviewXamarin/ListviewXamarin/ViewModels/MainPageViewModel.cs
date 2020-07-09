using Syncfusion.ListView.XForms;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace ListviewXamarin
{
    class MainPageViewModel : INotifyPropertyChanged
    {

        private ObservableCollection<Message> messagesList;
        internal Syncfusion.ListView.XForms.SfListView ListView;
        public bool indicatorIsVisible;
        public bool gridIsVisible;
        public ObservableCollection<Message> Messages
        {
            get { return messagesList; }
            set { messagesList = value; }
        }

        public bool IndicatorIsVisible
        {
            get { return indicatorIsVisible; }
            set
            {
                indicatorIsVisible = value;
                OnPropertyChanged("IndicatorIsVisible");
            }
        }

        public bool GridIsVisible
        {
            get { return gridIsVisible; }
            set
            {
                gridIsVisible = value;
                OnPropertyChanged("GridIsVisible");
            }
        }

        private string newText;

        public string NewText
        {
            get { return newText; }
            set
            {
                newText = value;
                OnPropertyChanged("NewText");
            }
        }

        private ImageSource sendIcon;

        public ImageSource SendIcon
        {
            get
            { return sendIcon; }
            set
            { sendIcon = value; }
        }
        private string outgoingText;

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public ICommand SendCommand { get; set; }

        public string OutGoingText
        {
            get { return outgoingText; }
            set { outgoingText = value; }
        }

        public MainPageViewModel()
        {
            InitializeSendCommand();
            // Initialize with default values
            var r = new Random();
            Messages = new ObservableCollection<Message>();
            for (int i = 0; i < 10; i++)
            {
                var collection = new Message();
                collection.Text = MessageText[r.Next(0, MessageText.Count() - 1)];
                collection.IsIncoming = i % 2 == 0 ? true : false;
                collection.MessagDateTime = DateTime.Now.ToString();
                Messages.Add(collection);
            }
            GridIsVisible = true;
        }
        private void InitializeSendCommand()
        {
            NewText = "";
            SendCommand = new Command(() =>
            {
                if (!string.IsNullOrWhiteSpace(NewText))
                {
                    Messages.Add(new Message
                    {
                        Text = NewText,
                        IsIncoming = true,
                        MessagDateTime = DateTime.Now.ToString()
                    });
                    (ListView.LayoutManager as LinearLayout).ScrollToRowIndex(Messages.Count - 1, true);
                }
                NewText = null;
            });
        }

        public string[] MessageText = new string[]
        {
            "Hi Squirrel! \uD83D\uDE0A",
            "Hi Baboon, How are you? \uD83D\uDE0A",
            "We've a party at Mandrill's. Would you like to join? We would love to have you there! \uD83D\uDE01",
            "You will love it. Don't miss.",
            "Sounds like a plan. \uD83D\uDE0E",
            "\uD83D\uDE48 \uD83D\uDE49 \uD83D\uDE49"
        };
    }
}
    
    

    

