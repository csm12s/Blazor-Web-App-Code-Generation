using Furion.DatabaseAccessor;
using Gardener.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gardener.CodeGeneration.Domains;

[Table("Sys_Code_Gen")]
[Description("Code Generate")]
public class CodeGen: GardenerEntityBase<int>, IEntityTypeBuilder<CodeGen>
{
    [MaxLength(100)]
    public string TableName { get; set; }
    public string ClassName { get; set; }
    public string Module { get; set; }

    public string TableDescriptionEN { get; set; }
    public string TableDescriptionCH { get; set; }

    public string MenuName { get; set; }
    public string MenuParentId { get; set; }


    [MaxLength(5)]
    public string? TablePrefix { get; set; }

    [MaxLength(100)]
    public string? NameSpace { get; set; }

    public bool UseCustomTemplate { get; set; } = false;

    public ICollection<CodeGenConfig> CodeGenConfigs { get; set; }

    public void Configure(EntityTypeBuilder<CodeGen> entityBuilder, DbContext dbContext, Type dbContextLocator)
    {
    }

}
