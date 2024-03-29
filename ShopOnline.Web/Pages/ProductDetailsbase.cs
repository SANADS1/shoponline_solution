using Microsoft.AspNetCore.Components;
using ShopOnline.Models.Dtos;
using ShopOnlineSolution.Web.Services.Contracts;

namespace ShopOnlineSolution.Web.Pages;

public class ProductDetailsbase : ComponentBase
{ 
    [Parameter]
    public int Id { get; set; }
    
    [Inject]
    public IProductService ProductService { get; set; }
    
    [Inject]
    public IShoppingCartService ShoppingCartService { get; set; }
    
    [Inject]
    public IManageProductsLocalStorageService ManageProductsLocalStorageService { get; set; }
    
    [Inject]
    public IManageCartItemsLocalStorageService ManageCartItemsLocalStorageService  { get; set; }
    
    public ProductDto Product { get; set; }
    
    public string ErrorMessage { get; set; }
    
    private List<CartItemDto> ShoppingCartItems { get; set; }

    [Inject]
    public NavigationManager NavigationManager { get; set; }

    protected override async Task OnInitializedAsync()
    { 
        try
        {
            ShoppingCartItems = await ManageCartItemsLocalStorageService.GetCollection();
            Product = await ProductService.GetItem(Id);
        }
        catch (Exception e)
        {
            ErrorMessage = e.Message;
        }
    }

    protected async Task AddToCart_Click(CartItemToAddDto cartItemToAddDto)
    {
        try
        {
            var cartItemDto = await ShoppingCartService.AddItem(cartItemToAddDto);

            if (cartItemDto != null)
            {
                ShoppingCartItems.Add(cartItemDto);
                await ManageCartItemsLocalStorageService.SaveCollection(ShoppingCartItems);
            }
            NavigationManager.NavigateTo("/ShoppingCart");
        }
        catch (Exception e)
        {
            //Log Exception
            throw;
        }
    }
    
    private async Task<ProductDto> GetProductById(int id)
    {
        var productDtos = await ManageProductsLocalStorageService.GetCollection();

        if(productDtos!=null)
        {
            return productDtos.SingleOrDefault(p => p.Id == id);
        }
        return null;
    }
}