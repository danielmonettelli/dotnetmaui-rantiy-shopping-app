namespace Rantiy.Views;

public partial class FavoritesPage : ContentPage
{
    public FavoritesPage()
    {
        InitializeComponent();

    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        BindingContext = App.FavoritesViewModel;
    }

    private void FavoritesPage_SizeChanged(object sender, EventArgs e)
    {
        App.FavoritesViewModel.Columns = (int)(Width / 178);
    }
}