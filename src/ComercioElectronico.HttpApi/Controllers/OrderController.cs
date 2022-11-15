using ComercioElectronico.Application.Model;
using ComercioElectronico.Application.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ComercioElectronico.HttpApi.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class OrderController : ControllerBase
{
    private readonly IAppService<OrderDto, OrderCreateUpdateDto, Guid> orderAppService;

    public OrderController(IAppService<OrderDto, OrderCreateUpdateDto, Guid> orderAppService )
    {
        this.orderAppService = orderAppService;
    }

    [HttpGet("all_order")]
    public ICollection<OrderDto> GetAll()
    {
        return orderAppService.GetAll();
    }

    [HttpGet("find_order/{id}")]
    public Task<OrderDto> GetById(Guid id)
    {
        return orderAppService.GetByIdAsync(id);
    }

    [HttpPost("create_order")]
    public async Task<bool> CreateAsync(OrderCreateUpdateDto x)
    {
        return await orderAppService.CreateAsync(x);
    }

    [HttpPut("update_order")]
    public async Task<bool> UpdateAsync(Guid id, OrderCreateUpdateDto x)
    {
        return await orderAppService.UpdateAsync(id, x);
    }

    [HttpDelete("delete_order")]
    public async Task<bool> DeleteAsync( Guid id)
    {
        return await orderAppService.DeleteAsync(id);
    }
}
