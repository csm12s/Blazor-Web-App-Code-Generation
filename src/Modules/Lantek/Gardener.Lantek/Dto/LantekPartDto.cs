
using System;
using System.ComponentModel.DataAnnotations;

namespace Gardener.Lantek.Dto;

public partial class LantekPartDto : LantekBaseDto
{
    #region Base
         /// <summary>
         /// PrdRef
         /// </summary>
        [Required]
         public string PrdRef { get; set; }
         /// <summary>
         /// PrdName
         /// </summary>
        [Required]
         public string PrdName { get; set; }
         /// <summary>
         /// PrdIntName
         /// </summary>
        [Required]
         public string PrdIntName { get; set; }
         /// <summary>
         /// PGroup
         /// </summary>
        [Required]
         public string PGroup { get; set; }
         /// <summary>
         /// BarCode
         /// </summary>
        [Required]
         public string BarCode { get; set; }
         /// <summary>
         /// IsActive
         /// </summary>
        [Required]
         public byte IsActive { get; set; }
         /// <summary>
         /// RealPrd
         /// </summary>
        [Required]
         public byte RealPrd { get; set; }
         /// <summary>
         /// Assembly
         /// </summary>
        [Required]
         public byte Assembly { get; set; }
         /// <summary>
         /// ForSale
         /// </summary>
        [Required]
         public byte ForSale { get; set; }
         /// <summary>
         /// PType
         /// </summary>
        [Required]
         public Int16 PType { get; set; }
         /// <summary>
         /// CstRanges
         /// </summary>
        [Required]
         public byte CstRanges { get; set; }
         /// <summary>
         /// FixPrice
         /// </summary>
        [Required]
         public byte FixPrice { get; set; }
         /// <summary>
         /// StdCost
         /// </summary>
        [Required]
         public double StdCost { get; set; }
         /// <summary>
         /// CurCost
         /// </summary>
        [Required]
         public double CurCost { get; set; }
         /// <summary>
         /// PrcMethod
         /// </summary>
        [Required]
         public Int16 PrcMethod { get; set; }
         /// <summary>
         /// CstMethod
         /// </summary>
        [Required]
         public Int16 CstMethod { get; set; }
         /// <summary>
         /// UCtName
         /// </summary>
        [Required]
         public string UCtName { get; set; }
         /// <summary>
         /// UntName
         /// </summary>
        [Required]
         public string UntName { get; set; }
         /// <summary>
         /// CurQuan
         /// </summary>
        [Required]
         public double CurQuan { get; set; }
         /// <summary>
         /// CDate
         /// </summary>
        [Required]
         public DateTime CDate { get; set; }
         /// <summary>
         /// KeyWords
         /// </summary>
        [Required]
         public string KeyWords { get; set; }
         /// <summary>
         /// Descrip
         /// </summary>
        [Required]
         public string Descrip { get; set; }
         /// <summary>
         /// Image
         /// </summary>
        [Required]
         public string Image { get; set; }
         public string Image_Data { get; set; }
         /// <summary>
         /// Weight
         /// </summary>
        [Required]
         public double Weight { get; set; }
         /// <summary>
         /// LeadTime
         /// </summary>
        [Required]
         public double LeadTime { get; set; }
         /// <summary>
         /// LeadUCtName
         /// </summary>
        [Required]
         public string LeadUCtName { get; set; }
         /// <summary>
         /// LeadUntName
         /// </summary>
        [Required]
         public string LeadUntName { get; set; }
         /// <summary>
         /// LeadUpdateMethod
         /// </summary>
        [Required]
         public Int16 LeadUpdateMethod { get; set; }
         /// <summary>
         /// CommCode
         /// </summary>
        [Required]
         public string CommCode { get; set; }
         /// <summary>
         /// OCountry
         /// </summary>
        [Required]
         public string OCountry { get; set; }
         /// <summary>
         /// CGroup
         /// </summary>
        [Required]
         public string CGroup { get; set; }
         /// <summary>
         /// GlsVarRef1
         /// </summary>
        [Required]
         public string GlsVarRef1 { get; set; }
         /// <summary>
         /// GlsUntName1
         /// </summary>
        [Required]
         public string GlsUntName1 { get; set; }
         /// <summary>
         /// GlsVarRef2
         /// </summary>
        [Required]
         public string GlsVarRef2 { get; set; }
         /// <summary>
         /// GlsUntName2
         /// </summary>
        [Required]
         public string GlsUntName2 { get; set; }
         /// <summary>
         /// GlsVarRef3
         /// </summary>
        [Required]
         public string GlsVarRef3 { get; set; }
         /// <summary>
         /// GlsUntName3
         /// </summary>
        [Required]
         public string GlsUntName3 { get; set; }
         /// <summary>
         /// GlsVarRef4
         /// </summary>
        [Required]
         public string GlsVarRef4 { get; set; }
         /// <summary>
         /// GlsUntName4
         /// </summary>
        [Required]
         public string GlsUntName4 { get; set; }
         /// <summary>
         /// GlsVarRef5
         /// </summary>
        [Required]
         public string GlsVarRef5 { get; set; }
         /// <summary>
         /// GlsUntName5
         /// </summary>
        [Required]
         public string GlsUntName5 { get; set; }
         /// <summary>
         /// GlsUCtName1
         /// </summary>
        [Required]
         public string GlsUCtName1 { get; set; }
         /// <summary>
         /// GlsUCtName2
         /// </summary>
        [Required]
         public string GlsUCtName2 { get; set; }
         /// <summary>
         /// GlsUCtName3
         /// </summary>
        [Required]
         public string GlsUCtName3 { get; set; }
         /// <summary>
         /// GlsUCtName4
         /// </summary>
        [Required]
         public string GlsUCtName4 { get; set; }
         /// <summary>
         /// GlsUCtName5
         /// </summary>
        [Required]
         public string GlsUCtName5 { get; set; }
         /// <summary>
         /// GlsAtRNameSerial
         /// </summary>
        [Required]
         public string GlsAtRNameSerial { get; set; }
         /// <summary>
         /// GlsAtRNameBatch
         /// </summary>
        [Required]
         public string GlsAtRNameBatch { get; set; }
         /// <summary>
         /// GlsTblRef
         /// </summary>
        [Required]
         public string GlsTblRef { get; set; }
         /// <summary>
         /// DisMatRef
         /// </summary>
        [Required]
         public string DisMatRef { get; set; }
         /// <summary>
         /// DisLength
         /// </summary>
        [Required]
         public double DisLength { get; set; }
         /// <summary>
         /// DisWidth
         /// </summary>
        [Required]
         public double DisWidth { get; set; }
         /// <summary>
         /// DisThickness
         /// </summary>
        [Required]
         public double DisThickness { get; set; }
         /// <summary>
         /// DisArea
         /// </summary>
        [Required]
         public double DisArea { get; set; }
         /// <summary>
         /// DisCreationM
         /// </summary>
        [Required]
         public string DisCreationM { get; set; }
         /// <summary>
         /// DisIsCanal
         /// </summary>
        [Required]
         public byte DisIsCanal { get; set; }
         /// <summary>
         /// DisShtRef
         /// </summary>
        [Required]
         public string DisShtRef { get; set; }
         /// <summary>
         /// DisRotations
         /// </summary>
        [Required]
         public string DisRotations { get; set; }
         /// <summary>
         /// DisFillerPart
         /// </summary>
        [Required]
         public Int16 DisFillerPart { get; set; }
         /// <summary>
         /// DisCanQuote
         /// </summary>
        [Required]
         public byte DisCanQuote { get; set; }
         /// <summary>
         /// DisSide
         /// </summary>
        [Required]
         public string DisSide { get; set; }
         /// <summary>
         /// DisCutPerim
         /// </summary>
        [Required]
         public double DisCutPerim { get; set; }
         /// <summary>
         /// DisMrkPerim
         /// </summary>
        [Required]
         public double DisMrkPerim { get; set; }
         /// <summary>
         /// DisExtArea
         /// </summary>
        [Required]
         public double DisExtArea { get; set; }
         /// <summary>
         /// DisRectArea
         /// </summary>
        [Required]
         public double DisRectArea { get; set; }
         /// <summary>
         /// DisTypeArea
         /// </summary>
        [Required]
         public Int16 DisTypeArea { get; set; }
         /// <summary>
         /// DisExtWeight
         /// </summary>
        [Required]
         public double DisExtWeight { get; set; }
         /// <summary>
         /// DisRectWeight
         /// </summary>
        [Required]
         public double DisRectWeight { get; set; }
         /// <summary>
         /// DisIsDuctPart
         /// </summary>
        [Required]
         public byte DisIsDuctPart { get; set; }
         /// <summary>
         /// DisUData1Prt
         /// </summary>
        [Required]
         public string DisUData1Prt { get; set; }
         /// <summary>
         /// DisUData2Prt
         /// </summary>
        [Required]
         public string DisUData2Prt { get; set; }
         /// <summary>
         /// DisUData3Prt
         /// </summary>
        [Required]
         public string DisUData3Prt { get; set; }
         /// <summary>
         /// DisUData4Prt
         /// </summary>
        [Required]
         public string DisUData4Prt { get; set; }
         /// <summary>
         /// DisUData5Prt
         /// </summary>
        [Required]
         public string DisUData5Prt { get; set; }
         /// <summary>
         /// DisUData6Prt
         /// </summary>
        [Required]
         public string DisUData6Prt { get; set; }
         /// <summary>
         /// DisUData7Prt
         /// </summary>
        [Required]
         public string DisUData7Prt { get; set; }
         /// <summary>
         /// DisUData8Prt
         /// </summary>
        [Required]
         public string DisUData8Prt { get; set; }
         /// <summary>
         /// DisIsDraft
         /// </summary>
        [Required]
         public byte DisIsDraft { get; set; }
         /// <summary>
         /// DisDuctDINCode
         /// </summary>
        [Required]
         public Int16 DisDuctDINCode { get; set; }
         /// <summary>
         /// DisDuctDINRef
         /// </summary>
        [Required]
         public string DisDuctDINRef { get; set; }
         /// <summary>
         /// DisSeamL
         /// </summary>
        [Required]
         public double DisSeamL { get; set; }
         /// <summary>
         /// DisCnnL
         /// </summary>
        [Required]
         public double DisCnnL { get; set; }
         /// <summary>
         /// DisFPosition
         /// </summary>
        [Required]
         public string DisFPosition { get; set; }
         /// <summary>
         /// DisCArea
         /// </summary>
        [Required]
         public double DisCArea { get; set; }
         /// <summary>
         /// DisUMAX
         /// </summary>
        [Required]
         public double DisUMAX { get; set; }
         /// <summary>
         /// DisLMAX
         /// </summary>
        [Required]
         public double DisLMAX { get; set; }
         /// <summary>
         /// DisSMAX
         /// </summary>
        [Required]
         public double DisSMAX { get; set; }
         /// <summary>
         /// DisPrssIndex
         /// </summary>
        [Required]
         public Int16 DisPrssIndex { get; set; }
         /// <summary>
         /// DisIsRemnant
         /// </summary>
        [Required]
         public byte DisIsRemnant { get; set; }
         /// <summary>
         /// DisPrcRmntPrice
         /// </summary>
        [Required]
         public float DisPrcRmntPrice { get; set; }
         /// <summary>
         /// DisPrcScrpPrice
         /// </summary>
        [Required]
         public float DisPrcScrpPrice { get; set; }
         /// <summary>
         /// DisRPriority
         /// </summary>
        [Required]
         public Int16 DisRPriority { get; set; }
         /// <summary>
         /// DisIsLocked
         /// </summary>
        [Required]
         public byte DisIsLocked { get; set; }
         /// <summary>
         /// DisCamQuan
         /// </summary>
        [Required]
         public int DisCamQuan { get; set; }
         /// <summary>
         /// DisShtRefOrg
         /// </summary>
        [Required]
         public string DisShtRefOrg { get; set; }
         /// <summary>
         /// DisUData1Sht
         /// </summary>
        [Required]
         public string DisUData1Sht { get; set; }
         /// <summary>
         /// DisUData2Sht
         /// </summary>
        [Required]
         public string DisUData2Sht { get; set; }
         /// <summary>
         /// DisUData3Sht
         /// </summary>
        [Required]
         public string DisUData3Sht { get; set; }
         /// <summary>
         /// DisPrice
         /// </summary>
        [Required]
         public double DisPrice { get; set; }
         /// <summary>
         /// DisInProgress
         /// </summary>
        [Required]
         public int DisInProgress { get; set; }
         /// <summary>
         /// DisFactor
         /// </summary>
        [Required]
         public double DisFactor { get; set; }
         /// <summary>
         /// DisVl
         /// </summary>
        [Required]
         public double DisVl { get; set; }
         /// <summary>
         /// DisVh
         /// </summary>
        [Required]
         public double DisVh { get; set; }
         /// <summary>
         /// DisVg
         /// </summary>
        [Required]
         public double DisVg { get; set; }
         /// <summary>
         /// DisVp
         /// </summary>
        [Required]
         public double DisVp { get; set; }
         /// <summary>
         /// DisBPrice
         /// </summary>
        [Required]
         public double DisBPrice { get; set; }
         /// <summary>
         /// DisCPrice
         /// </summary>
        [Required]
         public double DisCPrice { get; set; }
         /// <summary>
         /// DisAPrice
         /// </summary>
        [Required]
         public double DisAPrice { get; set; }
         /// <summary>
         /// DisFormatRef
         /// </summary>
        [Required]
         public string DisFormatRef { get; set; }
         /// <summary>
         /// DisProfileRef
         /// </summary>
        [Required]
         public string DisProfileRef { get; set; }
         /// <summary>
         /// DisWSA
         /// </summary>
        [Required]
         public double DisWSA { get; set; }
         /// <summary>
         /// DisWEA
         /// </summary>
        [Required]
         public double DisWEA { get; set; }
         /// <summary>
         /// DisFSA
         /// </summary>
        [Required]
         public double DisFSA { get; set; }
         /// <summary>
         /// DisFEA
         /// </summary>
        [Required]
         public double DisFEA { get; set; }
         /// <summary>
         /// DisPClass
         /// </summary>
        [Required]
         public Int16 DisPClass { get; set; }
         /// <summary>
         /// DisIsQuote
         /// </summary>
        [Required]
         public byte DisIsQuote { get; set; }
         /// <summary>
         /// DisVolume
         /// </summary>
        [Required]
         public double DisVolume { get; set; }
         /// <summary>
         /// DisCommonPartIni
         /// </summary>
        [Required]
         public byte DisCommonPartIni { get; set; }
         /// <summary>
         /// DisCommonPartEnd
         /// </summary>
        [Required]
         public byte DisCommonPartEnd { get; set; }
         /// <summary>
         /// DisCommonProfileIni
         /// </summary>
        [Required]
         public byte DisCommonProfileIni { get; set; }
         /// <summary>
         /// DisCommonProfileEnd
         /// </summary>
        [Required]
         public byte DisCommonProfileEnd { get; set; }
         /// <summary>
         /// DisJobRef
         /// </summary>
        [Required]
         public string DisJobRef { get; set; }
         /// <summary>
         /// DisChecked
         /// </summary>
        [Required]
         public Int16 DisChecked { get; set; }
         /// <summary>
         /// DisCanSplit
         /// </summary>
        [Required]
         public byte DisCanSplit { get; set; }
         /// <summary>
         /// DisPriceDate
         /// </summary>
        [Required]
         public DateTime DisPriceDate { get; set; }
         /// <summary>
         /// DisAreaByLength
         /// </summary>
        [Required]
         public double DisAreaByLength { get; set; }
         /// <summary>
         /// DisWeightByLength
         /// </summary>
        [Required]
         public double DisWeightByLength { get; set; }
         /// <summary>
         /// DisModelingBy
         /// </summary>
        [Required]
         public Int16 DisModelingBy { get; set; }
         /// <summary>
         /// DisModelingByID
         /// </summary>
        [Required]
         public string DisModelingByID { get; set; }
         /// <summary>
         /// DisStrength
         /// </summary>
        [Required]
         public double DisStrength { get; set; }
         /// <summary>
         /// DisSimpleBends
         /// </summary>
        [Required]
         public Int16 DisSimpleBends { get; set; }
         /// <summary>
         /// DisSpecialBends
         /// </summary>
        [Required]
         public Int16 DisSpecialBends { get; set; }
         /// <summary>
         /// DisBendingToolChanges
         /// </summary>
        [Required]
         public Int16 DisBendingToolChanges { get; set; }
         /// <summary>
         /// DisBendingRotations
         /// </summary>
        [Required]
         public Int16 DisBendingRotations { get; set; }
         /// <summary>
         /// DisRouteAbbreviation
         /// </summary>
        [Required]
         public string DisRouteAbbreviation { get; set; }
         /// <summary>
         /// DisExternalKey
         /// </summary>
        [Required]
         public string DisExternalKey { get; set; }
         /// <summary>
         /// RecState
         /// </summary>
        [Required]
         public Int16 RecState { get; set; }
         /// <summary>
         /// CrtDate
         /// </summary>
        [Required]
         public DateTime CrtDate { get; set; }
         /// <summary>
         /// LastDate
         /// </summary>
        [Required]
         public DateTime LastDate { get; set; }
         /// <summary>
         /// CrtUser
         /// </summary>
        [Required]
         public string CrtUser { get; set; }
         /// <summary>
         /// LastUser
         /// </summary>
        [Required]
         public string LastUser { get; set; }
         /// <summary>
         /// Owner
         /// </summary>
        [Required]
         public string Owner { get; set; }
         /// <summary>
         /// RecEnt
         /// </summary>
        [Required]
         public string RecEnt { get; set; }
         /// <summary>
         /// RecOU
         /// </summary>
        [Required]
         public string RecOU { get; set; }
         /// <summary>
         /// RecSec
         /// </summary>
        [Required]
         public int RecSec { get; set; }
         /// <summary>
         /// CntID
         /// </summary>
        [Required]
         public int CntID { get; set; }
         /// <summary>
         /// RecID
         /// </summary>
        [Required]
         public int RecID { get; set; }
	#endregion
}

