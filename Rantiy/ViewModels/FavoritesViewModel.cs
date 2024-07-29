namespace Rantiy.ViewModels;

public partial class FavoritesViewModel : BaseViewModel, IRecipient<FavoriteProductMessage>
{
    [ObservableProperty]
    private ObservableCollection<Product> favoriteProducts = new();

    public FavoritesViewModel()
    {
        Title = "Favorite Products";

        WeakReferenceMessenger.Default.Register<FavoriteProductMessage>(this);
    }

    public void Receive(FavoriteProductMessage message)
    {
        if (message.IsFavorite)
        {
            if (!FavoriteProducts.Contains(message.CurrentProduct))
            {
                FavoriteProducts.Add(message.CurrentProduct);
                OnPropertyChanged(nameof(FavoriteProducts));
            }
        }
        else
        {
            if (FavoriteProducts.Contains(message.CurrentProduct))
            {
                FavoriteProducts.Remove(message.CurrentProduct);
                OnPropertyChanged(nameof(FavoriteProducts));
            }
        }
    }
}