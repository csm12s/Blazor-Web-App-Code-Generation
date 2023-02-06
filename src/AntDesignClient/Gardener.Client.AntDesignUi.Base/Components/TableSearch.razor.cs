// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using AntDesign;
using Gardener.Attributes;
using Gardener.Base;
using Gardener.Client.AntDesignUi.Base.Constants;
using Gardener.Client.Base;
using Gardener.Common;
using Microsoft.AspNetCore.Components;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Gardener.Client.AntDesignUi.Base.Components
{
    /// <summary>
    /// 表格搜索
    /// </summary>
    /// <typeparam name="TDto"></typeparam>
    public partial class TableSearch<TDto>
    {
        /// <summary>
        /// 所有搜索字段的信息
        /// </summary>
        List<TableSearchField> _fields;
        /// <summary>
        /// 所有搜索字段的信息
        /// </summary>
        List<TableSearchField> _currentFields = new List<TableSearchField>();
        /// <summary>
        /// 选中的搜索字段
        /// </summary>
        IEnumerable<string> _selectedValues = new List<string>();
        /// <summary>
        /// 搜索字段是否展示
        /// </summary>
        Dictionary<string, bool> _showFields = new Dictionary<string, bool>();
        /// <summary>
        /// 默认本地化器
        /// </summary>
        [Inject]
        protected IClientLocalizer localizer { get; set; }

        /// <summary>
        /// 自定义本地化器
        /// </summary>
        [Parameter]
        public IClientLocalizer CustomLocalizer { get; set; }

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
        public EventCallback OnSearch { get; set; }

        /// <summary>
        /// 当搜索字段变化回调
        /// </summary>
        [Parameter]
        [Required]
        public EventCallback<List<FilterGroup>> OnSearchFieldChanged { get; set; }
        /// <summary>
        /// 默认搜索值
        /// key：字段名称，value：单值、多值（逗号隔开字符串）
        /// </summary>
        [Parameter]
        public Dictionary<string, object> DefaultValue { get; set; }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <returns></returns>
        protected override async Task OnInitializedAsync()
        {
            if (CustomLocalizer != null)
            {
                localizer = CustomLocalizer;
            }
            Type type = typeof(TDto);
            //从dto找到需要查询的字段
            _fields = new List<TableSearchField>();
            PropertyInfo[] properties = type.GetProperties();
            foreach (PropertyInfo property in properties)
            {

                Type fieldType = property.PropertyType.GetNonNullableType();
                if (!fieldType.IsPrimitive && !fieldType.IsEnum && !fieldType.Equals(typeof(string)) && !fieldType.Equals(typeof(Guid)) && !fieldType.Equals(typeof(DateTime)) && !fieldType.Equals(typeof(DateTimeOffset)))
                {
                    continue;
                }
                if (fieldType.IsArray || fieldType.IsEnumerable() || property.HasAttribute<DisabledSearchFieldAttribute>() || property.HasAttribute<CustomSearchFieldAttribute>())
                {
                    continue;
                }
                string name = property.Name;
                string displayName = property.GetDescription();
                TableSearchField searchField = new TableSearchField
                {
                    Name = name,
                    DisplayName = displayName,
                    Type = fieldType
                };

                Action<TableSearchField, object> action = (fieid, value) =>
                {
                    fieid.Value = value.ToString();
                };

                if (searchField.Type.GetNonNullableType().Equals(typeof(DateTimeOffset)) || searchField.Type.GetNonNullableType().Equals(typeof(DateTime)))
                {
                    searchField.Multiple = true;
                    action = (fieid, value) =>
                    {
                        fieid.Values = value.ToString().Split(",");
                    };
                }
                else if (searchField.Type.IsEnum)
                {
                    searchField.Multiple = true;
                    action = (fieid, value) =>
                    {
                        List<string> values = new List<string>();
                        foreach (string item in value.ToString().Split(","))
                        {
                            if (item.IsNumber())
                            {
                                values.Add(item);
                            }
                            else
                            {
                                object enumValue = Enum.Parse(searchField.Type, item);
                                object numValue = Convert.ChangeType(enumValue, searchField.Type.GetEnumUnderlyingType());
                                values.Add(numValue.ToString());
                            }

                        }
                        fieid.Values = values;
                    };
                }
                else if (searchField.Type.GetNonNullableType().Equals(typeof(bool)))
                {
                    searchField.Multiple = true;
                    action = (fieid, value) =>
                    {
                        fieid.Values = value.ToString().ToLower().Split(",");
                    };
                }
                bool fieldShow = false;
                if (DefaultValue != null && DefaultValue.ContainsKey(name))
                {
                    object value = DefaultValue.GetValueOrDefault(name);
                    if (value != null)
                    {
                        action(searchField, value);
                        fieldShow = true;
                    }
                }
                _fields.Add(searchField);
                _showFields.Add(name, fieldShow);
                if (fieldShow)
                {
                    ((List<string>)_selectedValues).Add(name);
                }
            }
            ResetSearchFieldValue();
            RefershCurrentFiled();
            await OnSearchFieldChanged.InvokeAsync(GetFilterGroups());
            await base.OnInitializedAsync();
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
                    //已展示的不重置
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
                        field.Values = new string[0];
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
            values = values == null ? new String[0] : values;
            bool reduce = values.Count() < lastFieldCount;
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
            //刷新存在的搜索条件
            RefershCurrentFiled();
            //如果减少就刷新一下列表
            if (reduce)
            {
                await OnSearchClick();
            }
        }
        /// <summary>
        /// 清理搜索值
        /// </summary>
        /// <returns></returns>
        private async void OnClearSearchValue()
        {
            RefershCurrentFiled();
            await OnSearchClick();
        }

        /// <summary>
        /// 获取搜索信息
        /// </summary>
        /// <returns></returns>
        private List<FilterGroup> GetFilterGroups()
        {
            List<FilterGroup> filterGroups = new List<FilterGroup>();
            if (_selectedValues != null)
            {
                foreach (string value in _selectedValues)
                {
                    FilterGroup filterGroup = new FilterGroup();
                    var field = _currentFields.FirstOrDefault(f => f.Name.Equals(value));
                    if (field == null)
                    {
                        continue; // null when clear _selectedValues
                    }

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
            return filterGroups;

        }

        /// <summary>
        /// 搜索
        /// </summary>
        private async Task OnSearchClick()
        {
            ResetSearchFieldValue();
            var filterGroups = GetFilterGroups();
            await OnSearchFieldChanged.InvokeAsync(filterGroups);
            await OnSearch.InvokeAsync();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="eventArgs"></param>
        /// <param name="values"></param>
        private IEnumerable<string> DateTimeFormat(DateRangeChangedEventArgs eventArgs)
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

        /// <summary>
        /// 刷新当前搜索字段
        /// </summary>
        private void RefershCurrentFiled()
        {
            _showFields.ForEach(x =>
            {
                if (x.Value && !_currentFields.Any(c => c.Name.Equals(x.Key)))
                {
                    _currentFields.Add(_fields.First(f => f.Name.Equals(x.Key)));
                }
                else if (!x.Value && _currentFields.Any(c => c.Name.Equals(x.Key)))
                {
                    _currentFields.Remove(_fields.First(f => f.Name.Equals(x.Key)));
                }
            });

        }
    }

}
