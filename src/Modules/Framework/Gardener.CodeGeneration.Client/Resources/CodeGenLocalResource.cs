// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Base;
using Gardener.Base.Resources;

namespace Gardener.CodeGeneration.Client.Resources
{
    public class CodeGenLocalResource : SharedLocalResource
    {
        /// <summary>
        /// 搜索
        /// </summary>
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
        public const string OperateConfirmMessage = nameof(OperateConfirmMessage);
        public const string GenerateMenu = nameof(GenerateMenu);
    }
}
