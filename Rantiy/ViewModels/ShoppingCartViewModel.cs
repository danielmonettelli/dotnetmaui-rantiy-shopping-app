namespace Rantiy.ViewModels;

public partial class ShoppingCartViewModel : BaseViewModel
{
    // Observable collection of products in the shopping cart
    [ObservableProperty]
    private ObservableCollection<Product> shoppingCartProducts = new();

    public decimal TotalPrice
    {
        get { return (decimal)ShoppingCartProducts.Sum(product => product.Price); }
    }


    public ShoppingCartViewModel()
    {
        Title = "Shopping Cart";

        WeakReferenceMessenger.Default.Register<Product>(this, (r, product) =>
        {
            // Handle the received message and add the product to the cart
            ShoppingCartProducts.Add(product);

            // Print to console to verify the product was added
            Console.WriteLine($"Product added: {product.Title}");

            // Optional: Notify property change
            OnPropertyChanged(nameof(ShoppingCartProducts));
        });

        ShoppingCartProducts.CollectionChanged += (s, e) =>
        {
            // Notify property change when the collection changes
            OnPropertyChanged(nameof(ShoppingCartProducts));
            OnPropertyChanged(nameof(TotalPrice));
        };
    }

    [RelayCommand]
    public void RemoveProduct(Product product)
    {
        // Remove the product from the shopping cart
        ShoppingCartProducts.Remove(product);

        OnPropertyChanged(nameof(ShoppingCartProducts));
    }
}
