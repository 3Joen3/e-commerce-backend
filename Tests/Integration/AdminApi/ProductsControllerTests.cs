using System.Net;
using AdminApi.Responses;
using Core.Entities.ProductEntities;
using Tests.Integration.AdminApi.Utils;
using Tests.Integration.Shared;

namespace Tests.Integration.AdminApi;

public class ProductsControllerTests
{
    [Fact]
    public async Task GetProductById_NonExistentId_ReturnsNotFound()
    {
        var randomId = Guid.NewGuid();

        var client = AdminApiTestHelper.GetClient();

        var response = await client.GetAsync($"/products/{randomId}");
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