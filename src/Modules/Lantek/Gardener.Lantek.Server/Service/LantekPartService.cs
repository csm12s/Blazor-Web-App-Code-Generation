
using Furion.DatabaseAccessor;
using Furion.DependencyInjection;
using Gardener.Base;
using Gardener.Lantek.Server.Model;

namespace Gardener.Lantek.Server.Service;

public class LantekPartService : BaseService<LantekPart>, ITransient
{
    public LantekPartService(IRepository<LantekPart,
        MasterDbContextLocator> repository, SqlSugarRepository<LantekPart> sugarRepository) 
        : base(repository, sugarRepository)
    {
    }
}
