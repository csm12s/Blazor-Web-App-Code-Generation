
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
        public string Name { get; set; }

        public string Key { get; set; }
        public string ValueEN { get; set; }
        public string ValueCH { get; set; }

        /// <summary>
        /// Key in XxxResource.cs
        /// </summary>
        [ExcelIgnore]
        public string KeyInResource { get; set; }
    }
}
