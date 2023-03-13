namespace ShoppingList.ItemView;

public partial class ItemView : ContentView
{
    int item_quantity = 1;

    public ItemView()
	{
		InitializeComponent();
	}

    public static readonly BindableProperty NameProperty = BindableProperty.Create(
        nameof(Name),
        typeof(string),
        typeof(ItemView),
        defaultValue: "Item Name");
    
    public string Name
    {
        get => (string)GetValue(NameProperty);
        set => SetValue(NameProperty, value);
    }

    private void RemoveItemQuantity(System.Object sender, System.EventArgs e)
    {
        if (item_quantity > 1)
        {
            item_quantity--;

            this.itemQuantity.Text = item_quantity.ToString();
        }
    }

    private void AddItemQuantity(System.Object sender, System.EventArgs e)
    {
        if (item_quantity < 99)
        {
            item_quantity++;

            this.itemQuantity.Text = item_quantity.ToString();
        }
    }

    private void DeleteItem(System.Object sender, System.EventArgs e)
    {

    }
}