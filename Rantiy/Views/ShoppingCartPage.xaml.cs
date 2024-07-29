namespace Rantiy.Views;

public partial class ShoppingCartPage : ContentPage
{
    private readonly ShoppingCartViewModel vm = new();

    public ShoppingCartPage()
    {
        InitializeComponent();

    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        BindingContext = App.ShoppingCartViewModel;
    }
}