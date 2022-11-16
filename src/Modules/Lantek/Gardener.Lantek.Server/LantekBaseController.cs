
using Gardener.Base;
using Furion.DynamicApiController;
using Microsoft.AspNetCore.Mvc;

namespace Gardener.Lantek.Server;

[ApiDescriptionSettings(Module = "Lantek", Groups = new string[] { "Lantek" }, ForceWithRoutePrefix = true, KeepName = true, KeepVerb = true, LowercaseRoute = false, SplitCamelCase = false)]
public class LantekBaseController<T, TDto>
    : BaseController<T, TDto, int>,
    IDynamicApiController
    where T : class, new()
    where TDto : class, new()
{
    public LantekBaseController(IBaseService<T> service) : base(service)
    {
    }
}

