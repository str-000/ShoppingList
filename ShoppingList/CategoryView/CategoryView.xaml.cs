using Firebase.Database;
using Firebase.Database.Query;
using Microsoft.Maui.Storage;
using System.Collections.ObjectModel;

namespace ShoppingList.CategoryView;

public partial class CategoryView : ContentView
{
    FirebaseClient firebaseClient = new FirebaseClient("https://shoppinglist-60cbe-default-rtdb.europe-west1.firebasedatabase.app/");

    public class Item
    {
        public string Name { get; set; }
        public int Quantity { get; set; }
        public bool IsChecked { get; set; }
        public int Id { get; set; }
    }

    FirebaseHelper firebaseHelper = new FirebaseHelper();

    public ObservableCollection<Item> Items { get; set; } = new ObservableCollection<Item>();

    public static readonly BindableProperty NameProperty = BindableProperty.Create(
        nameof(Name),
        typeof(string),
        typeof(CategoryView),
        defaultValue: "Item Name");

    public string Name
    {
        get => (string)GetValue(NameProperty);
        set => SetValue(NameProperty, value);
    }

    public CategoryView()
	{
		InitializeComponent();

        BindingContext = this;
        
        var collection = firebaseClient
        .Child("Categories")
        .AsObservable<Item>()
        .Subscribe((item) =>
        {
            if (item.Object != null)
            {
                Items.Add(item.Object);
            }
        });
    }

    private async void DeleteCategory(System.Object sender, System.EventArgs e)
    {
        await firebaseHelper.DeleteCategory(1);
        await App.Current.MainPage.DisplayAlert("Success", "Category Deleted Successfully", "OK");
    }
    
    private async void AddNewItem(System.Object sender, System.EventArgs e)
    {
        string name = await App.Current.MainPage.DisplayPromptAsync("Add Item", "Enter the name of the item");
        int count = MainPage.Categories.Count + 1;
        int item_count = Items.Count + 1;
        await firebaseHelper.AddItem(name, count, item_count);
    }
}