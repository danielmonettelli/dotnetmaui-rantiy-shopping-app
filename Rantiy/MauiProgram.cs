namespace Rantiy;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("RobotoCondensed-Italic.ttf", "Roboto#300");
                fonts.AddFont("Roboto-Regular.ttf", "Roboto#400");
                fonts.AddFont("Roboto-Medium.ttf", "Roboto#500");
                fonts.AddFont("Roboto-Bold.ttf", "Roboto#700");
                fonts.AddFont("customfonticons.ttf", "CustomFontIcons");
            }).UseMauiCommunityToolkit();

        builder.Services.AddSingleton<IFakeStoreService, FakeStoreService>();

        builder.Services.AddSingleton<HomeViewModel>();
        builder.Services.AddTransient<HomePage>();

        builder.Services.AddTransient<ProductDetailViewModel>();
        builder.Services.AddTransient<ProductDetailPage>();

        builder.Services.AddTransient<ShoppingCartViewModel>();
        builder.Services.AddTransient<ShoppingCartPage>();

        builder.Services.AddTransient<FavoritesViewModel>();
        builder.Services.AddTransient<FavoritesPage>();

#if DEBUG
        builder.Logging.AddDebug();
#endif
        return builder.Build();
    }
}