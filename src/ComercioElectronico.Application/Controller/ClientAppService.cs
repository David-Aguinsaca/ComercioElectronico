//paso 9
using AutoMapper;
using ComercioElectronico.Application.Model;
using ComercioElectronico.Application.Repository;
using ComercioElectronico.Domain.Model;
using ComercioElectronico.Domain.Repository;
using FluentValidation;

namespace ComercioElectronico.Application.Controller;

public class ClientAppService : IAppService<ClientDto, ClientCreateUpdateDto, Guid>
{
    private readonly IClientRepository clientRepository;
    private readonly IMapper mapper;
    //private readonly IValidator<TypeProductCreateUpdateDto> validator;

    public ClientAppService(IClientRepository clientRepository,
    IMapper mapper
    //IValidator<TypeProductCreateUpdateDto> validator
    )
    {
        this.clientRepository = clientRepository;
        this.mapper = mapper;
        //this.validator = validator;
    }

    public async Task<bool> CreateAsync(ClientCreateUpdateDto entityDto)
    {
        try
        {

            var product = mapper.Map<Client>(entityDto);
            product = await clientRepository.AddAsync(product);

            return true;
        }
        catch (System.Exception ex)
        {
            throw new ArgumentException(ex.ToString());
        }
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        try
        {

            var entity = await clientRepository.GetByIdAsync(id);
            if (entity == null)
            {
                throw new ArgumentException($"La marca con la id {id} no existe");
            }

            clientRepository.Delete(entity);
            //await typeclientRepository.UnitOfWork.SaveChangesAsync();

            return true;

        }
        catch (System.Exception ex)
        {
            throw new ArgumentException(ex.ToString());
        }
    }

    public ICollection<ClientDto> GetAll()
    {
        try
        {
            var consulta = clientRepository.GetAll();

            var objectListDto = mapper.Map<IEnumerable<ClientDto>>(consulta);

            return objectListDto.ToList();
        }
        catch (System.Exception ex)
        {
            throw new ArgumentException(ex.ToString());

        }

    }

    public async Task<ClientDto> GetByIdAsync(Guid id)
    {
        try
        {
            var consulta = await clientRepository.GetByIdAsync(id);

            return mapper.Map<ClientDto>(consulta);

        }
        catch (System.Exception ex)
        {
            throw new ArgumentException(ex.ToString());

        }
    }

    public async Task<bool> UpdateAsync(Guid id, ClientCreateUpdateDto entityDto)
    {
        try
        {
            var entity = await clientRepository.GetByIdAsync(id);
            var updateEntity = mapper.Map<ClientCreateUpdateDto, Client>(entityDto, entity);
            await clientRepository.UpdateAsync(updateEntity);
            return true;
        }
        catch (System.Exception ex)
        {
            throw new ArgumentException(ex.ToString());

        }
    }

}