using Firebase.Database;
using Firebase.Database.Query;
using Microsoft.Maui.Storage;
using System.Collections.ObjectModel;
using static ShoppingList.MainPage;

namespace ShoppingList;

public class FirebaseHelper : ContentPage
{
    FirebaseClient firebaseClient = new FirebaseClient("https://shoppinglist-60cbe-default-rtdb.europe-west1.firebasedatabase.app/");

    public async Task AddCategory(String name, ObservableCollection<CategoryGroup> Items)
    {
        await firebaseClient
          .Child(name)
          .PutAsync(Items);
    }
}