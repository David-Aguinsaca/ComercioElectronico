using ComercioElectronico.Application.Model;
using ComercioElectronico.Application.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ComercioElectronico.HttpApi.Controllers;

[Authorize]

[ApiController]
[Route("api/[controller]")]
public class ShoppingCartItemController : ControllerBase
{
    private readonly IAppService<ShoppingCartItemDto, ShoppingCartItemCreateUpdatetDto, Guid> shoppingCartItemAppService;

    public ShoppingCartItemController(IAppService<ShoppingCartItemDto, ShoppingCartItemCreateUpdatetDto, Guid> shoppingCartItemAppService )
    {
        this.shoppingCartItemAppService = shoppingCartItemAppService;
    }

    [HttpGet("all_shopping_cart_item")]
    public ICollection<ShoppingCartItemDto> GetAll()
    {
        return shoppingCartItemAppService.GetAll();
    }

    [HttpGet("find_shopping_cart_item/{id}")]
    public Task<ShoppingCartItemDto> GetById(Guid id)
    {
        return shoppingCartItemAppService.GetByIdAsync(id);
    }

    [HttpPost("create_shopping_cart_item")]
    public async Task<bool> CreateAsync(ShoppingCartItemCreateUpdatetDto x)
    {
        return await shoppingCartItemAppService.CreateAsync(x);
    }

    [HttpPut("update_shopping_cart_item")]
    public async Task<bool> UpdateAsync(Guid id, ShoppingCartItemCreateUpdatetDto x)
    {
        return await shoppingCartItemAppService.UpdateAsync(id, x);
    }

    [HttpDelete("delete_shopping_cart_item")]
    public async Task<bool> DeleteAsync( Guid id)
    {
        return await shoppingCartItemAppService.DeleteAsync(id);
    }
}
