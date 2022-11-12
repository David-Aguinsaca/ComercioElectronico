//paso 9
using AutoMapper;
using ComercioElectronico.Application.Model;
using ComercioElectronico.Application.Repository;
using ComercioElectronico.Domain.Model;
using ComercioElectronico.Domain.Repository;
using FluentValidation;

namespace ComercioElectronico.Application.Controller;

public class TypeProductAppService : IAppService<TypeProductDto, TypeProductCreateUpdateDto>
{
    private readonly ITypeProductRepository typeProductRepository;
    private readonly IMapper mapper;
    private readonly IValidator<TypeProductCreateUpdateDto> validator;

    public TypeProductAppService(ITypeProductRepository typeProductRepository, IMapper mapper,
    IValidator<TypeProductCreateUpdateDto> validator)
    {
        this.typeProductRepository = typeProductRepository;
        this.mapper = mapper;
        this.validator = validator;
    }

    public async Task<bool> CreateAsync(TypeProductCreateUpdateDto entityDto)
    {
        try
        {
            //await validator.ValidateAndThrowAsync(x);

            var typeProduct = mapper.Map<TypeProduct>(entityDto);
            typeProduct = await typeProductRepository.AddAsync(typeProduct);

            return true;
        }
        catch (System.Exception ex)
        {
            throw new ArgumentException(ex.ToString());
        }
    }

    public async Task<bool> DeleteAsync(int id)
    {
        try
        {

            var entity = await typeProductRepository.GetByIdAsync(id);
            if (entity == null)
            {
                throw new ArgumentException($"La marca con la id {id} no existe");
            }

            typeProductRepository.Delete(entity);
            //await typeProductRepository.UnitOfWork.SaveChangesAsync();

            return true;

        }
        catch (System.Exception ex)
        {
            throw new ArgumentException(ex.ToString());
        }
    }

    public ICollection<TypeProductDto> GetAll()
    {
        try
        {
            var objectList = typeProductRepository.GetAll();

            var objectListDto = mapper.Map<IEnumerable<TypeProductDto>>(objectList);

            return objectListDto.ToList();
        }
        catch (System.Exception ex)
        {
            throw new ArgumentException(ex.ToString());

        }

    }

    public async Task<TypeProductDto> GetByIdAsync(int id)
    {
        try
        {
            var typeProduct = await typeProductRepository.GetByIdAsync(id);
            return mapper.Map<TypeProductDto>(typeProduct);
        }
        catch (System.Exception ex)
        {
            throw new ArgumentException(ex.ToString());

        }
    }

    public async Task<bool> UpdateAsync(int id, TypeProductCreateUpdateDto entityDto)
    {
        try
        {
            var entity = await typeProductRepository.GetByIdAsync(id);
            var updateEntity = mapper.Map<TypeProductCreateUpdateDto, TypeProduct>(entityDto, entity);
            await typeProductRepository.UpdateAsync(updateEntity);
            return true;
        }
        catch (System.Exception ex)
        {
            throw new ArgumentException(ex.ToString());

        }
    }
}