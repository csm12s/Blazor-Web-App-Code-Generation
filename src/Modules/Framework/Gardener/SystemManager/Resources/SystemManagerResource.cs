﻿// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Base.Resources;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        /// 排序
        /// </summary>
        public const string Order = nameof(Order);
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
        /// 字典管理
        /// </summary>
        public const string CodeManager = nameof(CodeManager);


    }
}
