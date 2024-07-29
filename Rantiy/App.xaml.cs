namespace Rantiy;

public partial class App : Application
{
    private static Lazy<FavoritesViewModel> _favoritesViewModel = new(() => new FavoritesViewModel());
    private static Lazy<ShoppingCartViewModel> _shoppingCartViewModel = new(() => new ShoppingCartViewModel());

    public static FavoritesViewModel FavoritesViewModel => _favoritesViewModel.Value;
    public static ShoppingCartViewModel ShoppingCartViewModel => _shoppingCartViewModel.Value;

    public App()
    {
        InitializeComponent();

        MainPage = new AppShell();
    }

    protected override void OnStart()
    {
        base.OnStart();

        var favoritesViewModel = FavoritesViewModel;
        var shoppingCartViewModel = ShoppingCartViewModel;
    }
}
