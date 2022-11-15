using ComercioElectronico.Application.Model;
using ComercioElectronico.Application.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ComercioElectronico.HttpApi.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class OrderItemController : ControllerBase
{
    private readonly IAppService<OrderItemDto, OrderItemCreateUpdateDto, Guid> orderItemAppService;

    public OrderItemController(IAppService<OrderItemDto, OrderItemCreateUpdateDto, Guid> orderItemAppService )
    {
        this.orderItemAppService = orderItemAppService;
    }

    [HttpGet("all_order_item")]
    public ICollection<OrderItemDto> GetAll()
    {
        return orderItemAppService.GetAll();
    }

    [HttpGet("find_order_item/{id}")]
    public Task<OrderItemDto> GetById(Guid id)
    {
        return orderItemAppService.GetByIdAsync(id);
    }

    [HttpPost("create_order_item")]
    public async Task<bool> CreateAsync(OrderItemCreateUpdateDto x)
    {
        return await orderItemAppService.CreateAsync(x);
    }

    [HttpPut("update_order_item")]
    public async Task<bool> UpdateAsync(Guid id, OrderItemCreateUpdateDto x)
    {
        return await orderItemAppService.UpdateAsync(id, x);
    }

    [HttpDelete("delete_order_item")]
    public async Task<bool> DeleteAsync( Guid id)
    {
        return await orderItemAppService.DeleteAsync(id);
    }
}
