//paso 9
using System.Text.Json;
using AutoMapper;
using ComercioElectronico.Application.Model;
using ComercioElectronico.Application.Repository;
using ComercioElectronico.Domain;
using ComercioElectronico.Domain.Model;
using ComercioElectronico.Domain.Repository;
using ComercioElectronico.HttpApi.Model;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace ComercioElectronico.Application.Controller;

public class OrderAppService : IAppService<OrderDto, OrderCreateUpdateDto, Guid>
{
    private readonly IOrderRepository orderRepository;
    private readonly IProductRepository productRepository;
    private readonly IClientRepository clientRepository;
    private readonly IMapper mapper;

    private readonly IConfiguration iconfiguration;
    private readonly IOptions<AppSetting> ioption;


    public OrderAppService(IOrderRepository orderRepository,
    IProductRepository productRepository, IClientRepository clientRepository,
    IMapper mapper,
    IConfiguration iconfiguration, IOptions<AppSetting> ioption
    )
    {
        this.clientRepository = clientRepository;
        this.productRepository = productRepository;
        this.orderRepository = orderRepository;

        this.iconfiguration = iconfiguration;
        this.ioption = ioption;

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
            var consulta = orderRepository.GetAllIncluding(x => x.Client, x => x.OrderItems)
            .Where(x => x.Id == id).SingleOrDefault();

            if (consulta != null)
            {
                return mapper.Map<OrderDto>(consulta);

            }

            throw new ArgumentException($"La orden con el identidicador {id} no existe");

        }
        catch (System.Exception ex)
        {
            throw new ArgumentException(ex.ToString());

        }
    }

    public Task<OrderDto> Search(string id)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> UpdateAsync(Guid id, OrderCreateUpdateDto entityDto)
    {

        try
        {

            var entity = orderRepository.GetAllIncluding(x => x.Client, x => x.OrderItems)
            .Where(x => x.Id == id).Single();

            if (entity != null)
            {
                switch (entityDto.StatusShoppingCart)
                {
                    //B. Cancelar carrito de compras, cambiar de estado

                    case StatusShoppingCart.Anulada:

                        var updateEntity = mapper.Map<OrderCreateUpdateDto, Order>(entityDto, entity);

                        foreach (var item in entity.OrderItems)
                        {

                            var productDto = new ProductCreateUpdateDto();

                            var productUpdate = await productRepository.GetByIdAsync(item.ProductId);
                            productDto.Stock = productUpdate.Stock + item.Product.Stock;
                            var productMap = mapper.Map<ProductCreateUpdateDto, Product>(productDto, productUpdate);
                            await productRepository.UpdateAsync(productMap);
                        }

                        await orderRepository.UpdateAsync(updateEntity);

                        break;
                    case StatusShoppingCart.Confirmar:
                        //C. Calcular los impuestos de la orden a todos los productos que posee la propiedad que contienen impuestos, 
                        //el porcentaje del impuesto debe ser una configuracion en appsetting.

                        var appSetting = new AppSetting();

                        appSetting.Iva = iconfiguration.GetValue<double>("Impuesto:Iva:Percentage");


                        /*
                        **
                        Nota: se detectó un posible ciclo de objeto. Esto se debe a un ciclo o si la profundidad del 
                        objeto es mayor que la profundidad máxima permitida 

                        var totalIva = entity.ShoppingCarItems.Where(x => x.Product.IsIva)
                        .Sum(x => (x.Quantity * x.Product.Value) * (appSetting.Iva / 100)); 
                        var subTotal = entity.ShoppingCarItems.Sum(x => (x.Quantity * x.Product.Value));
                        **
                        */

                        decimal totalIva = 0.00M;
                        decimal subTotal = 0.00M;

                        foreach (var item in entity.OrderItems)
                        {
                            var producto = await productRepository.GetByIdAsync(item.ProductId);
                            if (producto.IsIva)
                            {
                                totalIva = +(item.Quantity * producto.Value) * ((decimal)appSetting.Iva / 100);
                            }
                            subTotal = +item.Quantity * producto.Value;
                        }

                        var totalValue = Math.Round(totalIva + subTotal, 2);

                        //D. Calcular descuentos en la orden, segun una propiedad del cliente en el cual contiene el % del descuento. 

                        var totalDescuentoClien = totalValue - (totalValue * ((decimal)entity.Client.Discount / 100));


                        entity.TotalIva = Math.Round(totalIva, 2);
                        entity.SubTotalValue = Math.Round(subTotal, 2);
                        entity.TotalValue = Math.Round(totalDescuentoClien, 2);

                        var updateEntityShoppingCart = mapper.Map<OrderCreateUpdateDto, Order>(entityDto, entity);

                        //actualizar carrito de compras
                        await orderRepository.UpdateAsync(updateEntityShoppingCart);

                        //orden
                        //var updateEntityOrderCart = mapper.Map<OrderCreateUpdateDto, Order>(entityDto, entity);
                        //await orderRepository.AddAsync(updateEntityOrderCart);

                        break;

                }


                return true;
            }

            throw new ArgumentException($"La orden con el identificador {id} se encuentra");

        }
        catch (System.Exception ex)
        {
            throw new ArgumentException(ex.ToString());

        }




    }


}
/* try
{

    var entity = await orderRepository.GetByIdAsync(id);
    var updateEntity = mapper.Map<OrderCreateUpdateDto, Order>(entityDto, entity);
    await orderRepository.UpdateAsync(updateEntity);

    return true;
}
catch (System.Exception ex)
{
    throw new ArgumentException(ex.ToString());

} */
