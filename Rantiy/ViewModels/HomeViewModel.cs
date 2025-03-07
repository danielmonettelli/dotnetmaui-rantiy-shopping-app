namespace Rantiy.ViewModels;

public partial class HomeViewModel : BaseViewModel
{
    private readonly IFakeStoreService _fakeStoreService;
    private readonly Dictionary<string, bool> _favoriteProductsState = [];

    public HomeViewModel(IFakeStoreService fakeStoreService)
    {
        _fakeStoreService = fakeStoreService;

        _ = LoadDataAsync();

        WeakReferenceMessenger.Default.Register<FavoriteProductMessage>(this, (recipient, message) =>
        {
            Product? product = FilteredProducts.FirstOrDefault(p => p.Title == message.CurrentProduct.Title);

            if (product != null)
            {
                product.IsFavorite = message.IsFavorite;

                // Almacenar el estado del favorito por título del producto
                _favoriteProductsState[product.Title] = message.IsFavorite;
            }
        });
    }

    [ObservableProperty]
    private List<string> categories;

    [ObservableProperty]
    private string selectedCategory;

    [ObservableProperty]
    private List<Product> filteredProducts;

    [ObservableProperty]
    private Product selectedFilteredProduct;

    [ObservableProperty]
    private bool isIconFavorite;

    [ObservableProperty]
    private string favoriteIconSource = "icon_favorite_outline";

    partial void OnSelectedCategoryChanged(string value)
    {
        if (value == "All")
        {
            _ = LoadAllProductsAsync();
        }
        else
        {
            _ = FilterProductsByCategoryAsync(value);
        }
    }

    public async Task LoadDataAsync()
    {
        // Fetch and store categories from service
        Categories = await _fakeStoreService.GetCategories();

        // Insert "All" option at the beginning of the categories list
        Categories.Insert(0, "All");

        // Set "All" as the initially selected category
        SelectedCategory = Categories.First();

        // Load and display all products by default
        FilteredProducts = await _fakeStoreService.GetAllProducts();

        // Restaurar estado de favoritos
        RestoreFavoritesState();
    }

    public async Task LoadAllProductsAsync()
    {
        FilteredProducts = await _fakeStoreService.GetAllProducts();

        // Restaurar estado de favoritos
        RestoreFavoritesState();
    }

    public async Task FilterProductsByCategoryAsync(string categoryName)
    {
        FilteredProducts = await _fakeStoreService.GetProductsByCategory(categoryName);

        // Restaurar estado de favoritos
        RestoreFavoritesState();
    }

    private void RestoreFavoritesState()
    {
        if (FilteredProducts != null)
        {
            foreach (Product product in FilteredProducts)
            {
                // Si hay un estado guardado para este producto, aplicarlo
                if (_favoriteProductsState.TryGetValue(product.Title, out bool isFavorite))
                {
                    product.IsFavorite = isFavorite;
                }
            }
        }
    }

    [RelayCommand]
    public async Task SelectedFilteredProductAsync()
    {
        Dictionary<string, object> productParameter = new()
        {
            [nameof(SelectedFilteredProduct)] = SelectedFilteredProduct
        };

        await Shell.Current.GoToAsync(
            state: nameof(ProductDetailPage),
            animate: true,
            parameters: productParameter);
    }

    [RelayCommand]
    public void ToggleFavoriteIcon(Product product)
    {
        if (product != null)
        {
            product.IsFavorite = !product.IsFavorite;

            // Guardar el estado actual
            _favoriteProductsState[product.Title] = product.IsFavorite;

            _ = WeakReferenceMessenger.Default.Send(new FavoriteProductMessage(product, product.IsFavorite));

            FavoriteIconSource = product.IsFavorite ? "icon_favorite_solid" : "icon_favorite_outline";
        }
    }
}