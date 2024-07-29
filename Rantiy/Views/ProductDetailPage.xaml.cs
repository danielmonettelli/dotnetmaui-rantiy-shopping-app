namespace Rantiy.Views;

public partial class ProductDetailPage : ContentPage
{
    private readonly ProductDetailViewModel vm = new();

    public ProductDetailPage()
    {
        InitializeComponent();

        BindingContext = vm;
    }
}