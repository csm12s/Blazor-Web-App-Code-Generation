// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using AntDesign;
using Gardener.Attributes;
using Gardener.Base;
using Gardener.Client.Base.Model;
using Gardener.Common;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Gardener.Client.Base.Components
{
    /// <summary>
    /// 表格搜索
    /// </summary>
    /// <typeparam name="TDto"></typeparam>
    public partial class TableSearch<TDto>
    {
        List<TableSearchField> _fields;
        IEnumerable<string> _selectedValues=new List<string>() { nameof(BaseDto<int>.Id)};
        Dictionary<string, object> _bindValues=new Dictionary<string, object>();
        [Inject]
        protected IClientLocalizer localizer { get; set; }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <returns></returns>
        protected override async Task OnInitializedAsync()
        {
            Type type = typeof(TDto);
            //从dto找到需要查询的字段
            _fields =new List<TableSearchField>();
            PropertyInfo[] properties = type.GetProperties();
            foreach (PropertyInfo property in properties)
            {
                string name=property.Name;
                string displayName = property.GetDescription();
                Type fieldType = property.PropertyType;
                
                if (fieldType.IsArray || fieldType.IsEnumerable() || property.HasAttribute<DisabledSearchFieldAttribute>())
                {
                    continue;
                }
                _fields.Add(new TableSearchField
                {
                    Name = name,
                    DisplayName = localizer[displayName],
                    Type = property.PropertyType,
                });
            }
            
        }

        /// <summary>
        /// 筛选字段下拉选择
        /// </summary>
        /// <param name="values"></param>
        private void OnSelectedItemsChangedHandler(IEnumerable<string> values)
        {
            
        }
        /// <summary>
        /// 搜索
        /// </summary>
        private void OnSearch()
        {
            Console.WriteLine(System.Text.Json.JsonSerializer.Serialize(_bindValues));
        }
    }
}
