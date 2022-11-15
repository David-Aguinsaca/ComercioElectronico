using System.Text.Json;
using AutoMapper;
using ComercioElectronico.Application.Model;
using ComercioElectronico.Application.Repository;
using ComercioElectronico.Domain.Model;
using ComercioElectronico.Domain.Repository;

namespace ComercioElectronico.Application.Controller;

public class OrderItemAppService : IAppService<OrderItemDto, OrderItemCreateUpdateDto, Guid>
{

    private readonly IOrderItemRepository orderItemRepository;
    private readonly IProductRepository productRepository;
    private readonly IMapper mapper;

    public OrderItemAppService(IOrderItemRepository orderItemRepository, IProductRepository productRepository,
    IMapper mapper)
    {
        this.productRepository = productRepository;
        this.orderItemRepository = orderItemRepository;
        this.mapper = mapper;
    }

    public async Task<bool> CreateAsync(OrderItemCreateUpdateDto entityDto)
    {
        try
        {

            //A: Controlar el registro de una orden exista productos en stock. 

            var producto = await productRepository.GetByIdAsync(entityDto.ProductId);

            if (producto != null)
            {
                if (producto.Stock > 0)
                {
                    //crear orden
                    var orderItem = mapper.Map<OrderItem>(entityDto);
                    orderItem = await orderItemRepository.AddAsync(orderItem);

                    return true;
                }
                throw new ArgumentException($"No hay stock del producto {producto.Name}");

            }
            throw new ArgumentException($"El producto con la id:{entityDto.ProductId} no existe");



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

            var entity = await orderItemRepository.GetByIdAsync(id);
            if (entity == null)
            {
                throw new ArgumentException($"La orden item con la id {id} no existe");
            }

            orderItemRepository.Delete(entity);
            //await typeProductRepository.UnitOfWork.SaveChangesAsync();

            return true;

        }
        catch (System.Exception ex)
        {
            throw new ArgumentException(ex.ToString());
        }
    }

    public ICollection<OrderItemDto> GetAll()
    {
        try
        {
            var consulta = orderItemRepository.GetAllIncluding(x => x.Product);

            var objectListDto = mapper.Map<IEnumerable<OrderItemDto>>(consulta);

            return objectListDto.ToList();
        }
        catch (System.Exception ex)
        {
            throw new ArgumentException(ex.ToString());

        }
    }

    public async Task<OrderItemDto> GetByIdAsync(Guid id)
    {
        try
        {
            var consulta = orderItemRepository.GetAllIncluding(x => x.Product)
            .Where(x=>x.Id == id).SingleOrDefault();

            return mapper.Map<OrderItemDto>(consulta);

        }
        catch (System.Exception ex)
        {
            throw new ArgumentException(ex.ToString());

        }
    }

    public Task<OrderItemDto> Search(string id)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> UpdateAsync(Guid id, OrderItemCreateUpdateDto entityDto)
    {
        try
        {
            var entity = await orderItemRepository.GetByIdAsync(id);
            var updateEntity = mapper.Map<OrderItemCreateUpdateDto, OrderItem>(entityDto, entity);
            await orderItemRepository.UpdateAsync(updateEntity);
            return true;
        }
        catch (System.Exception ex)
        {
            throw new ArgumentException(ex.ToString());

        }
    }
}
