namespace Rantiy;

public partial class App : Application
{
    private static readonly Lazy<FavoritesViewModel> _favoritesViewModel = new(() => new FavoritesViewModel());
    private static readonly Lazy<ShoppingCartViewModel> _shoppingCartViewModel = new(() => new ShoppingCartViewModel());

    public static FavoritesViewModel FavoritesViewModel => _favoritesViewModel.Value;
    public static ShoppingCartViewModel ShoppingCartViewModel => _shoppingCartViewModel.Value;

    public App()
    {
        InitializeComponent();
    }

    protected override Window CreateWindow(IActivationState? activationState)
    {
        return new Window(new AppShell());
    }

    protected override void OnStart()
    {
        base.OnStart();

        _ = FavoritesViewModel;
        _ = ShoppingCartViewModel;
    }
}