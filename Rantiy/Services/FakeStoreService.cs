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

    public async Task<List<string>> GetCategories()
    {
        HttpResponseMessage response = await _httpClient.GetAsync(APIConstants.CategoriesEndPoint);

        if (response.IsSuccessStatusCode)
        {
            string content = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<List<string>>(content);
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

    public async Task<List<Product>> GetProductsByCategory(string categoryName)
    {
        HttpResponseMessage response = await _httpClient.GetAsync($"{APIConstants.FilterProductsByCategoryEndPoint}{categoryName}");

        if (response.IsSuccessStatusCode)
        {
            string content = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<List<Product>>(content);
        }

        return default;
    }
}