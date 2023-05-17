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
using Gardener.Enums;
using Gardener.SystemManager.Utils;
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
        List<TableSearchField> _fields = new List<TableSearchField>();
        /// <summary>
        /// 选中的搜索字段
        /// </summary>
        IEnumerable<string> _selectedValues = new List<string>();

        #region Parameters
        /// <summary>
        /// 默认本地化器
        /// </summary>
        [Inject]
        public IClientLocalizer localizer { get; set; } = null!;

        /// <summary>
        /// 自定义本地化器
        /// </summary>
        [Parameter]
        public IClientLocalizer CustomLocalizer { get; set; } = null!;

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
        /// 默认搜索值
        /// key：字段名称，value：单值、多值（逗号隔开字符串）
        /// </summary>
        [Parameter]
        public Dictionary<string, object> DefaultValue { get; set; } = null!;

        /// <summary>
        /// 是否总是显示搜索按钮
        /// </summary>
        [Parameter]
        public bool AlwaysShowSearchButton { get; set; } = false;

        /// <summary>
        /// 包含的字段
        /// </summary>
        /// <remarks>
        /// 优先级最高
        /// </remarks>
        [Parameter]
        public IEnumerable<string>? IncludeFields { get; set; }

        /// <summary>
        /// 排除的字段
        /// </summary>
        /// <remarks>
        /// 优先级最高
        /// </remarks>
        [Parameter]
        public IEnumerable<string>? ExcludeFields { get; set; }

        /// <summary>
        /// 字段排序
        /// </summary>
        /// <remarks>
        /// 优先级高于<see cref="CodeAttribute"/>
        /// </remarks>
        [Parameter]
        public Dictionary<string, int>? FieldOrders { get; set; }

        /// <summary>
        /// 排序方式
        /// </summary>
        /// <remarks>
        /// 默认升序
        /// </remarks>
        [Parameter]
        public ListSortType SortType { get; set; } = ListSortType.Asc;

        /// <summary>
        /// 字段对应的下拉项
        /// </summary>
        /// <remarks>
        /// <para>字典key:为字段名称</para>
        /// <para>字典value:为下拉选项</para>
        /// <para>为下拉选项key:为SelectItem的value</para>
        /// <para>为下拉选项value:为SelectItem的label</para>
        /// </remarks>
        [Parameter]
        public Dictionary<string,IEnumerable<KeyValuePair<string, string>>>? FieldSelectItems { get;set; }

        #endregion

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
            InitSearchFields();

            await base.OnInitializedAsync();
        }

        /// <summary>
        /// 初始化搜索字段
        /// </summary>
        private void InitSearchFields()
        {
            Type type = typeof(TDto);
            //从dto找到需要查询的字段
            PropertyInfo[] properties = type.GetProperties();
            foreach (PropertyInfo property in properties)
            {

                Type fieldType = property.PropertyType.GetNonNullableType();
                if (!fieldType.IsPrimitive && !fieldType.IsEnum && !fieldType.Equals(typeof(string)) && !fieldType.Equals(typeof(Guid)) && !fieldType.Equals(typeof(DateTime)) && !fieldType.Equals(typeof(DateTimeOffset)))
                {
                    continue;
                }
                if (fieldType.IsArray || fieldType.IsEnumerable() || property.HasAttribute<CustomSearchFieldAttribute>())
                {
                    continue;
                }
                string name = property.Name;
                //被排除
                if (this.ExcludeFields != null && this.ExcludeFields.Any(x => x.Equals(name)))
                {
                    continue;
                }
                //不在包含内
                if (this.IncludeFields != null && !this.IncludeFields.Any(x => x.Equals(name)))
                {
                    continue;
                }
                //特性禁用
                if (this.IncludeFields == null && property.HasAttribute<DisabledSearchFieldAttribute>())
                {
                    continue;
                }

                string? displayName = property.GetDescription();
                TableSearchField searchField = new TableSearchField
                {
                    Name = name,
                    DisplayName = displayName,
                    Type = fieldType
                };
                //排序值
                if (FieldOrders != null && FieldOrders.ContainsKey(searchField.Name))
                {
                    searchField.Order = FieldOrders[searchField.Name];
                }
                else
                {
                    var orderAttr = property.GetCustomAttribute<OrderAttribute>();
                    if (orderAttr != null)
                    {
                        searchField.Order = orderAttr.Order;
                    }
                }

                //填充默认值方式
                Action<TableSearchField, object> fullValue = (field, value) =>
                {
                    field.Value = value.ToString() ?? "";
                };
                searchField.Values = new string[0];
                searchField.Value = string.Empty;

                var codeType = property.GetCustomAttribute<CodeTypeAttribute>();
                if (codeType != null)
                {
                    //字典
                    searchField.IsCode = true;
                    searchField.CodeTypeValue = codeType.CodeTypeValue;
                    searchField.Codes = CodeUtil.GetCodesFromCache(codeType.CodeTypeValue)?.ToList();
                    searchField.Multiple = true;
                    fullValue = (field, value) =>
                    {
                        field.Values = (value.ToString() ?? "").Split(",");
                    };
                }
                else if (FieldSelectItems != null && FieldSelectItems.ContainsKey(name))
                {
                    //设置下拉项的字段
                    searchField.IsSetSelectItem = true;
                    searchField.Multiple = true;
                    fullValue = (field, value) =>
                    {
                        field.Values = (value.ToString() ?? "").Split(",");
                    };
                    searchField.SelectItems = FieldSelectItems[name];
                }
                else if (IsDateTimeType(searchField.Type))
                {
                    searchField.Multiple = true;
                    //默认值初始化
                    fullValue = (field, value) =>
                    {
                        field.Values = (value.ToString() ?? "").Split(",");
                    };
                    //初始化值
                    searchField.Values = new string[] { BeginTime.ToString(ClientConstant.InputDateTimeFormat), EndTime.ToString(ClientConstant.InputDateTimeFormat) };
                }
                else if (searchField.Type.IsEnum)
                {
                    searchField.Multiple = true;
                    fullValue = (field, value) =>
                    {
                        List<string> values = new List<string>();
                        foreach (string item in (value.ToString() ?? "").Split(","))
                        {
                            if (item.IsNumber())
                            {
                                values.Add(item);
                            }
                            else
                            {
                                object enumValue = Enum.Parse(searchField.Type, item);
                                object numValue = Convert.ChangeType(enumValue, searchField.Type.GetEnumUnderlyingType());
                                values.Add(numValue.ToString() ?? "");
                            }

                        }
                        field.Values = values;
                    };
                }
                else if (searchField.Type.GetNonNullableType().Equals(typeof(bool)))
                {
                    searchField.Multiple = true;
                    fullValue = (field, value) =>
                    {
                        field.Values = (value.ToString() ?? "").ToLower().Split(",");
                    };
                }

                if (DefaultValue != null && DefaultValue.ContainsKey(searchField.Name))
                {
                    object? value = DefaultValue.GetValueOrDefault(searchField.Name);
                    if (value != null)
                    {
                        fullValue(searchField, value);
                    }
                }

                _fields.Add(searchField);
                bool fieldShow = DefaultValue != null && DefaultValue.ContainsKey(name) && DefaultValue.GetValueOrDefault(name) != null;
                if (fieldShow)
                {
                    ((List<string>)_selectedValues).Add(name);
                }
            }
            if (ListSortType.Desc.Equals(SortType))
            {
                _fields = _fields.OrderByDescending(x => x.Order).ToList();
            }
            else if (ListSortType.Asc.Equals(SortType))
            {
                _fields = _fields.OrderBy(x => x.Order).ToList();
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
            _selectedValues = values == null ? new List<string>() : values;
            ResetSearchFieldValue();
            bool reduce = _selectedValues.Count() < lastFieldCount;
            lastFieldCount = _selectedValues.Count();
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
        private void OnClearSearchValue()
        {
            _selectedValues = new List<string>();
        }
        /// <summary>
        /// 重置搜索字段值
        /// </summary>
        /// <param name="fields"></param>
        private void ResetSearchFieldValue()
        {
            if (_fields != null)
            {
                foreach (TableSearchField searchField in _fields)
                {
                    //已展示的不重置
                    if (!_selectedValues.Any(x => x.Equals(searchField.Name)))
                    {
                        searchField.Value = string.Empty;
                        if (IsDateTimeType(searchField.Type))
                        {
                            //初始化值
                            searchField.Values = new string[] { BeginTime.ToString(ClientConstant.InputDateTimeFormat), EndTime.ToString(ClientConstant.InputDateTimeFormat) };
                        }
                        else
                        {
                            searchField.Values = new string[0];
                        }
                    }

                }

            }

        }
        /// <summary>
        /// 获取搜索条件
        /// </summary>
        /// <returns></returns>
        public List<FilterGroup> GetFilterGroups()
        {
            List<FilterGroup> filterGroups = new List<FilterGroup>();
            if (_selectedValues != null)
            {
                foreach (string value in _selectedValues)
                {
                    FilterGroup filterGroup = new FilterGroup();
                    var field = _fields.FirstOrDefault(f => f.Name.Equals(value));
                    if (field == null)
                    {
                        continue; // null when clear _selectedValues
                    }

                    if (IsDateTimeType(field.Type))
                    {
                        //日期
                        if (field.Values != null && field.Values.Count() > 1 && !string.IsNullOrEmpty(field.Values.First()))
                        {
                            FilterRule ruleBegin = new FilterRule();
                            ruleBegin.Field = field.Name;
                            ruleBegin.Value = DateTime.Parse(field.Values.First());
                            ruleBegin.Operate = FilterOperate.GreaterOrEqual;
                            filterGroup.AddRule(ruleBegin);
                        }

                        if (field.Values != null && field.Values.Count() > 2 && !string.IsNullOrEmpty(field.Values.Last()))
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
                        if (field.Values == null || !field.Values.Any())
                        {
                            continue;
                        }

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
            await OnSearch.InvokeAsync(GetFilterGroups());
        }

        /// <summary>
        /// 日期格式化
        /// </summary>
        /// <param name="eventArgs"></param>
        /// <param name="values"></param>
        private IEnumerable<string> DateTimeFormat(DateRangeChangedEventArgs eventArgs)
        {
            if (eventArgs.Dates != null && eventArgs.Dates.Length > 0)
            {
                List<string> times = new List<string>();
                foreach (var item in eventArgs.Dates)
                {
                    if (item == null) continue;
                    times.Add(item.Value.ToString(Client.Base.Constants.ClientConstant.InputDateTimeFormat));
                }
                return times;
            }
            else
            {
                return new string[0];
            }
        }
    }

}
