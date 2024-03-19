using ShopOnline.Models.Dtos;

namespace ShopOnlineSolution.Web.Services.Contracts;

public interface IManageProductsLocalStorageService
{
    Task<IEnumerable<ProductDto>> GetCollection();
    Task RemoveCollection();
}