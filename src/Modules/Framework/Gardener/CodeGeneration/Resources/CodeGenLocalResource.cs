// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Base.Resources;

namespace Gardener.CodeGeneration.Resources
{
    /// <summary>
    /// 代码生成资源Key
    /// </summary>
    public class CodeGenLocalResource : SharedLocalResource
    {
        public const string TableName = nameof(TableName);
        public const string ClassName = nameof(ClassName);
        public const string Module = nameof(Module);
        public const string Remark = nameof(Remark);
        public const string TableDescription = nameof(TableDescription);
        public const string ParentMenu = nameof(ParentMenu);
        public const string IconName = nameof(IconName);
        public const string MenuName = nameof(MenuName);
        public const string PrimaryKeyName = nameof(PrimaryKeyName);
        public const string GenerateProjectFile = nameof(GenerateProjectFile);
        public const string GenerateBaseClass = nameof(GenerateBaseClass);
        public const string UseCustomTemplate = nameof(UseCustomTemplate);
        public const string UseChineseKey = nameof(UseChineseKey);
        public const string HasAdd = nameof(HasAdd);
        public const string HasEdit = nameof(HasEdit);
        public const string HasDelete = nameof(HasDelete);
        public const string HasBatchDelete = nameof(HasBatchDelete);
        public const string EntityFromTable = nameof(EntityFromTable);
        public const string ExplainEntityFromTable = nameof(ExplainEntityFromTable);
        public const string EntityFromTableSettings = nameof(EntityFromTableSettings);
        public const string OriginalModule = nameof(OriginalModule);
        public const string NewTableName = nameof(NewTableName);
        public const string AllowNull = nameof(AllowNull);
        public const string ExplainAllowNull = nameof(ExplainAllowNull);
        public const string OnlySave = nameof(OnlySave);
        public const string SaveAndResetSettings = nameof(SaveAndResetSettings);
        public const string BatchGenerate = nameof(BatchGenerate);
        public const string OpenCodeGenFolder = nameof(OpenCodeGenFolder);
        public const string GenerateMenu = nameof(GenerateMenu);
        public const string SaveAndClose = nameof(SaveAndClose);
        public const string ColumnName = nameof(ColumnName);
        public const string NetColumnName = nameof(NetColumnName);
        public const string NetType = nameof(NetType);
        public const string DbDataType = nameof(DbDataType);
        public const string IsView = nameof(IsView);
        public const string ViewComponentType = nameof(ViewComponentType);
        public const string IsEdit = nameof(IsEdit);
        public const string EditComponentType = nameof(EditComponentType);
        public const string IsSearch = nameof(IsSearch);
        public const string IsCustomSearch = nameof(IsCustomSearch);
        public const string CustomSearchType = nameof(CustomSearchType);
        public const string CustomSearchLength = nameof(CustomSearchLength);
        public const string IsRequired = nameof(IsRequired);
        public const string IsEntity = nameof(IsEntity);
        public const string ColumnDescription = nameof(ColumnDescription);
        public const string ColumnSummary = nameof(ColumnSummary);
    }
}
