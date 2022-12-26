﻿// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gardener.CodeGeneration.Dtos
{
    /// <summary>
    /// 表字段重命名配置
    /// </summary>
    public class CodeGenReplaceItem
    {
        public string OriginText { get; set; }//SYS_
        public string ReplacedText { get; set; }//Sys
    }
}