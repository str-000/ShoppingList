using Firebase.Database;
using Firebase.Database.Query;
using System.Collections.ObjectModel;
using System.Xml.Linq;

namespace ShoppingList;

public partial class MainPage : ContentPage
{
    public class Category
    {
        public string Name { get; set; }
        public int Id { get; set; }
    }

    FirebaseHelper firebaseHelper = new FirebaseHelper();

    public ObservableCollection<Category> Categories { get; set; } = new ObservableCollection<Category>();
    
    public MainPage()
	{
		InitializeComponent();
        
        BindingContext = this;
    }
    
    private async void AddNewCategory(System.Object sender, System.EventArgs e)
    {
        string name = await DisplayPromptAsync("Add Category", "Enter the name of the category");
        int count = Categories.Count + 1;
        Categories.Add(new Category { Name = name, Id = count });
        await firebaseHelper.AddCategory(name, count);   
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
