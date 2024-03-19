using ShopOnline.Models.Dtos;

namespace ShopOnlineSolution.Web.Services.Contracts;

public interface IShoppingCartService
{
    Task<List<CartItemDto>> GetItems(int userId);
    Task<CartItemDto> AddItem(CartItemToAddDto cartItemToAddDto);
    Task<CartItemDto> DeleteItem(int Id);
    Task<CartItemDto> UpdateQty(CartItemQtyUpdateDto cartItemQtyUpdate);
    event Action<int> OnShoppingCartChanged;
    void RaiseEventOnShoppingCartChanged(int totalQty);
}