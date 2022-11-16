
using Gardener.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Gardener.Lantek.Dto;

public partial class LantekPartSearchDto
{

    public string PrdRef { get; set; }
    public string PrdName { get; set; }
    
    [CustomSearchField]
    public string DisMatRef { get; set; }
}

