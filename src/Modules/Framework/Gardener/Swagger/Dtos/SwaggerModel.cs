// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using System.Collections.Generic;

namespace Gardener.Swagger.Dtos
{
    /// <summary>
    /// 数据模型
    /// </summary>
    public class SwaggerModel
    {
        /// <summary>
        /// 接口
        /// </summary>
        public string openapi { get; set; }
        /// <summary>
        /// 服务器
        /// </summary>
        public List<SwaggerServerInfo> servers { get; set; }
        /// <summary>
        /// 相关信息
        /// </summary>
        public SwaggerInfo info { get; set; }
        /// <summary>
        /// 路径
        /// </summary>
        public Dictionary <string, Dictionary<string, SwaggerApiInfo>> paths { get; set; }
        /// <summary>
        /// 标签
        /// </summary>
        public List<SwaggerTagInfo> tags { get; set; }


    }
    /// <summary>
    /// 服务器信息
    /// </summary>
    public class SwaggerServerInfo
    {
        /// <summary>
        /// URL
        /// </summary>
        public string url { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string description { get; set; }
    }
    /// <summary>
    /// 标签信息
    /// </summary>
    public class SwaggerTagInfo
    { 
        /// <summary>
        /// 名称
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string description { get; set; }
    }
    /// <summary>
    /// swagger 接口信息
    /// </summary>
    public class SwaggerInfo
    {
        /// <summary>
        /// 标题名称
        /// </summary>
        public string title { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string description { get; set; }
        /// <summary>
        /// 版本
        /// </summary>
        public string version { get; set; }
    }
    /// <summary>
    /// swagger API接口信息
    /// </summary>
    public class SwaggerApiInfo
    {
        /// <summary>
        /// 标签
        /// </summary>
        public List<string> tags { get; set; }
        /// <summary>
        /// 概述
        /// </summary>
        public string summary { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string description { get; set; }

    }

}
