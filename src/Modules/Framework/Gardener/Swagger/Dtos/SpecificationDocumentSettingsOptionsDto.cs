// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

namespace Gardener.Swagger.Dtos
{
    /// <summary>
    /// swagger 文档分组信息
    /// </summary>
    public class SwaggerSpecificationOpenApiInfoDto
    {
        /// <summary>
        /// 排序
        /// </summary>
        public int Order { get; set; }
        /// <summary>
        /// 所属组
        /// </summary>
        public string? Group { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string? Title { get; set; }
    }
}