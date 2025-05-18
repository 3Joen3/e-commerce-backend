using Core.Entities.ProductEntities;

namespace AdminApi.Responses;

public static class ResponseMapping
{
    public static ProductResponse ToProductResponse(this Product product)
    {
        return new ProductResponse()
        {
            Id = product.Id
        };
    }
}