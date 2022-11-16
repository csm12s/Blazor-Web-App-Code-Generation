
using Gardener.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Gardener.Lantek.Dto;

public partial class LantekPartSearchDto
{
             /// <summary>
             /// PrdRef
             /// </summary>
             [DisplayName("PrdRef")]
                 public string PrdRef { get; set; }
             /// <summary>
             /// PrdName
             /// </summary>
             [DisplayName("PrdName")]
                 public string PrdName { get; set; }
             /// <summary>
             /// DisMatRef
             /// </summary>
             [DisplayName("DisMatRef")]
                 [CustomSearchField]
                 [DisabledSearchField]
                     public string DisMatRef { get; set; }
}

