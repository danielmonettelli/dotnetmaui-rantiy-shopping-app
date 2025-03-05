namespace Rantiy.Services;

/// <summary>
/// Provides a set of methods for interacting with a fake store's data, including fetching products, categories, and performing searches.
/// This interface is designed to be implemented by services that communicate with a fake store API, facilitating data retrieval for ViewModels.
/// </summary>
public interface IFakeStoreService
{
    /// <summary>
    /// Asynchronously retrieves all products available in the fake store.
    /// This method is useful for displaying a complete list of products, for example, when the user selects an "All" category in a UI.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation, containing a list of all <see cref="Product"/> instances.</returns>
    Task<List<Product>> GetAllProducts();

    /// <summary>
    /// Asynchronously retrieves a list of all categories from the fake store.
    /// This method supports populating category selections in the UI, allowing users to filter products by category.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation, containing a list of all category names as strings.</returns>
    Task<List<string>> GetCategories();

    /// <summary>
    /// Asynchronously retrieves products filtered by a specific category.
    /// This method is intended for filtering the displayed products list when a user selects a category other than "All".
    /// </summary>
    /// <param name="categoryName">The name of the category for which products are to be retrieved.</param>
    /// <returns>A task that represents the asynchronous operation, containing a list of <see cref="Product"/> instances that belong to the specified category.</returns>
    Task<List<Product>> GetProductsByCategory(string categoryName);
}
