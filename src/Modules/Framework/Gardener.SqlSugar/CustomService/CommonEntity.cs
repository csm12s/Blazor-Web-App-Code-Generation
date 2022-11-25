using Furion.DatabaseAccessor;


namespace Gardener;


//无主键表

//TODO: 下面的问题怎么解决，目前贴suppress sniffer
//需求：1 不自动创建表，贴[Manual]
//2 需要自动设置DbSet, 不能贴[Manual]，手动怎么设置DbSet


public abstract class CommonEntityNoKey : IEntity
{
}