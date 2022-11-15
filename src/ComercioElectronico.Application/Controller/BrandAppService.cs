//paso 9
using AutoMapper;
using ComercioElectronico.Application.Model;
using ComercioElectronico.Application.Repository;
using ComercioElectronico.Domain.Model;
using ComercioElectronico.Domain.Repository;
using FluentValidation;

namespace ComercioElectronico.Application.Controller;

//public class BrandAppService : IBrandAppService
public class BrandAppService : IAppService<BrandDto, BrandCreateUpdateDto, int>
{
    private readonly IBrandRepository brandRepository;
    private readonly IMapper mapper;
    private readonly IValidator<BrandCreateUpdateDto> validator;

    public BrandAppService(IBrandRepository brandRepository, IMapper mapper,
    IValidator<BrandCreateUpdateDto> validator)
    {
        this.brandRepository = brandRepository;
        this.mapper = mapper;
        this.validator = validator;
    }

    public async Task<bool> CreateAsync(BrandCreateUpdateDto entityDto)
    {
        try
        {
            //await validator.ValidateAndThrowAsync(x);

            var existsName = await brandRepository.ExistsNameAsync(entityDto.Name);

            if (!existsName)
            {
                var brand = mapper.Map<Brand>(entityDto);
                brand = await brandRepository.AddAsync(brand);

                return true;

            }

            throw new ArgumentException($"La marca con el nombre {entityDto.Name} ya existe");


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

            var entity = await brandRepository.GetByIdAsync(id);
            if (entity == null)
            {
                throw new ArgumentException($"La marca con la id {id} no existe");
            }

            brandRepository.Delete(entity);
            await brandRepository.UnitOfWork.SaveChangesAsync();


            return true;

        }
        catch (System.Exception ex)
        {
            throw new ArgumentException(ex.ToString());
        }
    }


    public ICollection<BrandDto> GetAll()
    {
        try
        {
            var objectList = brandRepository.GetAll();

            var objectListDto = mapper.Map<IEnumerable<BrandDto>>(objectList);

            return objectListDto.ToList();
        }
        catch (System.Exception ex)
        {
            throw new ArgumentException(ex.ToString());
        }

    }

    public async Task<BrandDto> GetByIdAsync(int id)
    {

        try
        {
            var brand = await brandRepository.GetByIdAsync(id);

            return mapper.Map<BrandDto>(brand);


        }
        catch (System.Exception ex)
        {
            throw new ArgumentException(ex.ToString());

        }
    }

    public Task<BrandDto> Search(string id)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> UpdateAsync(int id, BrandCreateUpdateDto entityDto)
    {

        try
        {

            var exsiteBrand = await brandRepository.GetByIdAsync(id);

            if (exsiteBrand != null)
            {
                var entity = await brandRepository.GetByIdAsync(id);
                var updateEntity = mapper.Map<BrandCreateUpdateDto, Brand>(entityDto, entity);
                await brandRepository.UpdateAsync(updateEntity);
                return true;
            }
            throw new ArgumentException($"EL usuario con el identificador {id} no existe");


        }
        catch (System.Exception ex)
        {
            throw new ArgumentException(ex.ToString());

        }

    }
}
