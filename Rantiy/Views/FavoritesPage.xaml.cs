namespace Rantiy.Views;

public partial class FavoritesPage : ContentPage
{
    public FavoritesPage()
    {
        InitializeComponent();
        SizeChanged += FavoritesPage_SizeChanged;
    }

    private void FavoritesPage_SizeChanged(object? sender, EventArgs e)
    {
        Debug.WriteLine($"Width: {Width}");
        App.FavoritesViewModel.Columns = (int)(Width / 178);
    }

    private void FavoritesPage_Loaded(object sender, EventArgs e)
    {
        MainContentView.Opacity = 0;
        MainContentView.Scale = 0.95;
        MainContentView.TranslationY = 10;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        BindingContext = App.FavoritesViewModel;
        
        // Llamamos al método que activará el shimmer y cargará los datos
        await App.FavoritesViewModel.LoadFavoritesAsync();
    }
}