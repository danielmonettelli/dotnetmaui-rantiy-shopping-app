namespace Rantiy.Views;

public partial class HomePage : ContentPage
{
    private readonly HomeViewModel vm = new(new FakeStoreService());

    public HomePage()
    {
        InitializeComponent();

        BindingContext = vm;
    }

    private void HomePage_SizeChanged(object sender, EventArgs e)
    {
        vm.Columns = (int)(Width / 178);
    }
}