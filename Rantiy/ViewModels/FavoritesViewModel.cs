namespace Rantiy.ViewModels;

public partial class FavoritesViewModel : BaseViewModel, IRecipient<FavoriteProductMessage>
{
    [ObservableProperty]
    private ObservableCollection<Product> favoriteProducts = [];

    [ObservableProperty]
    private int columns = 2;

    [ObservableProperty]
    private bool isFavoritesLoading = true; // Inicialmente está cargando

    public FavoritesViewModel()
    {
        Title = "Favorite Products";
        WeakReferenceMessenger.Default.Register<FavoriteProductMessage>(this);
    }

    [RelayCommand]
    public async Task LoadFavoritesAsync()
    {
        try
        {
            // Aseguramos que el estado de carga esté activo
            if (!IsFavoritesLoading)
                IsFavoritesLoading = true;
            
            // Simulamos tiempo de carga (reemplazar con lógica real de carga)
            await Task.Delay(1000);
            
            // Aquí podríamos cargar datos de una base de datos o servicio
            // Por ejemplo: var items = await _productService.GetFavoriteProductsAsync();
            // FavoriteProducts = new ObservableCollection<Product>(items);
            
            // Finalizamos la carga con una pequeña animación
            await FinishLoadingWithAnimationAsync();
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error cargando favoritos: {ex.Message}");
            // Aseguramos que no se quede en estado de carga si hay un error
            await FinishLoadingWithAnimationAsync();
        }
    }

    private async Task FinishLoadingWithAnimationAsync()
    {
        try
        {
            // Dejamos que el shimmer complete la última animación
            await Task.Delay(300);
            
            // Desactivamos el estado de carga
            IsFavoritesLoading = false;
            
            // Animamos la aparición del contenido real
            var contentView = Shell.Current.CurrentPage.FindByName<ContentView>("MainContentView");
            if (contentView != null)
            {
                await Task.WhenAll(
                    contentView.FadeTo(1, 300, Easing.CubicOut),
                    contentView.ScaleTo(1, 300, Easing.CubicOut),
                    contentView.TranslateTo(0, 0, 300, Easing.CubicOut)
                );
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error en FinishLoadingWithAnimationAsync: {ex.Message}");
            // En caso de error, asegurar que el estado de carga se desactive
            IsFavoritesLoading = false;
        }
    }

    public void Receive(FavoriteProductMessage message)
    {
        MainThread.BeginInvokeOnMainThread(async () => {
            try
            {
                // Solo activamos el shimmer brevemente para actualizaciones de favoritos
                if (!IsFavoritesLoading)
                    IsFavoritesLoading = true;
                
                // Pequeño retraso para que el shimmer se note
                await Task.Delay(300);
                
                // Actualizamos la colección de favoritos
                if (message.IsFavorite)
                {
                    // Verificamos que el producto no exista ya
                    Product? existingProduct = FavoriteProducts.FirstOrDefault(p => p.Id == message.CurrentProduct.Id);
                    if (existingProduct == null)
                    {
                        // Hacemos una copia del producto para evitar problemas de referencia
                        Product newProduct = new Product
                        {
                            Id = message.CurrentProduct.Id,
                            Category = message.CurrentProduct.Category,
                            Description = message.CurrentProduct.Description,
                            Image = message.CurrentProduct.Image,
                            Price = message.CurrentProduct.Price,
                            Rating = message.CurrentProduct.Rating,
                            Title = message.CurrentProduct.Title,
                            IsFavorite = true
                        };

                        FavoriteProducts.Add(newProduct);
                    }
                }
                else
                {
                    // Buscamos y eliminamos el producto si existe
                    Product? productToRemove = FavoriteProducts.FirstOrDefault(p => p.Id == message.CurrentProduct.Id);
                    if (productToRemove != null)
                    {
                        FavoriteProducts.Remove(productToRemove);
                    }
                }
                
                // Notificamos que la colección ha cambiado
                OnPropertyChanged(nameof(FavoriteProducts));
                
                // Terminamos la carga con animación
                await FinishLoadingWithAnimationAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error en Receive: {ex.Message}");
                IsFavoritesLoading = false;
            }
        });
    }

    [RelayCommand]
    public async Task RemoveFavoriteAsync(Product product)
    {
        if (product == null)
            return;
            
        // Mostrar mensaje de confirmación
        bool result = await Shell.Current.CurrentPage.DisplayAlert(
            "Remove from Favorites", 
            $"Are you sure you want to remove {product.Title} from your favorites?", 
            "Yes", "No");

        if (result)
        {
            try
            {
                // Para eliminar favoritos, usamos una animación más sutil sin shimmer
                // ya que no es necesario bloquear la UI para esta operación
                var item = Shell.Current.CurrentPage.FindByName<Border>($"item_{product.Id}");
                if (item != null)
                {
                    // Animamos la desaparición del elemento
                    await item.ScaleTo(0.8, 150);
                    await item.FadeTo(0, 150);
                }

                // Cambiar el estado del producto a no favorito
                product.IsFavorite = false;
                
                // Eliminar de la colección local de favoritos
                FavoriteProducts.Remove(product);
                
                // Notificar a otras vistas del cambio
                WeakReferenceMessenger.Default.Send(new FavoriteProductMessage(product, false));
                
                // Notificar que la colección ha cambiado
                OnPropertyChanged(nameof(FavoriteProducts));
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error en RemoveFavoriteAsync: {ex.Message}");
            }
        }
    }

    // Método para refrescar los favoritos (útil para un pull-to-refresh)
    [RelayCommand]
    public async Task RefreshFavoritesAsync()
    {
        await LoadFavoritesAsync();
    }
}