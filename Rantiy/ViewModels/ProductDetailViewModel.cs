namespace Rantiy.ViewModels;

[QueryProperty(nameof(CurrentProduct), "SelectedFilteredProduct")]
public partial class ProductDetailViewModel : BaseViewModel
{
    [ObservableProperty]
    private string favoriteIconSource = "icon_favorite_outline";

    [ObservableProperty]
    private Product currentProduct;

    [ObservableProperty]
    private string selectedProductImageUrl;

    [ObservableProperty]
    private List<string> productImageUrls;

    // Called when the CurrentProduct property changes.
    partial void OnCurrentProductChanged(Product value)
    {
        // Update the list of image URLs based on the current product.
        ProductImageUrls = new List<string> { value.Image };

        // Set the initially selected image URL to the first in the list.
        SelectedProductImageUrl = ProductImageUrls.First();

        // Update favorite icon based on the current product's favorite status
        FavoriteIconSource = value.IsFavorite ? "icon_favorite_solid" : "icon_favorite_outline";
    }

    public ProductDetailViewModel()
    {
        Title = "Product Detail";
    }

    [RelayCommand]
    public void AddCurrentProductToShoppingCart()
    {
        // Send the current product to the shopping cart page for update.
        WeakReferenceMessenger.Default.Send(CurrentProduct);
    }

    [RelayCommand]
    private async Task GoBack()
    {
        await Shell.Current.GoToAsync("..", true);
    }

    [RelayCommand]
    private void ToggleFavorite()
    {
        bool isCurrentlyFavorite = FavoriteIconSource == "icon_favorite_solid";
        FavoriteIconSource = isCurrentlyFavorite ? "icon_favorite_outline" : "icon_favorite_solid";

        if (CurrentProduct != null)
        {
            CurrentProduct.IsFavorite = !isCurrentlyFavorite;
            WeakReferenceMessenger.Default.Send(new FavoriteProductMessage(CurrentProduct, !isCurrentlyFavorite));
        }
    }

    private string IsThisFavorite(string favoriteIconSource)
    {
        string newIconSource = "icon_favorite_outline";

        WeakReferenceMessenger.Default.Register<FavoriteProductMessage>(this, (recipient, message) =>
        {
            if (CurrentProduct.Title == message.CurrentProduct.Title)
            {
                newIconSource = message.IsFavorite ? "icon_favorite_solid" : "icon_favorite_outline";
            }
        });

        return newIconSource;
    }
}
