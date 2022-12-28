// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

namespace Gardener.Base.Resources
{
    /// <summary>
    /// 菜单名称资源
    /// </summary>
    /// <remarks>
    /// 菜单会通过Key: "menu:ResourceDto.Key" 在资源中查找，如果未找到，将使用"ResourceDto.Name"
    /// </remarks>
    public class MenuNameLocalResource : ILocalResource
    {
    }
}
