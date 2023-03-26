using Firebase.Database;
using Firebase.Database.Query;
using System.Collections.ObjectModel;
using System.Xml.Linq;

namespace ShoppingList;

public partial class MainPage : ContentPage
{
    FirebaseClient firebaseClient = new FirebaseClient("https://shoppinglist-60cbe-default-rtdb.europe-west1.firebasedatabase.app/");
    FirebaseHelper firebaseHelper = new FirebaseHelper();

    public class CategoryGroup : ObservableCollection<Item>
    {
        public string Name { get; private set; }
        public string CategoryName { get; private set; }

        public CategoryGroup(string name, string categoryName, ObservableCollection<Item> items) : base(items)
        {
            Name = name;
            CategoryName = categoryName;
        }
    }

    public class Item
    {
        public string Name { get; set; }
        public int Quantity { get; set; }
        public bool IsChecked { get; set; }
        public string Id { get; set; }
    }

    public ObservableCollection<CategoryGroup> Items { get; set; } = new ObservableCollection<CategoryGroup>();

    public MainPage()
	{
		InitializeComponent();
        BindingContext = this;

        var collection = firebaseClient
        .Child("Categories")
        .AsObservable<CategoryGroup>()
        .Subscribe((item) =>
        {
            if (item.Object != null)
            {
                Items.Add(item.Object);
            }
        });
    }

    private async void AddNewCategory(System.Object sender, System.EventArgs e)
    {
        string name = await DisplayPromptAsync("Add Category", "Enter the name of the category");
        Items.Add(new CategoryGroup(name, name, new ObservableCollection<Item> { }));
        await firebaseHelper.AddCategory(name, Items);
    }

    private async void DeleteCategory(System.Object sender, System.EventArgs e)
    {
        Button button = (Button)sender;
        string categoryName = (string)button.CommandParameter;
        CategoryGroup category = Items.FirstOrDefault(c => c.Name == categoryName);
        if (category != null)
        {
            bool answer = await DisplayAlert
                ("Delete All", "Are you sure?", "Yes", "No");
            if (answer) Items.Remove(category);
        }
    }

    private async void AddNewItem(System.Object sender, System.EventArgs e)
    {
        Button button = (Button)sender;
        string categoryName = (string)button.CommandParameter;
        CategoryGroup category = Items.FirstOrDefault(c => c.Name == categoryName);
        if (category != null)
        {
            string name = await App.Current.MainPage.DisplayPromptAsync(
                "Add Item", "Enter the name of the item");
            category.Add(new Item { Name = name, Quantity = 1, IsChecked = false, Id = "1" });
        }
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
