
using ComercioElectronico.Application.Model;

namespace ComercioElectronico.HttpApi.Model;
public class AppSetting
{
    public AppSetting()
    {
    }

    public double Iva {get;set;}

    public ICollection<UserInput> UserInputs {get; set;}

}