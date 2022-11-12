using ComercioElectronico.Application.Model;
using ComercioElectronico.Application.Repository;
using Microsoft.AspNetCore.Mvc;

namespace ComercioElectronico.HttpApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private readonly IAppService<ProductDto, ProductCreateUpdateDto, Guid> productAppService;

    public ProductController(IAppService<ProductDto, ProductCreateUpdateDto, Guid> productAppService )
    {
        this.productAppService = productAppService;
    }

    [HttpGet("all_product")]
    public ICollection<ProductDto> GetAll()
    {
        return productAppService.GetAll();
    }

    [HttpGet("find_product/{id}")]
    public Task<ProductDto> GetById(Guid id)
    {
        return productAppService.GetByIdAsync(id);
    }

    [HttpPost("create_product")]
    public async Task<bool> CreateAsync(ProductCreateUpdateDto x)
    {
        return await productAppService.CreateAsync(x);
    }

    [HttpPut("update_product")]
    public async Task<bool> UpdateAsync(Guid id, ProductCreateUpdateDto x)
    {
        return await productAppService.UpdateAsync(id, x);
    }

    [HttpDelete("delete_product")]
    public async Task<bool> DeleteAsync( Guid id)
    {
        return await productAppService.DeleteAsync(id);
    }
}
