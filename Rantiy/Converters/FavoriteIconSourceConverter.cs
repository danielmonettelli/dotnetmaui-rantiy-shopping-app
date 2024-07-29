namespace Rantiy.Converters;

public class FavoriteIconSourceConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        string resourceName = value as string;

        if (Application.Current.Resources.TryGetValue(resourceName, out var resource))
        {
            return resource;
        }

        // Return a default icon if resource not found
        return "icon_default_outline";
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
