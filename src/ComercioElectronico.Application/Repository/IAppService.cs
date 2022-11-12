namespace ComercioElectronico.Application.Repository;

public interface IAppService <ModelDto, ModelCreateUpdateDto>
{
    ICollection<ModelDto> GetAll();

    Task<bool> CreateAsync(ModelCreateUpdateDto entityDto);

    Task<bool> UpdateAsync (int id, ModelCreateUpdateDto entityDto);

    Task<bool> DeleteAsync(int id);

    Task<ModelDto> GetByIdAsync (int id);
}
