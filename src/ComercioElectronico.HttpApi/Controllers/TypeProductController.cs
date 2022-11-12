using ComercioElectronico.Application.Model;
using ComercioElectronico.Application.Repository;
using Microsoft.AspNetCore.Mvc;

namespace ComercioElectronico.HttpApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TypeProductController : ControllerBase
{
    private readonly IAppService<TypeProductDto, TypeProductCreateUpdateDto> typeProductAppService;

    public TypeProductController(IAppService<TypeProductDto, TypeProductCreateUpdateDto> typeProductAppService )
    {
        this.typeProductAppService = typeProductAppService;
    }

    [HttpGet("all_typeProduct")]
    public ICollection<TypeProductDto> GetAll()
    {
        return typeProductAppService.GetAll();
    }

    [HttpGet("find_typeProduct/{id}")]
    public Task<TypeProductDto> GetById(int id)
    {
        return typeProductAppService.GetByIdAsync(id);
    }

    [HttpPost("create_typeProduct")]
    public async Task<bool> CreateAsync(TypeProductCreateUpdateDto x)
    {
        return await typeProductAppService.CreateAsync(x);
    }

    [HttpPut("update_typeProduct")]
    public async Task<bool> UpdateAsync(int id, TypeProductCreateUpdateDto x)
    {
        return await typeProductAppService.UpdateAsync(id, x);
    }

    [HttpDelete("delete_typeProduct")]
    public async Task<bool> DeleteAsync(int id)
    {
        return await typeProductAppService.DeleteAsync(id);
    }
}
