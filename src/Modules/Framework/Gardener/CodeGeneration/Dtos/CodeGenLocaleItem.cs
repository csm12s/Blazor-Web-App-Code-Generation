
using MiniExcelLibs.Attributes;

namespace Gardener.CodeGeneration.Dtos
{
    /// <summary>
    /// 数据库表多语言配置
    /// Excel中第一行是表信息
    /// 其他为列信息
    /// </summary>
    public class CodeGenLocaleItem
    {
        /// <summary>
        /// Table/Column name in DB
        /// </summary>
        public string Name { get; set; } = null!;
        /// <summary>
        /// Key
        /// </summary>
        public string? Key { get; set; }
        /// <summary>
        /// ValueEN
        /// </summary>
        public string? ValueEN { get; set; }
        /// <summary>
        /// ValueCH
        /// </summary>
        public string? ValueCH { get; set; }

        /// <summary>
        /// Key in XxxResource.cs
        /// </summary>
        [ExcelIgnore]
        public string? KeyInResource { get; set; }
    }
}
