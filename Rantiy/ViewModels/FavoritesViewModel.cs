namespace Rantiy.ViewModels;

public partial class FavoritesViewModel : BaseViewModel, IRecipient<FavoriteProductMessage>
{
    [ObservableProperty]
    private ObservableCollection<Product> favoriteProducts = [];

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
            Product? existingProduct = FavoriteProducts.FirstOrDefault(p => p.Id == message.CurrentProduct.Id);
            if (existingProduct == null)
            {
                FavoriteProducts.Add(message.CurrentProduct);
                OnPropertyChanged(nameof(FavoriteProducts));
            }
        }
        else
        {
            // Find product by ID and remove if it exists
            Product? productToRemove = FavoriteProducts.FirstOrDefault(p => p.Id == message.CurrentProduct.Id);
            if (productToRemove != null)
            {
                _ = FavoriteProducts.Remove(productToRemove);
                OnPropertyChanged(nameof(FavoriteProducts));
            }
        }
    }
}