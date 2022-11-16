
using Gardener.Base;
using Gardener.Client.Base;
using Gardener.Lantek.Dto;
using Gardener.Lantek.IController;

namespace Gardener.Lantek.Client.Controller;

[ScopedService]
public class LantekPartClientController 
    : LantekBaseClientController<LantekPartDto>, 
    ILantekPartController
{
    public LantekPartClientController(IApiCaller apiCaller)
        :base(apiCaller, "LantekPart")
    {
    }

}
