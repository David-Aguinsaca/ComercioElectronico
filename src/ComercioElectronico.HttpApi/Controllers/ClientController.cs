using ComercioElectronico.Application.Controller;
using ComercioElectronico.Application.Model;
using ComercioElectronico.Application.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ComercioElectronico.HttpApi.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class ClientController : ControllerBase
{
    private readonly IAppService<ClientDto, ClientCreateUpdateDto, Guid> clientAppService;

    public ClientController(IAppService<ClientDto, ClientCreateUpdateDto, Guid> clientAppService )
    {
        this.clientAppService = clientAppService;
    }

    [HttpGet("all_client")]
    public ICollection<ClientDto> GetAll()
    {
        return clientAppService.GetAll();
    }

    [HttpGet("find_client/{id}")]
    public Task<ClientDto> GetById(Guid id)
    {
        return clientAppService.GetByIdAsync(id);
    }

    [HttpPost("create_client")]
    public async Task<bool> CreateAsync(ClientCreateUpdateDto x)
    {
        return await clientAppService.CreateAsync(x);
    }

    [HttpPut("update_client")]
    public async Task<bool> UpdateAsync(Guid id, ClientCreateUpdateDto x)
    {
        return await clientAppService.UpdateAsync(id, x);
    }

    [HttpDelete("delete_client")]
    public async Task<bool> DeleteAsync(Guid id)
    {
        return await clientAppService.DeleteAsync(id);
    }

    [HttpGet("sarch_client/{search}")]
    public Task<ClientDto> SearchClient(string search)
    {
        return clientAppService.Search(search);
    }
}