namespace Rantiy.ViewModels;

public partial class BaseViewModel : ObservableObject
{
    [ObservableProperty]
    private bool isBusy;

    [ObservableProperty]
    private string? title;

    [ObservableProperty]
    private int columns;
}