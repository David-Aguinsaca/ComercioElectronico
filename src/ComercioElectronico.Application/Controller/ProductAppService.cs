//paso 9
using AutoMapper;
using ComercioElectronico.Application.Model;
using ComercioElectronico.Application.Repository;
using ComercioElectronico.Domain.Model;
using ComercioElectronico.Domain.Repository;
using FluentValidation;

namespace ComercioElectronico.Application.Controller;

public class ProductAppService : IAppService<ProductDto, ProductCreateUpdateDto, Guid>
{
    private readonly IProductRepository productRepository;
    private readonly IMapper mapper;
    //private readonly IValidator<TypeProductCreateUpdateDto> validator;

    public ProductAppService(IProductRepository productRepository,
    IMapper mapper
    //IValidator<TypeProductCreateUpdateDto> validator
    )
    {
        this.productRepository = productRepository;
        this.mapper = mapper;
        //this.validator = validator;
    }

    public async Task<bool> CreateAsync(ProductCreateUpdateDto entityDto)
    {
        try
        {

            var product =  mapper.Map<Product>(entityDto);
            product = await productRepository.AddAsync(product);

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

            var entity = await productRepository.GetByIdAsync(id);
            if (entity == null)
            {
                throw new ArgumentException($"La marca con la id {id} no existe");
            }

            productRepository.Delete(entity);
            //await typeProductRepository.UnitOfWork.SaveChangesAsync();

            return true;

        }
        catch (System.Exception ex)
        {
            throw new ArgumentException(ex.ToString());
        }
    }

    public ICollection<ProductDto> GetAll()
    {
        try
        {
            var consulta = productRepository.GetAllIncluding(x => x.Brand, x => x.TypeProduct);

            var consultaOrdenDto = from x in consulta
                                   select x;

            var objectListDto = mapper.Map<IEnumerable<ProductDto>>(consultaOrdenDto);

            return objectListDto.ToList();
        }
        catch (System.Exception ex)
        {
            throw new ArgumentException(ex.ToString());

        }

    }

    public async Task<ProductDto> GetByIdAsync(Guid id)
    {
        try
        {
            var consulta =  productRepository.GetAllIncluding(x => x.Brand, x => x.TypeProduct);

            var consultaOrdenDto = from x in consulta
                                   where x.Id == id
                                   select x;

            return  mapper.Map<ProductDto>(consultaOrdenDto.SingleOrDefault());

        }
        catch (System.Exception ex)
        {
            throw new ArgumentException(ex.ToString());

        }
    }

    public Task<ProductDto> Search(string id)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> UpdateAsync(Guid id, ProductCreateUpdateDto entityDto)
    {
        try
        {
            var entity = await productRepository.GetByIdAsync(id);
            var updateEntity = mapper.Map<ProductCreateUpdateDto, Product>(entityDto, entity);
            await productRepository.UpdateAsync(updateEntity);
            return true;
        }
        catch (System.Exception ex)
        {
            throw new ArgumentException(ex.ToString());

        }
    }
}