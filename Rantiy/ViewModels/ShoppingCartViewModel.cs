namespace Rantiy.ViewModels;

public partial class ShoppingCartViewModel : BaseViewModel
{
    // Observable collection of products in the shopping cart
    [ObservableProperty]
    private ObservableCollection<Product> shoppingCartProducts = [];

    public decimal TotalPrice => (decimal)ShoppingCartProducts.Sum(product => product.TotalPrice);

    public int TotalItems => ShoppingCartProducts.Sum(product => product.Quantity);

    public ShoppingCartViewModel()
    {
        Title = "Shopping Cart";

        WeakReferenceMessenger.Default.Register<Product>(this, (r, product) =>
        {
            // Buscar si el producto ya existe en el carrito
            var existingProduct = ShoppingCartProducts.FirstOrDefault(p => p.Id == product.Id);
            
            if (existingProduct != null)
            {
                // Si existe, incrementa la cantidad
                existingProduct.Quantity++;
                Console.WriteLine($"Product quantity increased: {existingProduct.Title}, Quantity: {existingProduct.Quantity}");
            }
            else
            {
                // Si no existe, añade el producto (con cantidad establecida a 1 por defecto)
                ShoppingCartProducts.Add(product);
                Console.WriteLine($"Product added: {product.Title}");
            }

            // Notificar cambio de propiedades
            OnPropertyChanged(nameof(ShoppingCartProducts));
            OnPropertyChanged(nameof(TotalPrice));
            OnPropertyChanged(nameof(TotalItems));
        });

        ShoppingCartProducts.CollectionChanged += (s, e) =>
        {
            // Notify property change when the collection changes
            OnPropertyChanged(nameof(ShoppingCartProducts));
            OnPropertyChanged(nameof(TotalPrice));
            OnPropertyChanged(nameof(TotalItems));
        };
    }

    [RelayCommand]
    public void RemoveProduct(Product product)
    {
        // Remove the product from the shopping cart
        _ = ShoppingCartProducts.Remove(product);

        OnPropertyChanged(nameof(ShoppingCartProducts));
        OnPropertyChanged(nameof(TotalPrice));
        OnPropertyChanged(nameof(TotalItems));
    }
    
    [RelayCommand]
    public void IncreaseQuantity(Product product)
    {
        product.Quantity++;
        OnPropertyChanged(nameof(TotalPrice));
        OnPropertyChanged(nameof(TotalItems));
    }
    
    [RelayCommand]
    public void DecreaseQuantity(Product product)
    {
        if (product.Quantity > 1)
        {
            product.Quantity--;
            OnPropertyChanged(nameof(TotalPrice));
            OnPropertyChanged(nameof(TotalItems));
        }
        else
        {
            // Si la cantidad llega a 0, elimina el producto
            RemoveProduct(product);
        }
    }
}
