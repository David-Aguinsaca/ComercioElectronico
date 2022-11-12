using ComercioElectronico.Application.Model;
using FluentValidation;

namespace ComercioElectronico.Application.Validator;

public class TypeProductCreateUpdateDtoValidator : AbstractValidator<TypeProductCreateUpdateDto>
{
    public TypeProductCreateUpdateDtoValidator()
    {
        RuleFor(x => x.Name).Length(1,4).WithMessage("El Nombre debe tener min 1 y max 4 digitos");
        
    }
}
