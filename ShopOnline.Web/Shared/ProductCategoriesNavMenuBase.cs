using Microsoft.AspNetCore.Components;
using ShopOnline.Models.Dtos;
using ShopOnlineSolution.Web.Services.Contracts;

namespace ShopOnlineSolution.Web.Shared;

public class ProductCategoriesNavMenuBase : ComponentBase
{
    [Inject] 
    public IProductService ProductService { get; set; }
    
    public IEnumerable<ProductCategoryDto> ProductCategoryDtos { get; set; }
    
    public string ErrorMessage { get; set; }

    protected override async  Task OnInitializedAsync()
    {
        try
        {
            ProductCategoryDtos = await ProductService.GetProductCategories();
        }
        catch (Exception e)
        {
            ErrorMessage = e.Message;
        }
    }
}