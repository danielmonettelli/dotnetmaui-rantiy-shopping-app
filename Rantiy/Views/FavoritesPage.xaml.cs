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

    protected override void OnAppearing()
    {
        base.OnAppearing();

        BindingContext = App.FavoritesViewModel;
    }
}