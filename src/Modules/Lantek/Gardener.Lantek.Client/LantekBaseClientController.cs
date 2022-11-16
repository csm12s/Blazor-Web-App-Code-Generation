
using Gardener.Client.Base;

namespace Gardener.Lantek.Client;

public abstract class LantekBaseClientController<TDto>
    : BaseClientController<TDto, int> where TDto : class, new()
{
    protected LantekBaseClientController(IApiCaller apiCaller, string controller)
        : base(apiCaller, "Lantek" + "/" + controller)
    {
    }
}

