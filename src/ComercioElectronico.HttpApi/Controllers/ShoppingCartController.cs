using ComercioElectronico.Application.Model;
using ComercioElectronico.Application.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ComercioElectronico.HttpApi.Controllers;

[Authorize]

[ApiController]
[Route("api/[controller]")]
public class ShoppingCartController : ControllerBase
{
    private readonly IAppService<ShoppingCartDto, ShoppingCartCreateUpdatetDto, Guid> shoppingCartAppService;

    public ShoppingCartController(IAppService<ShoppingCartDto, ShoppingCartCreateUpdatetDto, Guid> shoppingCartAppService )
    {
        this.shoppingCartAppService = shoppingCartAppService;
    }

    [HttpGet("all_shopping_cart")]
    public ICollection<ShoppingCartDto> GetAll()
    {
        return shoppingCartAppService.GetAll();
    }

    [HttpGet("find_shopping_cart/{id}")]
    public Task<ShoppingCartDto> GetById(Guid id)
    {
        return shoppingCartAppService.GetByIdAsync(id);
    }

    [HttpPost("create_shopping_cart")]
    public async Task<bool> CreateAsync(ShoppingCartCreateUpdatetDto x)
    {
        return await shoppingCartAppService.CreateAsync(x);
    }

    [HttpPut("update_shopping_cart")]
    public async Task<bool> UpdateAsync(Guid id, ShoppingCartCreateUpdatetDto x)
    {
        return await shoppingCartAppService.UpdateAsync(id, x);
    }

    [HttpDelete("delete_shopping_cart")]
    public async Task<bool> DeleteAsync( Guid id)
    {
        return await shoppingCartAppService.DeleteAsync(id);
    }
}
