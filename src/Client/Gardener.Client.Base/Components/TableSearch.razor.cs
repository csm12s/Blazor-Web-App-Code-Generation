// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using AntDesign;
using Gardener.Attributes;
using Gardener.Base;
using Gardener.Client.Base.Constants;
using Gardener.Common;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
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
        Dictionary<string, bool> _showFields=new Dictionary<string, bool>();
        [Inject]
        protected IClientLocalizer localizer { get; set; }

        /// <summary>
        /// 日期选择框是否选择时分秒
        /// </summary>
        [Parameter]
        public bool ShowTime { get; set; }
        /// <summary>
        /// 日期开始时间
        /// </summary>
        [Parameter]
        public DateTime BeginTime { get; set; } = DateTime.Now.Date.AddDays(1).AddMonths(-1);
         /// <summary>
        /// 日期结束时间
        /// </summary>
        [Parameter]
        public DateTime EndTime { get; set; } = DateTime.Now.Date.AddDays(1);
        /// <summary>
        /// 搜索
        /// </summary>
        [Parameter]
        [Required]
        public EventCallback<List<FilterGroup>> OnSearch { get; set; }
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
                
                Type fieldType = property.PropertyType.GetNonNullableType();
                 if (!fieldType.IsPrimitive && !fieldType.IsEnum  && !fieldType.Equals(typeof(string)) && !fieldType.Equals(typeof(Guid)) && !fieldType.Equals(typeof(DateTime))&& !fieldType.Equals(typeof(DateTimeOffset)))
                { 
                    continue ;
                }
                if (fieldType.IsArray || fieldType.IsEnumerable() || property.HasAttribute<DisabledSearchFieldAttribute>())
                {
                    continue;
                }
                string name=property.Name;
                string displayName = property.GetDescription();
                TableSearchField searchField = new TableSearchField
                {
                    Name = name,
                    DisplayName = localizer[displayName],
                    Type = fieldType,
                    IsSearchable = false
                };
                
                _fields.Add(searchField);


                ResetSearchFieldValue();

                _showFields.Add(name, false);
            }
        }
        /// <summary>
        /// 重置搜索字段值
        /// </summary>
        /// <param name="fields"></param>
        private void ResetSearchFieldValue()
        {
            if (_fields != null)
            {
                foreach (TableSearchField field in _fields)
                {
                    
                    if (_showFields.GetValueOrDefault(field.Name, true))
                    {
                        continue;
                    }
                    field.Value = "";
                    //日期类型默认值
                    if (IsDateTimeType(field.Type))
                    {
                        field.Values = new string[] { BeginTime.ToString(ClientConstant.InputDateTimeFormat), EndTime.ToString(ClientConstant.InputDateTimeFormat) };
                    }
                    else 
                    {
                        field.Values=new string[0];
                    }
                }
            
            }
        
        }
        /// <summary>
        /// 是否是日期类型
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private bool IsDateTimeType(Type type)
        {
            if (type.Equals(typeof(DateTime)) || type.Equals(typeof(DateTimeOffset)) || (type.IsNullableType() && (type.Equals(typeof(DateTime)) || type.Equals(typeof(DateTimeOffset)))))
            {
                return true;
            }
            return false;

        }

        private int lastFieldCount = 0;

        /// <summary>
        /// 筛选字段下拉选择
        /// </summary>
        /// <param name="values"></param>
        private async void OnSelectedItemsChangedHandler(IEnumerable<string> values)
        {
            bool increase = values.Count() > lastFieldCount;
            foreach (var item in _showFields)
            {
                if (values != null && values.Any(x => x.Equals(item.Key)))
                {
                    _showFields[item.Key] = true;
                }
                else 
                {
                    _showFields[item.Key] = false;
                }
            }
            lastFieldCount = values.Count();
            if (!increase) 
            {
                ResetSearchFieldValue();
                await OnSearchClick();
            }
        }
        /// <summary>
        /// 清理搜索值
        /// </summary>
        /// <returns></returns>
        private async void OnClearSearchValue()
        {
            ResetSearchFieldValue(); 
            await OnSearchClick();
        }
        /// <summary>
        /// 搜索
        /// </summary>
        private async Task OnSearchClick()
        {
            List<FilterGroup> filterGroups = new List<FilterGroup>();
            if (_selectedValues != null)
            {
                foreach (string value in _selectedValues)
                {
                    FilterGroup filterGroup = new FilterGroup();
                    var field = _fields.FirstOrDefault(f => f.Name.Equals(value));

                    if (IsDateTimeType(field.Type))
                    {
                        //日期
                        if (field.Values.Count() > 1 && !string.IsNullOrEmpty(field.Values.First()))
                        {
                            FilterRule ruleBegin = new FilterRule();
                            ruleBegin.Field = field.Name;
                            ruleBegin.Value = DateTime.Parse(field.Values.First());
                            ruleBegin.Operate = FilterOperate.GreaterOrEqual;
                            filterGroup.AddRule(ruleBegin);
                        }

                        if (field.Values.Count() > 2 && !string.IsNullOrEmpty(field.Values.Last()))
                        {
                            FilterRule ruleEnd = new FilterRule();
                            ruleEnd.Field = field.Name;
                            ruleEnd.Value = DateTime.Parse(field.Values.Last());
                            ruleEnd.Operate = FilterOperate.LessOrEqual;
                            filterGroup.AddRule(ruleEnd);
                        }

                    }
                    else if (field.Multiple)
                    {
                        //多值
                        if (!field.Values.Any())
                        {
                            continue;
                        }

                        if (field.Type.IsEnum || field.Type.Equals(typeof(bool)))
                        {
                            foreach (string valueTemp in field.Values)
                            {
                                FilterRule rule = new FilterRule();
                                rule.Field = field.Name;
                                rule.Value = valueTemp.CastTo(field.Type);
                                rule.Operate = FilterOperate.Equal;
                                rule.Condition = FilterCondition.Or;
                                filterGroup.AddRule(rule);
                            }
                        
                        }
                   
                    }
                    else
                    {
                        //单值
                        FilterRule rule = new FilterRule();
                        rule.Field = field.Name;
                        if (string.IsNullOrEmpty(field.Value))
                        {
                            continue;
                        }
                        rule.Value = field.Value.CastTo(field.Type);
                        if (field.Type.Equals(typeof(string)))
                        {
                            rule.Operate = FilterOperate.Contains;
                        }
                        else
                        {
                            rule.Operate = FilterOperate.Equal;
                        }
                        filterGroup.AddRule(rule);
                    }

                    filterGroups.Add(filterGroup);
                }
            }

            await OnSearch.InvokeAsync(filterGroups);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="eventArgs"></param>
        /// <param name="values"></param>
        private IEnumerable<string>  DateTimeFormat(DateRangeChangedEventArgs eventArgs)
        {
            if (eventArgs.Dates != null && eventArgs.Dates.Length > 0)
            {
                return eventArgs.Dates.Select(x => x.HasValue ? x.Value.ToString(ClientConstant.InputDateTimeFormat) : null).ToArray();
            }
            else 
            {
                return new string[0];
            }
        }
    }
}
