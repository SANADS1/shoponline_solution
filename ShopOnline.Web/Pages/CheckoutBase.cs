using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using ShopOnline.Models.Dtos;
using ShopOnlineSolution.Web.Services.Contracts;

namespace ShopOnlineSolution.Web.Pages;

public class CheckoutBase : ComponentBase
{
    [Inject]
    public IJSRuntime Js    { get; set; }
    
    protected IEnumerable<CartItemDto> ShoppingCartItems { get; set; }
    
    [Inject]
    public IManageCartItemsLocalStorageService ManageCartItemsLocalStorageService { get; set; }

    protected string DisplayButtons { get; set; } = "block";
    
    protected int TotalQty { get; set; }
    
    protected string PaymentDescription { get; set; }
    
    protected decimal PaymentAmount { get; set; }
    
    [Inject]
    public IShoppingCartService ShoppingCartService { get; set; }

    protected async override void OnInitialized()
    {
        try
        {
            ShoppingCartItems = await ManageCartItemsLocalStorageService.GetCollection();
           if (ShoppingCartItems != null  && ShoppingCartItems.Count() > 0)
           {
               Guid orderGuid = Guid.NewGuid();

               PaymentAmount = ShoppingCartItems.Sum(p => p.TotalPrice);
               TotalQty = ShoppingCartItems.Sum(p => p.Qty);
               PaymentDescription = $"O_{HardCoded.UserId}_{orderGuid}";
           }
           else
           {
               DisplayButtons = "none";
           }

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    protected override async  Task OnAfterRenderAsync(bool firstRender)
    {
        try
        {
            if (firstRender)
            {
                await Js.InvokeVoidAsync("paypal.Buttons");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}