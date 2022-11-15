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

public class ShoppingCartAppService : IAppService<ShoppingCartDto, ShoppingCartCreateUpdatetDto, Guid>
{

    private readonly IConfiguration iconfiguration;
    private readonly IOptions<AppSetting> ioption;
    private readonly IOrderRepository orderRepository;
    private readonly IShoppingCartRepository shoppingCartRepository;
    private readonly IProductRepository productRepository;
    private readonly IClientRepository clientRepository;

    private readonly IMapper mapper;
    //private readonly IValidator<TypeProductCreateUpdateDto> validator;

    public ShoppingCartAppService(
        IOrderRepository orderRepository,
        IShoppingCartRepository shoppingCartRepository,
        IProductRepository productRepository, IClientRepository clientRepository, 
        IConfiguration iconfiguration, IOptions<AppSetting> ioption,
        IMapper mapper
    )
    {
        this.clientRepository = clientRepository;
        this.productRepository = productRepository;
        this.orderRepository = orderRepository;
        this.shoppingCartRepository = shoppingCartRepository;

        this.iconfiguration = iconfiguration;
        this.ioption = ioption;

        this.mapper = mapper;
        //this.validator = validator;
    }

    public async Task<bool> CreateAsync(ShoppingCartCreateUpdatetDto entityDto)
    {
        try
        {

            var product = mapper.Map<ShoppingCart>(entityDto);
            product = await shoppingCartRepository.AddAsync(product);

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

    public ICollection<ShoppingCartDto> GetAll()
    {
        try
        {
            var consulta = shoppingCartRepository.GetAllIncluding(x => x.Client, x => x.ShoppingCarItems);

            var objectListDto = mapper.Map<IEnumerable<ShoppingCartDto>>(consulta);

            return objectListDto.ToList();
        }
        catch (System.Exception ex)
        {
            throw new ArgumentException(ex.ToString());

        }

    }

    public async Task<ShoppingCartDto> GetByIdAsync(Guid id)
    {
        try
        {
            var consulta = shoppingCartRepository.GetAllIncluding(x => x.Client, x => x.ShoppingCarItems)
            .Where(x => x.Id == id).SingleOrDefault();

            if (consulta != null)
            {
                return mapper.Map<ShoppingCartDto>(consulta);

            }

            throw new ArgumentException($"El usuario con el identificador {id} no esta registrado en la base de datos");

        }
        catch (System.Exception ex)
        {
            throw new ArgumentException(ex.ToString());

        }
    }

    public Task<ShoppingCartDto> Search(string search)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> UpdateAsync(Guid id, ShoppingCartCreateUpdatetDto entityDto)
    {
        try
        {

            //var entity = await shoppingCartRepository.GetByIdAsync(id);
            var entity = shoppingCartRepository.GetAllIncluding(x => x.Client, x => x.ShoppingCarItems)
            .Where(x => x.Id == id).Single();


            switch (entityDto.StatusShoppingCart)
            {
                //B. Cancelar carrito de compras, eliminar carrito de compras
                case StatusShoppingCart.Anulada:

                    shoppingCartRepository.Delete(entity);

                    break;
                case StatusShoppingCart.Procesar:

                    var updateEntity = mapper.Map<ShoppingCartCreateUpdatetDto, ShoppingCart>(entityDto, entity);

                    await shoppingCartRepository.UpdateAsync(updateEntity);

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

                    foreach (var item in entity.ShoppingCarItems)
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

                    var updateEntityShoppingCart = mapper.Map<ShoppingCartCreateUpdatetDto, ShoppingCart>(entityDto, entity);

                    //actualizar carrito de compras
                    await shoppingCartRepository.UpdateAsync(updateEntityShoppingCart);

                    //orden
                    //var updateEntityOrderCart = mapper.Map<OrderCreateUpdateDto, Order>(entityDto, entity);
                    //await orderRepository.AddAsync(updateEntityOrderCart);

                    break;

            }


            return true;
        }
        catch (System.Exception ex)
        {
            throw new ArgumentException(ex.ToString());

        }
    }



}