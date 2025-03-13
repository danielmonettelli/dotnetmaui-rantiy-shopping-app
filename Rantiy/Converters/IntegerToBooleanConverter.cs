namespace Rantiy.Converters;

public class IntegerToBooleanConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is int count && parameter is string param)
        {
            bool isZero = count == 0;

            // Si el parámetro es "not0", invertimos el resultado
            if (param == "not0")
            {
                return !isZero;
            }

            // Si el parámetro es "0", devolvemos true si count es 0
            if (param == "0")
            {
                return isZero;
            }

            // Si el parámetro es ">0", devolvemos true si count es mayor que 0
            if (param == ">0")
            {
                return count > 0;
            }

            // Si el parámetro es un número, comprobamos la igualdad
            if (int.TryParse(param, out int threshold))
            {
                return count == threshold;
            }
        }

        return false;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
