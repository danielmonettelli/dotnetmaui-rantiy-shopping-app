namespace Rantiy.Converters;

public class FavoriteIconSourceConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        string? iconName = value as string;

        if (string.IsNullOrEmpty(iconName))
        {
            return "icon_favorite_outline";
        }

        // Verificar si el nombre ya incluye el prefijo "icon_"
        if (iconName.StartsWith("icon_"))
        {
            if (Application.Current.Resources.TryGetValue(iconName, out object? resource))
            {
                return resource;
            }
        }

        // Si el recurso no se encuentra, devolver un icono predeterminado
        return Application.Current.Resources.TryGetValue("icon_favorite_outline", out object? defaultResource)
            ? defaultResource
            : "icon_favorite_outline";
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
