// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.EntityFramwork.Enums;

namespace Gardener.EntityFramwork.Dto
{
    /// <summary>
    /// 筛选规则
    /// </summary>
    public class FilterRule
    {
         /// <summary>
         /// 初始化一个<see cref="FilterRule"/>的新实例
         /// </summary>
        public FilterRule()
        { }
        /// <summary>
        /// 使用指定数据名称，数据值及操作方式初始化一个<see cref="FilterRule"/>的新实例
        /// </summary>
        /// <param name="field">数据名称</param>
        /// <param name="value">数据值</param>
        /// <param name="operate">操作方式</param>
        public FilterRule(string field, object value, FilterOperate operate)
        {
            Field = field;
            Value = value;
            Operate = operate;
        }
        /// <summary>
        /// 获取或设置 属性名称
        /// </summary>
        public string Field { get; set; }

        /// <summary>
        /// 获取或设置 属性值
        /// </summary>
        public object Value { get; set; }

        /// <summary>
        /// 获取或设置 操作类型
        /// </summary>
        public FilterOperate Operate { get; set; }

        /// <summary>
        /// 获取或设置 条件间操作方式，仅限And, Or
        /// </summary>
        public FilterCondition Condition { get; set; } = FilterCondition.And;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public bool Equals(FilterRule obj)
        {
            return obj.Field==this.Field && obj.Value==this.Value && obj.Operate==this.Operate;
        }
    }
}
