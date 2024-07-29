namespace Rantiy.Messages;

public class FavoriteProductMessage
{
    public Product CurrentProduct { get; set; }
    public bool IsFavorite { get; set; }

    public FavoriteProductMessage(Product product, bool isFavorite)
    {
        CurrentProduct = product;
        IsFavorite = isFavorite;
    }
}
