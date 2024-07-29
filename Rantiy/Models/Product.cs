namespace Rantiy.Models;

public partial class Product : ObservableObject
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("title")]
    public string Title { get; set; }

    [JsonPropertyName("price")]
    public int Price { get; set; }

    [JsonPropertyName("description")]
    public string Description { get; set; }

    [JsonPropertyName("images")]
    public List<string> Images { get; set; }

    [JsonPropertyName("creationAt")]
    public DateTime CreationAt { get; set; }

    [JsonPropertyName("updatedAt")]
    public DateTime UpdatedAt { get; set; }

    [JsonPropertyName("category")]
    public Category Category { get; set; }

    [ObservableProperty]
    private bool isFavorite;
}