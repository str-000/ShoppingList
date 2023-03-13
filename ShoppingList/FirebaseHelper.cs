using Firebase.Database;
using Firebase.Database.Query;
using Microsoft.Maui.Storage;
using static ShoppingList.MainPage;
using static ShoppingList.CategoryView.CategoryView;

namespace ShoppingList;

public class FirebaseHelper : ContentPage
{
    FirebaseClient firebaseClient = new FirebaseClient("https://shoppinglist-60cbe-default-rtdb.europe-west1.firebasedatabase.app/");

    public async Task<Category> GetAllCategories()
    {
        return (await firebaseClient
             .Child("Categories")
             .OnceAsync<Category>()).Select(item => new Category
             {
                 Name = item.Object.Name,
                 Id = item.Object.Id
             }).FirstOrDefault();
    }

    public async Task AddCategory(string name, int id)
    {
        await firebaseClient
          .Child("Categories")
          .PostAsync(new Category() { Name = name, Id = id });
    }

    public async Task AddItem(string name, int id)
    {
        await firebaseClient
          .Child("Items")
          .PostAsync(new Item() { Name = name, Id = id, IsChecked = false, Quantity = 1 });
    }

    public async Task DeleteCategory(int id)
    {
        var toDeleteCategory = (await firebaseClient
          .Child("Category")
          .OnceAsync<Category>()).Where(a => a.Object.Id == id).FirstOrDefault();
        await firebaseClient.Child("Category").Child(toDeleteCategory.Key).DeleteAsync();
    }
}