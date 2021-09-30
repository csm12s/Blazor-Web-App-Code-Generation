// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using AntDesign;

namespace Gardener.Client.Base.Model
{
    /// <summary>
    /// 抽屉的设置
    /// </summary>
    public class DrawerSettings
    {
        public bool Closable { get; set; } = true;
        public int Width { get; set; }
        public Placement Placement { get; set; } = Placement.Right;
    }
}
