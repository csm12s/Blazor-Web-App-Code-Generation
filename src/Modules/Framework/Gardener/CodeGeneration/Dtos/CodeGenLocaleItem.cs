
namespace Gardener.CodeGeneration.Dtos
{
    public class CodeGenLocaleItem
    {
        /// <summary>
        /// Table/Column name in DB
        /// </summary>
        public string Name { get; set; }

        public string Key { get; set; }
        public string ValueEN { get; set; }
        public string ValueCN { get; set; }
    }
}
