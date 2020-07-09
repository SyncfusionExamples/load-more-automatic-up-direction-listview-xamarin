using System;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using Syncfusion.DataSource.Extensions;
using Syncfusion.ListView.XForms;
using Syncfusion.ListView.XForms.Helpers;
using Syncfusion.ListView.XForms.Control.Helpers;

namespace ListviewXamarin
{
  public partial class MainPage : ContentPage
{ 
    MainPageViewModel ViewModel;
        VisualContainer visualContainer;
        public bool isScrolled;
    HeaderItem headerItem;

    public MainPage()
    {
        InitializeComponent();
        ViewModel = new MainPageViewModel();
        BindingContext = ViewModel;
        ViewModel.ListView = this.ListView;
        ListView.Loaded += ListView_Loaded;
        visualContainer = ListView.GetVisualContainer();
    }

    private void HeaderItem_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        if (e.PropertyName == "Visibility")
        {
            if (headerItem.Visibility && isScrolled)
                LoadMoreOnTop();
        }
    }

    private async void LoadMoreOnTop()
    {
        //To get the current first item which is visible in the View.
        var firstItem = ListView.DataSource.DisplayItems[0];
        ViewModel.IndicatorIsVisible = true;
        await Task.Delay(4000);
        var r = new Random();

        //To avoid layout calls for arranging each and every items to be added in the View. 
        ListView.DataSource.BeginInit();
        for (int i = 0; i < 5; i++)
        {
            var collection = new Message();
            collection.Text = ViewModel.MessageText[r.Next(0, ViewModel.MessageText.Count() - 1)];
            collection.IsIncoming = i % 2 == 0 ? true : false;
            collection.MessagDateTime = DateTime.Now.ToString();
            ViewModel.Messages.Insert(0, collection);
        }
        ListView.DataSource.EndInit();

        var firstItemIndex = ListView.DataSource.DisplayItems.IndexOf(firstItem);
        var header = (ListView.HeaderTemplate != null && !ListView.IsStickyHeader) ? 1 : 0;
        var totalItems = firstItemIndex + header;

        //Need to scroll back to previous position else the ScrollViewer moves to top of the list.
        ListView.LayoutManager.ScrollToRowIndex(totalItems, true);
        ViewModel.IndicatorIsVisible = false;
    }

    private void ListView_Loaded(object sender, Syncfusion.ListView.XForms.ListViewLoadedEventArgs e)
    {
        //To avoid loading items initially when page loaded.
        if (!isScrolled)
            (ListView.LayoutManager as LinearLayout).ScrollToRowIndex(ViewModel.Messages.Count - 1, true);
        headerItem = visualContainer.Children[0] as HeaderItem;
        headerItem.PropertyChanged += HeaderItem_PropertyChanged;
        isScrolled = true;
    }
}
}
