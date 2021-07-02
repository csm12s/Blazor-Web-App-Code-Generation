// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using AntDesign.TableModels;
using Gardener.Application.Dtos;
using Mapster;
using Microsoft.Extensions.DependencyInjection;

namespace Gardener.Client
{
    public static class MapsterExtension
    {

        public static IServiceCollection AddTypeAdapterConfigs(this IServiceCollection services)
        {
            TypeAdapterConfig<ITableSortModel, SearchSort>
                    .NewConfig()
                    .Map(s => s.FieldName, d => d.FieldName)
                    .Map(s => s.SortType, d => d.Sort== "descend" ? SearchSortType.Desc : SearchSortType.Asc);

            return services;
        }

    }
}
