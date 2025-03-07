namespace Rantiy.ViewModels;

public partial class FavoritesViewModel : BaseViewModel, IRecipient<FavoriteProductMessage>
{
    [ObservableProperty]
    private ObservableCollection<Product> favoriteProducts = new();

    [ObservableProperty]
    private int columns = 2;

    public FavoritesViewModel()
    {
        Title = "Favorite Products";

        WeakReferenceMessenger.Default.Register<FavoriteProductMessage>(this);
    }

    public void Receive(FavoriteProductMessage message)
    {
        if (message.IsFavorite)
        {
            // Check if product with same ID already exists
            var existingProduct = FavoriteProducts.FirstOrDefault(p => p.Id == message.CurrentProduct.Id);
            if (existingProduct == null)
            {
                FavoriteProducts.Add(message.CurrentProduct);
                OnPropertyChanged(nameof(FavoriteProducts));
            }
        }
        else
        {
            // Find product by ID and remove if it exists
            var productToRemove = FavoriteProducts.FirstOrDefault(p => p.Id == message.CurrentProduct.Id);
            if (productToRemove != null)
            {
                FavoriteProducts.Remove(productToRemove);
                OnPropertyChanged(nameof(FavoriteProducts));
            }
        }
    }
}