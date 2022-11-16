
using System;

namespace Gardener.Lantek.Dto;

public partial class LantekPartDto : LantekBaseDto
{
    #region Base
         /// <summary>
         /// PrdRef
         /// </summary>
         public string PrdRef { get; set; }
         /// <summary>
         /// PrdName
         /// </summary>
         public string PrdName { get; set; }
         /// <summary>
         /// PrdIntName
         /// </summary>
         public string PrdIntName { get; set; }
         /// <summary>
         /// PGroup
         /// </summary>
         public string PGroup { get; set; }
         /// <summary>
         /// BarCode
         /// </summary>
         public string BarCode { get; set; }
         /// <summary>
         /// IsActive
         /// </summary>
         public bool IsActive { get; set; }
         /// <summary>
         /// RealPrd
         /// </summary>
         public bool RealPrd { get; set; }
         /// <summary>
         /// Assembly
         /// </summary>
         public bool Assembly { get; set; }
         /// <summary>
         /// ForSale
         /// </summary>
         public bool ForSale { get; set; }
         /// <summary>
         /// PType
         /// </summary>
         public int PType { get; set; }
         /// <summary>
         /// CstRanges
         /// </summary>
         public bool CstRanges { get; set; }
         /// <summary>
         /// FixPrice
         /// </summary>
         public bool FixPrice { get; set; }
         /// <summary>
         /// StdCost
         /// </summary>
         public double StdCost { get; set; }
         /// <summary>
         /// CurCost
         /// </summary>
         public double CurCost { get; set; }
         /// <summary>
         /// PrcMethod
         /// </summary>
         public int PrcMethod { get; set; }
         /// <summary>
         /// CstMethod
         /// </summary>
         public int CstMethod { get; set; }
         /// <summary>
         /// UCtName
         /// </summary>
         public string UCtName { get; set; }
         /// <summary>
         /// UntName
         /// </summary>
         public string UntName { get; set; }
         /// <summary>
         /// CurQuan
         /// </summary>
         public double CurQuan { get; set; }
         /// <summary>
         /// CDate
         /// </summary>
         public DateTime CDate { get; set; }
         /// <summary>
         /// KeyWords
         /// </summary>
         public string KeyWords { get; set; }
         /// <summary>
         /// Descrip
         /// </summary>
         public string Descrip { get; set; }
         /// <summary>
         /// Image
         /// </summary>
         public string Image { get; set; }
         /// <summary>
         /// Weight
         /// </summary>
         public double Weight { get; set; }
         /// <summary>
         /// LeadTime
         /// </summary>
         public double LeadTime { get; set; }
         /// <summary>
         /// LeadUCtName
         /// </summary>
         public string LeadUCtName { get; set; }
         /// <summary>
         /// LeadUntName
         /// </summary>
         public string LeadUntName { get; set; }
         /// <summary>
         /// LeadUpdateMethod
         /// </summary>
         public int LeadUpdateMethod { get; set; }
         /// <summary>
         /// CommCode
         /// </summary>
         public string CommCode { get; set; }
         /// <summary>
         /// OCountry
         /// </summary>
         public string OCountry { get; set; }
         /// <summary>
         /// CGroup
         /// </summary>
         public string CGroup { get; set; }
         /// <summary>
         /// GlsVarRef1
         /// </summary>
         public string GlsVarRef1 { get; set; }
         /// <summary>
         /// GlsUntName1
         /// </summary>
         public string GlsUntName1 { get; set; }
         /// <summary>
         /// GlsVarRef2
         /// </summary>
         public string GlsVarRef2 { get; set; }
         /// <summary>
         /// GlsUntName2
         /// </summary>
         public string GlsUntName2 { get; set; }
         /// <summary>
         /// GlsVarRef3
         /// </summary>
         public string GlsVarRef3 { get; set; }
         /// <summary>
         /// GlsUntName3
         /// </summary>
         public string GlsUntName3 { get; set; }
         /// <summary>
         /// GlsVarRef4
         /// </summary>
         public string GlsVarRef4 { get; set; }
         /// <summary>
         /// GlsUntName4
         /// </summary>
         public string GlsUntName4 { get; set; }
         /// <summary>
         /// GlsVarRef5
         /// </summary>
         public string GlsVarRef5 { get; set; }
         /// <summary>
         /// GlsUntName5
         /// </summary>
         public string GlsUntName5 { get; set; }
         /// <summary>
         /// GlsUCtName1
         /// </summary>
         public string GlsUCtName1 { get; set; }
         /// <summary>
         /// GlsUCtName2
         /// </summary>
         public string GlsUCtName2 { get; set; }
         /// <summary>
         /// GlsUCtName3
         /// </summary>
         public string GlsUCtName3 { get; set; }
         /// <summary>
         /// GlsUCtName4
         /// </summary>
         public string GlsUCtName4 { get; set; }
         /// <summary>
         /// GlsUCtName5
         /// </summary>
         public string GlsUCtName5 { get; set; }
         /// <summary>
         /// GlsAtRNameSerial
         /// </summary>
         public string GlsAtRNameSerial { get; set; }
         /// <summary>
         /// GlsAtRNameBatch
         /// </summary>
         public string GlsAtRNameBatch { get; set; }
         /// <summary>
         /// GlsTblRef
         /// </summary>
         public string GlsTblRef { get; set; }
         /// <summary>
         /// DisMatRef
         /// </summary>
         public string DisMatRef { get; set; }
         /// <summary>
         /// DisLength
         /// </summary>
         public double DisLength { get; set; }
         /// <summary>
         /// DisWidth
         /// </summary>
         public double DisWidth { get; set; }
         /// <summary>
         /// DisThickness
         /// </summary>
         public double DisThickness { get; set; }
         /// <summary>
         /// DisArea
         /// </summary>
         public double DisArea { get; set; }
         /// <summary>
         /// DisCreationM
         /// </summary>
         public string DisCreationM { get; set; }
         /// <summary>
         /// DisIsCanal
         /// </summary>
         public bool DisIsCanal { get; set; }
         /// <summary>
         /// DisShtRef
         /// </summary>
         public string DisShtRef { get; set; }
         /// <summary>
         /// DisRotations
         /// </summary>
         public string DisRotations { get; set; }
         /// <summary>
         /// DisFillerPart
         /// </summary>
         public int DisFillerPart { get; set; }
         /// <summary>
         /// DisCanQuote
         /// </summary>
         public bool DisCanQuote { get; set; }
         /// <summary>
         /// DisSide
         /// </summary>
         public string DisSide { get; set; }
         /// <summary>
         /// DisCutPerim
         /// </summary>
         public double DisCutPerim { get; set; }
         /// <summary>
         /// DisMrkPerim
         /// </summary>
         public double DisMrkPerim { get; set; }
         /// <summary>
         /// DisExtArea
         /// </summary>
         public double DisExtArea { get; set; }
         /// <summary>
         /// DisRectArea
         /// </summary>
         public double DisRectArea { get; set; }
         /// <summary>
         /// DisTypeArea
         /// </summary>
         public int DisTypeArea { get; set; }
         /// <summary>
         /// DisExtWeight
         /// </summary>
         public double DisExtWeight { get; set; }
         /// <summary>
         /// DisRectWeight
         /// </summary>
         public double DisRectWeight { get; set; }
         /// <summary>
         /// DisIsDuctPart
         /// </summary>
         public bool DisIsDuctPart { get; set; }
         /// <summary>
         /// DisUData1Prt
         /// </summary>
         public string DisUData1Prt { get; set; }
         /// <summary>
         /// DisUData2Prt
         /// </summary>
         public string DisUData2Prt { get; set; }
         /// <summary>
         /// DisUData3Prt
         /// </summary>
         public string DisUData3Prt { get; set; }
         /// <summary>
         /// DisUData4Prt
         /// </summary>
         public string DisUData4Prt { get; set; }
         /// <summary>
         /// DisUData5Prt
         /// </summary>
         public string DisUData5Prt { get; set; }
         /// <summary>
         /// DisUData6Prt
         /// </summary>
         public string DisUData6Prt { get; set; }
         /// <summary>
         /// DisUData7Prt
         /// </summary>
         public string DisUData7Prt { get; set; }
         /// <summary>
         /// DisUData8Prt
         /// </summary>
         public string DisUData8Prt { get; set; }
         /// <summary>
         /// DisIsDraft
         /// </summary>
         public bool DisIsDraft { get; set; }
         /// <summary>
         /// DisDuctDINCode
         /// </summary>
         public int DisDuctDINCode { get; set; }
         /// <summary>
         /// DisDuctDINRef
         /// </summary>
         public string DisDuctDINRef { get; set; }
         /// <summary>
         /// DisSeamL
         /// </summary>
         public double DisSeamL { get; set; }
         /// <summary>
         /// DisCnnL
         /// </summary>
         public double DisCnnL { get; set; }
         /// <summary>
         /// DisFPosition
         /// </summary>
         public string DisFPosition { get; set; }
         /// <summary>
         /// DisCArea
         /// </summary>
         public double DisCArea { get; set; }
         /// <summary>
         /// DisUMAX
         /// </summary>
         public double DisUMAX { get; set; }
         /// <summary>
         /// DisLMAX
         /// </summary>
         public double DisLMAX { get; set; }
         /// <summary>
         /// DisSMAX
         /// </summary>
         public double DisSMAX { get; set; }
         /// <summary>
         /// DisPrssIndex
         /// </summary>
         public int DisPrssIndex { get; set; }
         /// <summary>
         /// DisIsRemnant
         /// </summary>
         public bool DisIsRemnant { get; set; }
         /// <summary>
         /// DisPrcRmntPrice
         /// </summary>
         public float DisPrcRmntPrice { get; set; }
         /// <summary>
         /// DisPrcScrpPrice
         /// </summary>
         public float DisPrcScrpPrice { get; set; }
         /// <summary>
         /// DisRPriority
         /// </summary>
         public int DisRPriority { get; set; }
         /// <summary>
         /// DisIsLocked
         /// </summary>
         public bool DisIsLocked { get; set; }
         /// <summary>
         /// DisCamQuan
         /// </summary>
         public int DisCamQuan { get; set; }
         /// <summary>
         /// DisShtRefOrg
         /// </summary>
         public string DisShtRefOrg { get; set; }
         /// <summary>
         /// DisUData1Sht
         /// </summary>
         public string DisUData1Sht { get; set; }
         /// <summary>
         /// DisUData2Sht
         /// </summary>
         public string DisUData2Sht { get; set; }
         /// <summary>
         /// DisUData3Sht
         /// </summary>
         public string DisUData3Sht { get; set; }
         /// <summary>
         /// DisPrice
         /// </summary>
         public double DisPrice { get; set; }
         /// <summary>
         /// DisInProgress
         /// </summary>
         public int DisInProgress { get; set; }
         /// <summary>
         /// DisFactor
         /// </summary>
         public double DisFactor { get; set; }
         /// <summary>
         /// DisVl
         /// </summary>
         public double DisVl { get; set; }
         /// <summary>
         /// DisVh
         /// </summary>
         public double DisVh { get; set; }
         /// <summary>
         /// DisVg
         /// </summary>
         public double DisVg { get; set; }
         /// <summary>
         /// DisVp
         /// </summary>
         public double DisVp { get; set; }
         /// <summary>
         /// DisBPrice
         /// </summary>
         public double DisBPrice { get; set; }
         /// <summary>
         /// DisCPrice
         /// </summary>
         public double DisCPrice { get; set; }
         /// <summary>
         /// DisAPrice
         /// </summary>
         public double DisAPrice { get; set; }
         /// <summary>
         /// DisFormatRef
         /// </summary>
         public string DisFormatRef { get; set; }
         /// <summary>
         /// DisProfileRef
         /// </summary>
         public string DisProfileRef { get; set; }
         /// <summary>
         /// DisWSA
         /// </summary>
         public double DisWSA { get; set; }
         /// <summary>
         /// DisWEA
         /// </summary>
         public double DisWEA { get; set; }
         /// <summary>
         /// DisFSA
         /// </summary>
         public double DisFSA { get; set; }
         /// <summary>
         /// DisFEA
         /// </summary>
         public double DisFEA { get; set; }
         /// <summary>
         /// DisPClass
         /// </summary>
         public int DisPClass { get; set; }
         /// <summary>
         /// DisIsQuote
         /// </summary>
         public bool DisIsQuote { get; set; }
         /// <summary>
         /// DisVolume
         /// </summary>
         public double DisVolume { get; set; }
         /// <summary>
         /// DisCommonPartIni
         /// </summary>
         public bool DisCommonPartIni { get; set; }
         /// <summary>
         /// DisCommonPartEnd
         /// </summary>
         public bool DisCommonPartEnd { get; set; }
         /// <summary>
         /// DisCommonProfileIni
         /// </summary>
         public bool DisCommonProfileIni { get; set; }
         /// <summary>
         /// DisCommonProfileEnd
         /// </summary>
         public bool DisCommonProfileEnd { get; set; }
         /// <summary>
         /// DisJobRef
         /// </summary>
         public string DisJobRef { get; set; }
         /// <summary>
         /// DisChecked
         /// </summary>
         public int DisChecked { get; set; }
         /// <summary>
         /// DisCanSplit
         /// </summary>
         public bool DisCanSplit { get; set; }
         /// <summary>
         /// DisPriceDate
         /// </summary>
         public DateTime DisPriceDate { get; set; }
         /// <summary>
         /// DisAreaByLength
         /// </summary>
         public double DisAreaByLength { get; set; }
         /// <summary>
         /// DisWeightByLength
         /// </summary>
         public double DisWeightByLength { get; set; }
         /// <summary>
         /// DisModelingBy
         /// </summary>
         public int DisModelingBy { get; set; }
         /// <summary>
         /// DisModelingByID
         /// </summary>
         public string DisModelingByID { get; set; }
         /// <summary>
         /// DisStrength
         /// </summary>
         public double DisStrength { get; set; }
         /// <summary>
         /// DisSimpleBends
         /// </summary>
         public int DisSimpleBends { get; set; }
         /// <summary>
         /// DisSpecialBends
         /// </summary>
         public int DisSpecialBends { get; set; }
         /// <summary>
         /// DisBendingToolChanges
         /// </summary>
         public int DisBendingToolChanges { get; set; }
         /// <summary>
         /// DisBendingRotations
         /// </summary>
         public int DisBendingRotations { get; set; }
         /// <summary>
         /// DisRouteAbbreviation
         /// </summary>
         public string DisRouteAbbreviation { get; set; }
         /// <summary>
         /// DisExternalKey
         /// </summary>
         public string DisExternalKey { get; set; }
         /// <summary>
         /// RecState
         /// </summary>
         public int RecState { get; set; }
         /// <summary>
         /// CrtDate
         /// </summary>
         public DateTime CrtDate { get; set; }
         /// <summary>
         /// LastDate
         /// </summary>
         public DateTime LastDate { get; set; }
         /// <summary>
         /// CrtUser
         /// </summary>
         public string CrtUser { get; set; }
         /// <summary>
         /// LastUser
         /// </summary>
         public string LastUser { get; set; }
         /// <summary>
         /// Owner
         /// </summary>
         public string Owner { get; set; }
         /// <summary>
         /// RecEnt
         /// </summary>
         public string RecEnt { get; set; }
         /// <summary>
         /// RecOU
         /// </summary>
         public string RecOU { get; set; }
         /// <summary>
         /// RecSec
         /// </summary>
         public int RecSec { get; set; }
         /// <summary>
         /// CntID
         /// </summary>
         public int CntID { get; set; }
         /// <summary>
         /// RecID
         /// </summary>
         public int RecID { get; set; }
	#endregion
}

