namespace Rantiy.Services;

public class FakeStoreService : IFakeStoreService
{
    private readonly HttpClient _httpClient;

    public FakeStoreService()
    {
        _httpClient = new HttpClient();

        _httpClient.BaseAddress = new Uri(APIConstants.APIBaseUrl);

        _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    }

    public async Task<List<Category>> GetCategories()
    {
        HttpResponseMessage response = await _httpClient.GetAsync(APIConstants.CategoriesEndPoint);

        if (response.IsSuccessStatusCode)
        {
            string content = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<List<Category>>(content);
        }

        return default;
    }

    public async Task<List<Product>> GetAllProducts()
    {
        HttpResponseMessage response = await _httpClient.GetAsync(APIConstants.ProductsEndPoint);

        if (response.IsSuccessStatusCode)
        {
            string content = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<List<Product>>(content);
        }

        return default;
    }

    public async Task<List<Product>> GetProductsByCategory(int categoryId)
    {
        HttpResponseMessage response = await _httpClient.GetAsync($"{APIConstants.FilterProductsByCategoryEndPoint}{categoryId}");

        if (response.IsSuccessStatusCode)
        {
            string content = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<List<Product>>(content);
        }

        return default;
    }

    public async Task<List<Product>> SearchProductsByTitle(string productTitle)
    {
        HttpResponseMessage response = await _httpClient.GetAsync($"{APIConstants.FilterProductsByTitleEndPoint}{productTitle}");

        if (response.IsSuccessStatusCode)
        {
            string content = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<List<Product>>(content);
        }

        return default;
    }
}