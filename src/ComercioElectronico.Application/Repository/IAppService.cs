namespace ComercioElectronico.Application.Repository;

public interface IAppService <ModelDto, ModelCreateUpdateDto, TypeId>
{
    ICollection<ModelDto> GetAll();

    Task<bool> CreateAsync(ModelCreateUpdateDto entityDto);

    Task<bool> UpdateAsync (TypeId id, ModelCreateUpdateDto entityDto);

    Task<bool> DeleteAsync(TypeId id);

    Task<ModelDto> GetByIdAsync (TypeId id);

    Task<ModelDto> Search(string id);
}
