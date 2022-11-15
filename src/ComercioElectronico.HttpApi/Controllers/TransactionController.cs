using ComercioElectronico.Application.Model;
using ComercioElectronico.Application.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ComercioElectronico.HttpApi.Controllers;

[Authorize]

[ApiController]
[Route("api/[controller]")]
public class TransactionController : ControllerBase
{

    private readonly IAppService<TransactionDto, TransactionCreateUpdateDto, Guid> transactionAppService;

    public TransactionController(IAppService<TransactionDto, TransactionCreateUpdateDto, Guid> transactionAppService )
    {
        this.transactionAppService = transactionAppService;
    }

    [HttpGet("all_transaction")]
    public ICollection<TransactionDto> GetAll()
    {
        return transactionAppService.GetAll();
    }

    [HttpGet("find_transaction/{id}")]
    public Task<TransactionDto> GetById(Guid id)
    {
        return transactionAppService.GetByIdAsync(id);
    }

    [HttpPost("create_transaction")]
    public async Task<bool> CreateAsync(TransactionCreateUpdateDto x)
    {
        return await transactionAppService.CreateAsync(x);
    }

    [HttpPut("update_transaction")]
    public async Task<bool> UpdateAsync(Guid id, TransactionCreateUpdateDto x)
    {
        return await transactionAppService.UpdateAsync(id, x);
    }

    [HttpDelete("delete_transaction")]
    public async Task<bool> DeleteAsync( Guid id)
    {
        return await transactionAppService.DeleteAsync(id);
    }
}
