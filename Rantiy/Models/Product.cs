namespace Rantiy.Models;

public partial class Product : ObservableObject
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("title")]
    public string Title { get; set; }

    [JsonPropertyName("price")]
    public float Price { get; set; }

    [JsonPropertyName("description")]
    public string Description { get; set; }

    [JsonPropertyName("category")]
    public string Category { get; set; }

    [JsonPropertyName("image")]
    public string Image { get; set; }

    [JsonPropertyName("rating")]
    public Rating Rating { get; set; }

    [ObservableProperty]
    private bool isFavorite;
}

public class Rating
{
    [JsonPropertyName("rate")]
    public float Rate { get; set; }

    [JsonPropertyName("count")]
    public int Count { get; set; }
}