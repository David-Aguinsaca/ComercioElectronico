//paso 9
using AutoMapper;
using ComercioElectronico.Application.Model;
using ComercioElectronico.Application.Repository;
using ComercioElectronico.Domain.Model;
using ComercioElectronico.Domain.Repository;
using FluentValidation;

namespace ComercioElectronico.Application.Controller;

public class ShoppingCartItemAppService : IAppService<ShoppingCartItemDto, ShoppingCartItemCreateUpdatetDto, Guid>
{
    private readonly IShoppingCartItemRepository shoppingCartRepository;
    private readonly IProductRepository productRepository;

    private readonly IMapper mapper;
    //private readonly IValidator<TypeProductCreateUpdateDto> validator;

    public ShoppingCartItemAppService(IShoppingCartItemRepository shoppingCartRepository, IProductRepository productRepository,
    IMapper mapper
    //IValidator<TypeProductCreateUpdateDto> validator
    )
    {
        this.productRepository = productRepository;
        this.shoppingCartRepository = shoppingCartRepository;
        this.mapper = mapper;
        //this.validator = validator;
    }

    public async Task<bool> CreateAsync(ShoppingCartItemCreateUpdatetDto entityDto)
    {
        try
        {

            //A: Controlar el registro de una orden exista productos en stock. 

            var producto = await productRepository.GetByIdAsync(entityDto.ProductId);

            if (producto != null)
            {
                if (producto.Stock > 0)
                {
                    var product = mapper.Map<ShoppingCartItem>(entityDto);
                    product = await shoppingCartRepository.AddAsync(product);
                    return true;
                }
                throw new ArgumentException($"No hay stock del producto {producto.Name}");

            }
            throw new ArgumentException($"El producto con la id:{entityDto.ProductId}");



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

            var entity = await shoppingCartRepository.GetByIdAsync(id);
            if (entity == null)
            {
                throw new ArgumentException($"La marca con la id {id} no existe");
            }

            shoppingCartRepository.Delete(entity);
            //await typeProductRepository.UnitOfWork.SaveChangesAsync();

            return true;

        }
        catch (System.Exception ex)
        {
            throw new ArgumentException(ex.ToString());
        }
    }

    public ICollection<ShoppingCartItemDto> GetAll()
    {
        try
        {
            var consulta = shoppingCartRepository.GetAllIncluding(x => x.Product);

            var consultaOrdenDto = from x in consulta
                                   select x;

            var objectListDto = mapper.Map<IEnumerable<ShoppingCartItemDto>>(consultaOrdenDto);

            return objectListDto.ToList();
        }
        catch (System.Exception ex)
        {
            throw new ArgumentException(ex.ToString());

        }

    }

    public async Task<ShoppingCartItemDto> GetByIdAsync(Guid id)
    {
        try
        {
            var consulta = shoppingCartRepository.GetAllIncluding(x => x.Product);

            var consultaOrdenDto = from x in consulta
                                   where x.Id == id
                                   select x;

            return mapper.Map<ShoppingCartItemDto>(consultaOrdenDto.SingleOrDefault());

        }
        catch (System.Exception ex)
        {
            throw new ArgumentException(ex.ToString());

        }
    }

    public async Task<bool> UpdateAsync(Guid id, ShoppingCartItemCreateUpdatetDto entityDto)
    {
        try
        {
             var entity = await shoppingCartRepository.GetByIdAsync(id);
             var updateEntity = mapper.Map<ShoppingCartItemCreateUpdatetDto, ShoppingCartItem>(entityDto, entity);
             await shoppingCartRepository.UpdateAsync(updateEntity);
            return true;
        }
        catch (System.Exception ex)
        {
            throw new ArgumentException(ex.ToString());

        }
    }

}