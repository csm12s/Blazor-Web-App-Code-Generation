// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Base.Resources;

namespace Gardener.SystemManager.Resources
{
    /// <summary>
    /// 系统管理资源
    /// </summary>
    public class SystemManagerResource : SharedLocalResource
    {
        /// <summary>
        /// 字典名称
        /// </summary>
        public const string CodeName = nameof(CodeName);
        /// <summary>
        /// 字典值
        /// </summary>
        public const string CodeValue = nameof(CodeValue);
        /// <summary>
        /// 扩展参数
        /// </summary>
        public const string ExtendParams = nameof(ExtendParams);
        /// <summary>
        /// 颜色
        /// </summary>
        public const string Color = nameof(Color);
        /// <summary>
        /// 字典类型
        /// </summary>
        public const string CodeType = nameof(CodeType);
        /// <summary>
        /// 字典类型编号
        /// </summary>
        public const string CodeTypeId = nameof(CodeTypeId);
        /// <summary>
        /// 字典类型名称
        /// </summary>
        public const string CodeTypeName = nameof(CodeTypeName);
        /// <summary>
        /// 字典类型值
        /// </summary>
        public const string CodeTypeValue = nameof(CodeTypeValue);
        /// <summary>
        /// 字典管理
        /// </summary>
        public const string CodeManager = nameof(CodeManager);
        /// <summary>
        /// 刷新字典工具缓存
        /// </summary>
        public const string RefreshCodeUtilCache = nameof(RefreshCodeUtilCache);


    }
}
