using ComercioElectronico.Application.Controller;
using ComercioElectronico.Application.Model;
using ComercioElectronico.Application.Repository;
using Microsoft.AspNetCore.Mvc;

namespace ComercioElectronico.HttpApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BrandController : ControllerBase
{
    private readonly IAppService<BrandDto, BrandCreateUpdateDto, int> brandAppService;

    public BrandController(IAppService<BrandDto, BrandCreateUpdateDto, int> brandAppService )
    {
        this.brandAppService = brandAppService;
    }

    [HttpGet("all_brand")]
    public ICollection<BrandDto> GetAll()
    {
        return brandAppService.GetAll();
    }

    [HttpGet("find_brand/{id}")]
    public Task<BrandDto> GetById(int id)
    {
        return brandAppService.GetByIdAsync(id);
    }

    [HttpPost("create_brand")]
    public async Task<bool> CreateAsync(BrandCreateUpdateDto x)
    {
        return await brandAppService.CreateAsync(x);
    }

    [HttpPut("update_brand")]
    public async Task<bool> UpdateAsync(int id, BrandCreateUpdateDto x)
    {
        return await brandAppService.UpdateAsync(id, x);
    }

    [HttpDelete("delete_brand")]
    public async Task<bool> DeleteAsync(int id)
    {
        return await brandAppService.DeleteAsync(id);
    }
}
