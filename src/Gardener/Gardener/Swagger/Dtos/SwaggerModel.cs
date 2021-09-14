// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using System.Collections.Generic;

namespace Gardener.Swagger.Dtos
{
    /// <summary>
    /// 
    /// </summary>
    public class SwaggerModel
    {
        public string openapi { get; set; }

        public List<SwaggerServerInfo> servers { get; set; }

        public SwaggerInfo info { get; set; }

        public Dictionary <string, Dictionary<string, SwaggerApiInfo>> paths { get; set; }
        
        public List<SwaggerTagInfo> tags { get; set; }


    }
    public class SwaggerServerInfo
    {
        public string url { get; set; }
        public string description { get; set; }
    }
    public class SwaggerTagInfo
    { 
        public string name { get; set; }
        public string description { get; set; }
    }
    /// <summary>
    /// 
    /// </summary>
    public class SwaggerInfo
    {
        public string title { get; set; }

        public string description { get; set; }

        public string version { get; set; }
    }
    /// <summary>
    /// 
    /// </summary>
    public class SwaggerApiInfo
    {
        public List<string> tags { get; set; }

        public string summary { get; set; }
        public string description { get; set; }

    }

}
