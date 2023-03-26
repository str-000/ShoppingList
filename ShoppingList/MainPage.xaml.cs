using Firebase.Database;
using Firebase.Database.Query;
using System.Collections.ObjectModel;
using System.Xml.Linq;

namespace ShoppingList;

public partial class MainPage : ContentPage
{
    public class CategoryGroup : ObservableCollection<Item>
    {
        public string Name { get; private set; }

        public CategoryGroup(string name, ObservableCollection<Item> items) : base(items)
        {
            Name = name;
        }
    }

    public class Item
    {
        public string Name { get; set; }
        public int Quantity { get; set; }
        public bool IsChecked { get; set; }
        public string Id { get; set; }
    }

    public ObservableCollection<Item> Items { get; set; } = new ObservableCollection<Item>();

    public MainPage()
	{
		InitializeComponent();
        BindingContext = this;
    }

    private async void AddNewCategory(System.Object sender, System.EventArgs e)
    {
        string name = await DisplayPromptAsync("Add Category", "Enter the name of the category");
    }

    private async void AddNewItem(System.Object sender, System.EventArgs e)
    {
        string name = await App.Current.MainPage.DisplayPromptAsync("Add Item", "Enter the name of the item");
    }

    private void ChangeDayNightMode(System.Object sender, System.EventArgs e)
    {
        AppTheme currentTheme = Application.Current.RequestedTheme;
        
        if (currentTheme == AppTheme.Dark)
            Application.Current.UserAppTheme = AppTheme.Light;
        else
            Application.Current.UserAppTheme = AppTheme.Dark;
    }

    private void ChangeViewMode(System.Object sender, System.EventArgs e)
    {
        
    }
}
