using System.Text.Json;
using Core.Entities.ProductEntities;

namespace Tests.Integration.Shared;

public static class IntegrationTestHelper
{
    public static Product GetProduct(string title = "Röd T-shirt", decimal price = 299, decimal? comparePrice = 399)
    {
        var variant = ProductVariant.CreateWithoutAttributes(price, comparePrice);
        return Product.Create(title, [variant]);
    }

    public static async Task<T?> DeserializeApiResponse<T>(HttpResponseMessage response)
    {
        var content = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<T>(content, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });
    }
}