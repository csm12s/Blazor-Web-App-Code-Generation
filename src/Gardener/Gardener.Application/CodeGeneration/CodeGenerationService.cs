// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion.DatabaseAccessor;
using Furion.DynamicApiController;
using Gardener.Application.Dtos;
using Gardener.Application.Interfaces;
using Gardener.Common;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Gardener.Application
{
    /// <summary>
    /// 代码生成服务 CodeGenerationServices
    /// </summary>
    [ApiDescriptionSettings("CodeGenerationServices")]
    public class CodeGenerationService : ICodeGenerationService, IDynamicApiController
    {
        /// <summary>
        /// 获取所有实体定义
        /// </summary>
        /// <returns></returns>
        public async Task<List<EntityDefinitionDto>> GetEntityDefinitions()
        {
            List<EntityDefinitionDto> dtos = new List<EntityDefinitionDto>();

            var types = AppDomain.CurrentDomain.GetAssemblies()
                     .SelectMany(a => a.GetTypes().Where(t => 
                     (  t.GetInterfaces().Contains(typeof(IEntity))  || t.GetInterfaces().Contains(typeof(IPrivateEntity))) 
                     && !t.FullName.StartsWith("Furion.DatabaseAccessor") 
                     && !t.FullName.StartsWith("Gardener.Core.Entites.GardenerEntityBase")))
                     .ToList();
            foreach (Type type in types)
            {
                EntityDefinitionDto dto = new EntityDefinitionDto
                {
                    Name = type.Name,
                    FullName = type.FullName,
                    Description = type.GetDescription()
                };
                List<EntityPropertyDto> properties = new List<EntityPropertyDto>();
                foreach (PropertyInfo property in type.GetProperties())
                {
                    EntityPropertyDto propertyDto = new EntityPropertyDto
                    {
                        FieldName = property.Name,
                       
                        DisplayName = property.GetDescription()
                    };
                    Type pType = property.PropertyType;
                    if (pType.IsNullableType())
                    {
                        pType = Nullable.GetUnderlyingType(pType);
                        propertyDto.IsNullableType = true;
                    }

                    propertyDto.DataTypeName = pType.Name;
                    propertyDto.DataTypeFullName = pType.FullName;

                    properties.Add(propertyDto);
                }
                dto.Properties = properties;
                dtos.Add(dto);
            }
            return dtos;
        }
    }
}
