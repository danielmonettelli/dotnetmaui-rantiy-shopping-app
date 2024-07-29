namespace Rantiy.Converters;

public class ItemCountToSingularPluralConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is int count)
        {
            // Returns the full string, including the count and the correct text for "item" or "items".
            return $"Total {count} {(count == 1 ? "Item" : "Items")}";
        }
        return "Total 0 Items";
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
