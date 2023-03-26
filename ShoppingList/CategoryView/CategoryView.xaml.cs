using Firebase.Database;
using Firebase.Database.Query;
using Microsoft.Maui.Storage;
using System.Collections.ObjectModel;
using System.Xml.Linq;

namespace ShoppingList.CategoryView;

public partial class CategoryView : ContentView
{
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
    }

    private async void DeleteCategory(System.Object sender, System.EventArgs e)
    {
        
        await App.Current.MainPage.DisplayAlert("Success", "Category Deleted Successfully", "OK");
    }
}