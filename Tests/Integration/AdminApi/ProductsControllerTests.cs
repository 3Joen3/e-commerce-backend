using System.Net;
using System.Net.Http.Json;
using AdminApi.Requests;
using AdminApi.Responses;
using Tests.Integration.AdminApi.Utils;
using Tests.Integration.Shared;

namespace Tests.Integration.AdminApi;

public class ProductsControllerTests
{
    private const string BaseEndpoint = "/products";

    //FIX
    [Fact]
    public async Task CreateProduct_WithValidData_ShouldSucceed()
    {
        var request = new CreateProductRequest
        {
            Title = "Red T-Shirt",
            Variants = [new ProductVariantRequest { Price = 299, ComparePrice = 399 }],
        };

        var client = AdminApiTestHelper.GetClient();

        var response = await client.PostAsJsonAsync(BaseEndpoint, request);

        var lol = "LOL";

        Assert.Equal("LOL", lol);
    }

    [Fact]
    public async Task GetProductById_NonExistentId_ReturnsNotFound()
    {
        var randomId = Guid.NewGuid();

        var client = AdminApiTestHelper.GetClient();

        var response = await client.GetAsync($"{BaseEndpoint}/{randomId}");
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task GetProductById_ExistentId_ReturnsProduct()
    {
        var product = IntegrationTestHelper.GetProduct();

        var factory = AdminApiTestHelper.GetFactory();

        factory.SeedDb(db =>
        {
            db.Products.Add(product);
            db.SaveChanges();
        });

        var client = factory.CreateClient();

        var response = await client.GetAsync($"/products/{product.Id}");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var responseProduct = await IntegrationTestHelper.DeserializeApiResponse<ProductResponse>(response);

        Assert.Equal(responseProduct?.Id, product.Id);
        Assert.NotNull(responseProduct);
        Assert.Equal(product.Id, responseProduct!.Id);
    }
}