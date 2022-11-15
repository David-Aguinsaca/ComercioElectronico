//paso 9
using AutoMapper;
using ComercioElectronico.Application.Model;
using ComercioElectronico.Application.Repository;
using ComercioElectronico.Domain.Model;
using ComercioElectronico.Domain.Repository;
using FluentValidation;

namespace ComercioElectronico.Application.Controller;

public class TransactionAppService : IAppService<TransactionDto, TransactionCreateUpdateDto, Guid>
{
    private readonly ITransactionRepository transactionRepository;
    private readonly IProductRepository productRepository;

    private readonly IMapper mapper;
    //private readonly IValidator<TypeProductCreateUpdateDto> validator;

    public TransactionAppService(ITransactionRepository TransactionRepository, IProductRepository productRepository,
    IMapper mapper
    //IValidator<TypeProductCreateUpdateDto> validator
    )
    {
        this.productRepository = productRepository;
        this.transactionRepository = TransactionRepository;
        this.mapper = mapper;
        //this.validator = validator;
    }

    public async Task<bool> CreateAsync(TransactionCreateUpdateDto entityDto)
    {
        try
        {

            //A: Controlar el registro de una orden exista productos en stock. 
            var product = mapper.Map<Transaction>(entityDto);
            product = await transactionRepository.AddAsync(product);
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

            var entity = await transactionRepository.GetByIdAsync(id);
            if (entity == null)
            {
                throw new ArgumentException($"La marca con la id {id} no existe");
            }

            transactionRepository.Delete(entity);
            //await typeProductRepository.UnitOfWork.SaveChangesAsync();

            return true;

        }
        catch (System.Exception ex)
        {
            throw new ArgumentException(ex.ToString());
        }
    }

    public ICollection<TransactionDto> GetAll()
    {
        try
        {
            var consultaOrdenDto = transactionRepository.GetAllIncluding(x => x.Client, x => x.Order);

            var objectListDto = mapper.Map<IEnumerable<TransactionDto>>(consultaOrdenDto);

            return objectListDto.ToList();
        }
        catch (System.Exception ex)
        {
            throw new ArgumentException(ex.ToString());

        }

    }

    public async Task<TransactionDto> GetByIdAsync(Guid id)
    {
        try
        {
            var consultaOrdenDto = transactionRepository.GetAllIncluding(x => x.Client, x => x.Order)
            .Where(x => x.Id == id);

            return mapper.Map<TransactionDto>(consultaOrdenDto.SingleOrDefault());

        }
        catch (System.Exception ex)
        {
            throw new ArgumentException(ex.ToString());

        }
    }

    public Task<TransactionDto> Search(string id)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> UpdateAsync(Guid id, TransactionCreateUpdateDto entityDto)
    {
        try
        {
            var entity = await transactionRepository.GetByIdAsync(id);
            var updateEntity = mapper.Map<TransactionCreateUpdateDto, Transaction>(entityDto, entity);
            await transactionRepository.UpdateAsync(updateEntity);
            return true;
        }
        catch (System.Exception ex)
        {
            throw new ArgumentException(ex.ToString());

        }
    }

}