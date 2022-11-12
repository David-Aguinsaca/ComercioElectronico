using ComercioElectronico.Application.Model;
using FluentValidation;

namespace ComercioElectronico.Application.Validator;

public class BrandCreateUpdateDtoValidator : AbstractValidator<BrandCreateUpdateDto>
{
    public BrandCreateUpdateDtoValidator()
    {
        RuleFor(x => x.Name).Length(1,4).WithMessage("El Nombre debe tener min 1 y max 4 digitos");
        
    }
}
