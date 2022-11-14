using System.Text.Json;
using AutoMapper;
using ComercioElectronico.Application.Model;
using ComercioElectronico.Application.Repository;
using ComercioElectronico.Domain.Model;
using ComercioElectronico.Domain.Repository;

namespace ComercioElectronico.Application.Controller;

public class OrderAppService : IAppService<OrderDto, OrderCreateUpdateDto, Guid>
{
    private readonly IOrderRepository orderRepository;
    private readonly IProductRepository productRepository;
    private readonly IClientRepository clientRepository;
    private readonly IMapper mapper;


    public OrderAppService(IOrderRepository orderRepository,
    IProductRepository productRepository, IClientRepository clientRepository,
    IMapper mapper
    )
    {
        this.clientRepository = clientRepository;
        this.productRepository = productRepository;
        this.orderRepository = orderRepository;

        this.mapper = mapper;

    }

    public async Task<bool> CreateAsync(OrderCreateUpdateDto entityDto)
    {
        try
        {

            var product = mapper.Map<Order>(entityDto);
            product = await orderRepository.AddAsync(product);

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

            var entity = await orderRepository.GetByIdAsync(id);
            if (entity == null)
            {
                throw new ArgumentException($"La orden con la id {id} no existe");
            }

            orderRepository.Delete(entity);
            //await typeProductRepository.UnitOfWork.SaveChangesAsync();

            return true;

        }
        catch (System.Exception ex)
        {
            throw new ArgumentException(ex.ToString());
        }
    }

    public ICollection<OrderDto> GetAll()
    {
       try
        {
            var consulta = orderRepository.GetAllIncluding(x => x.Client, x => x.OrderItems);
            /* var jsonString = JsonSerializer.Serialize(consulta);
            throw new Exception(jsonString); */
            //var consulta = shoppingCartRepository.GetAll();

            var objectListDto = mapper.Map<IEnumerable<OrderDto>>(consulta);

            return objectListDto.ToList();
        }
        catch (System.Exception ex)
        {
            throw new ArgumentException(ex.ToString());

        };
    }

    public async Task<OrderDto> GetByIdAsync(Guid id)
    {
       try
        {
            var consulta = orderRepository.GetAllIncluding(x => x.Client, x => x.OrderItems);

            var consultaOrdenDto = from x in consulta
                                   where x.Id == id
                                   select x;

            return mapper.Map<OrderDto>(consultaOrdenDto.SingleOrDefault());

        }
        catch (System.Exception ex)
        {
            throw new ArgumentException(ex.ToString());

        }
    }

    public async Task<bool> UpdateAsync(Guid id, OrderCreateUpdateDto entityDto)
    {
         try
        {
            var entity = await orderRepository.GetByIdAsync(id);
            var updateEntity = mapper.Map<OrderCreateUpdateDto, Order>(entityDto, entity);
            await orderRepository.UpdateAsync(updateEntity);

            return true;
        }
        catch (System.Exception ex)
        {
            throw new ArgumentException(ex.ToString());

        }
    }
}
