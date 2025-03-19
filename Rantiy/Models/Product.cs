namespace Rantiy.Models;

public partial class Product : ObservableObject
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("title")]
    public required string Title { get; set; }

    [JsonPropertyName("price")]
    public float Price { get; set; }

    [JsonPropertyName("description")]
    public required string Description { get; set; }

    [JsonPropertyName("category")]
    public required string Category { get; set; }

    [JsonPropertyName("image")]
    public required string Image { get; set; }

    [JsonPropertyName("rating")]
    public required Rating Rating { get; set; }

    [ObservableProperty]
    private bool isFavorite;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(TotalPrice))]
    private int quantity = 1;
    
    // Propiedad calculada para obtener el precio total basado en la cantidad
    public float TotalPrice => Price * Quantity;
}

public class Rating
{
    [JsonPropertyName("rate")]
    public float Rate { get; set; }

    [JsonPropertyName("count")]
    public int Count { get; set; }
}