
using Gardener.Base;

namespace Gardener.Lantek;

public interface ILantekBaseController<TDto>
    : IBaseController<TDto, int>
    where TDto : class, new()
{
}

