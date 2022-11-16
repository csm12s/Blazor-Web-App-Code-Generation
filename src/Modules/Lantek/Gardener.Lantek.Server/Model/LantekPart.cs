
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Furion.DatabaseAccessor;
using Microsoft.EntityFrameworkCore;

namespace Gardener.Lantek.Server.Model;

/// <summary>
/// BasePart
/// </summary>
[Table("PPRR_PPRR_00000100")]
[Comment("BasePart")]
public partial class LantekPart : LantekBaseModel
{
    public LantekPart() { }

    #region Base
        /// <summary>
        /// PrdRef
        /// </summary>
        [Column("PrdRef")]
        [Comment("PrdRef")][Required]
        public string PrdRef { get; set; }
        /// <summary>
        /// PrdName
        /// </summary>
        [Column("PrdName")]
        [Comment("PrdName")][Required]
        public string PrdName { get; set; }
        /// <summary>
        /// PrdIntName
        /// </summary>
        [Column("PrdIntName")]
        [Comment("PrdIntName")][Required]
        public string PrdIntName { get; set; }
        /// <summary>
        /// PGroup
        /// </summary>
        [Column("PGroup")]
        [Comment("PGroup")][Required]
        public string PGroup { get; set; }
        /// <summary>
        /// BarCode
        /// </summary>
        [Column("BarCode")]
        [Comment("BarCode")][Required]
        public string BarCode { get; set; }
        /// <summary>
        /// IsActive
        /// </summary>
        [Column("IsActive")]
        [Comment("IsActive")][Required]
        public bool IsActive { get; set; }
        /// <summary>
        /// RealPrd
        /// </summary>
        [Column("RealPrd")]
        [Comment("RealPrd")][Required]
        public bool RealPrd { get; set; }
        /// <summary>
        /// Assembly
        /// </summary>
        [Column("Assembly")]
        [Comment("Assembly")][Required]
        public bool Assembly { get; set; }
        /// <summary>
        /// ForSale
        /// </summary>
        [Column("ForSale")]
        [Comment("ForSale")][Required]
        public bool ForSale { get; set; }
        /// <summary>
        /// PType
        /// </summary>
        [Column("PType")]
        [Comment("PType")][Required]
        public int PType { get; set; }
        /// <summary>
        /// CstRanges
        /// </summary>
        [Column("CstRanges")]
        [Comment("CstRanges")][Required]
        public bool CstRanges { get; set; }
        /// <summary>
        /// FixPrice
        /// </summary>
        [Column("FixPrice")]
        [Comment("FixPrice")][Required]
        public bool FixPrice { get; set; }
        /// <summary>
        /// StdCost
        /// </summary>
        [Column("StdCost")]
        [Comment("StdCost")][Required]
        public double StdCost { get; set; }
        /// <summary>
        /// CurCost
        /// </summary>
        [Column("CurCost")]
        [Comment("CurCost")][Required]
        public double CurCost { get; set; }
        /// <summary>
        /// PrcMethod
        /// </summary>
        [Column("PrcMethod")]
        [Comment("PrcMethod")][Required]
        public int PrcMethod { get; set; }
        /// <summary>
        /// CstMethod
        /// </summary>
        [Column("CstMethod")]
        [Comment("CstMethod")][Required]
        public int CstMethod { get; set; }
        /// <summary>
        /// UCtName
        /// </summary>
        [Column("UCtName")]
        [Comment("UCtName")][Required]
        public string UCtName { get; set; }
        /// <summary>
        /// UntName
        /// </summary>
        [Column("UntName")]
        [Comment("UntName")][Required]
        public string UntName { get; set; }
        /// <summary>
        /// CurQuan
        /// </summary>
        [Column("CurQuan")]
        [Comment("CurQuan")][Required]
        public double CurQuan { get; set; }
        /// <summary>
        /// CDate
        /// </summary>
        [Column("CDate")]
        [Comment("CDate")][Required]
        public DateTime CDate { get; set; }
        /// <summary>
        /// KeyWords
        /// </summary>
        [Column("KeyWords")]
        [Comment("KeyWords")][Required]
        public string KeyWords { get; set; }
        /// <summary>
        /// Descrip
        /// </summary>
        [Column("Descrip")]
        [Comment("Descrip")][Required]
        public string Descrip { get; set; }
        /// <summary>
        /// Image
        /// </summary>
        [Column("Image")]
        [Comment("Image")][Required]
        public string Image { get; set; }
        /// <summary>
        /// Weight
        /// </summary>
        [Column("Weight")]
        [Comment("Weight")][Required]
        public double Weight { get; set; }
        /// <summary>
        /// LeadTime
        /// </summary>
        [Column("LeadTime")]
        [Comment("LeadTime")][Required]
        public double LeadTime { get; set; }
        /// <summary>
        /// LeadUCtName
        /// </summary>
        [Column("LeadUCtName")]
        [Comment("LeadUCtName")][Required]
        public string LeadUCtName { get; set; }
        /// <summary>
        /// LeadUntName
        /// </summary>
        [Column("LeadUntName")]
        [Comment("LeadUntName")][Required]
        public string LeadUntName { get; set; }
        /// <summary>
        /// LeadUpdateMethod
        /// </summary>
        [Column("LeadUpdateMethod")]
        [Comment("LeadUpdateMethod")][Required]
        public int LeadUpdateMethod { get; set; }
        /// <summary>
        /// CommCode
        /// </summary>
        [Column("CommCode")]
        [Comment("CommCode")][Required]
        public string CommCode { get; set; }
        /// <summary>
        /// OCountry
        /// </summary>
        [Column("OCountry")]
        [Comment("OCountry")][Required]
        public string OCountry { get; set; }
        /// <summary>
        /// CGroup
        /// </summary>
        [Column("CGroup")]
        [Comment("CGroup")][Required]
        public string CGroup { get; set; }
        /// <summary>
        /// GlsVarRef1
        /// </summary>
        [Column("GLS_VarRef1")]
        [Comment("GlsVarRef1")][Required]
        public string GlsVarRef1 { get; set; }
        /// <summary>
        /// GlsUntName1
        /// </summary>
        [Column("GLS_UntName1")]
        [Comment("GlsUntName1")][Required]
        public string GlsUntName1 { get; set; }
        /// <summary>
        /// GlsVarRef2
        /// </summary>
        [Column("GLS_VarRef2")]
        [Comment("GlsVarRef2")][Required]
        public string GlsVarRef2 { get; set; }
        /// <summary>
        /// GlsUntName2
        /// </summary>
        [Column("GLS_UntName2")]
        [Comment("GlsUntName2")][Required]
        public string GlsUntName2 { get; set; }
        /// <summary>
        /// GlsVarRef3
        /// </summary>
        [Column("GLS_VarRef3")]
        [Comment("GlsVarRef3")][Required]
        public string GlsVarRef3 { get; set; }
        /// <summary>
        /// GlsUntName3
        /// </summary>
        [Column("GLS_UntName3")]
        [Comment("GlsUntName3")][Required]
        public string GlsUntName3 { get; set; }
        /// <summary>
        /// GlsVarRef4
        /// </summary>
        [Column("GLS_VarRef4")]
        [Comment("GlsVarRef4")][Required]
        public string GlsVarRef4 { get; set; }
        /// <summary>
        /// GlsUntName4
        /// </summary>
        [Column("GLS_UntName4")]
        [Comment("GlsUntName4")][Required]
        public string GlsUntName4 { get; set; }
        /// <summary>
        /// GlsVarRef5
        /// </summary>
        [Column("GLS_VarRef5")]
        [Comment("GlsVarRef5")][Required]
        public string GlsVarRef5 { get; set; }
        /// <summary>
        /// GlsUntName5
        /// </summary>
        [Column("GLS_UntName5")]
        [Comment("GlsUntName5")][Required]
        public string GlsUntName5 { get; set; }
        /// <summary>
        /// GlsUCtName1
        /// </summary>
        [Column("GLS_UCtName1")]
        [Comment("GlsUCtName1")][Required]
        public string GlsUCtName1 { get; set; }
        /// <summary>
        /// GlsUCtName2
        /// </summary>
        [Column("GLS_UCtName2")]
        [Comment("GlsUCtName2")][Required]
        public string GlsUCtName2 { get; set; }
        /// <summary>
        /// GlsUCtName3
        /// </summary>
        [Column("GLS_UCtName3")]
        [Comment("GlsUCtName3")][Required]
        public string GlsUCtName3 { get; set; }
        /// <summary>
        /// GlsUCtName4
        /// </summary>
        [Column("GLS_UCtName4")]
        [Comment("GlsUCtName4")][Required]
        public string GlsUCtName4 { get; set; }
        /// <summary>
        /// GlsUCtName5
        /// </summary>
        [Column("GLS_UCtName5")]
        [Comment("GlsUCtName5")][Required]
        public string GlsUCtName5 { get; set; }
        /// <summary>
        /// GlsAtRNameSerial
        /// </summary>
        [Column("GLS_AtRNameSerial")]
        [Comment("GlsAtRNameSerial")][Required]
        public string GlsAtRNameSerial { get; set; }
        /// <summary>
        /// GlsAtRNameBatch
        /// </summary>
        [Column("GLS_AtRNameBatch")]
        [Comment("GlsAtRNameBatch")][Required]
        public string GlsAtRNameBatch { get; set; }
        /// <summary>
        /// GlsTblRef
        /// </summary>
        [Column("GLS_TblRef")]
        [Comment("GlsTblRef")][Required]
        public string GlsTblRef { get; set; }
        /// <summary>
        /// DisMatRef
        /// </summary>
        [Column("DIS_MatRef")]
        [Comment("DisMatRef")][Required]
        public string DisMatRef { get; set; }
        /// <summary>
        /// DisLength
        /// </summary>
        [Column("DIS_Length")]
        [Comment("DisLength")][Required]
        public double DisLength { get; set; }
        /// <summary>
        /// DisWidth
        /// </summary>
        [Column("DIS_Width")]
        [Comment("DisWidth")][Required]
        public double DisWidth { get; set; }
        /// <summary>
        /// DisThickness
        /// </summary>
        [Column("DIS_Thickness")]
        [Comment("DisThickness")][Required]
        public double DisThickness { get; set; }
        /// <summary>
        /// DisArea
        /// </summary>
        [Column("DIS_Area")]
        [Comment("DisArea")][Required]
        public double DisArea { get; set; }
        /// <summary>
        /// DisCreationM
        /// </summary>
        [Column("DIS_CreationM")]
        [Comment("DisCreationM")][Required]
        public string DisCreationM { get; set; }
        /// <summary>
        /// DisIsCanal
        /// </summary>
        [Column("DIS_IsCanal")]
        [Comment("DisIsCanal")][Required]
        public bool DisIsCanal { get; set; }
        /// <summary>
        /// DisShtRef
        /// </summary>
        [Column("DIS_ShtRef")]
        [Comment("DisShtRef")][Required]
        public string DisShtRef { get; set; }
        /// <summary>
        /// DisRotations
        /// </summary>
        [Column("DIS_Rotations")]
        [Comment("DisRotations")][Required]
        public string DisRotations { get; set; }
        /// <summary>
        /// DisFillerPart
        /// </summary>
        [Column("DIS_FillerPart")]
        [Comment("DisFillerPart")][Required]
        public int DisFillerPart { get; set; }
        /// <summary>
        /// DisCanQuote
        /// </summary>
        [Column("DIS_CanQuote")]
        [Comment("DisCanQuote")][Required]
        public bool DisCanQuote { get; set; }
        /// <summary>
        /// DisSide
        /// </summary>
        [Column("DIS_Side")]
        [Comment("DisSide")][Required]
        public string DisSide { get; set; }
        /// <summary>
        /// DisCutPerim
        /// </summary>
        [Column("DIS_CutPerim")]
        [Comment("DisCutPerim")][Required]
        public double DisCutPerim { get; set; }
        /// <summary>
        /// DisMrkPerim
        /// </summary>
        [Column("DIS_MrkPerim")]
        [Comment("DisMrkPerim")][Required]
        public double DisMrkPerim { get; set; }
        /// <summary>
        /// DisExtArea
        /// </summary>
        [Column("DIS_ExtArea")]
        [Comment("DisExtArea")][Required]
        public double DisExtArea { get; set; }
        /// <summary>
        /// DisRectArea
        /// </summary>
        [Column("DIS_RectArea")]
        [Comment("DisRectArea")][Required]
        public double DisRectArea { get; set; }
        /// <summary>
        /// DisTypeArea
        /// </summary>
        [Column("DIS_TypeArea")]
        [Comment("DisTypeArea")][Required]
        public int DisTypeArea { get; set; }
        /// <summary>
        /// DisExtWeight
        /// </summary>
        [Column("DIS_ExtWeight")]
        [Comment("DisExtWeight")][Required]
        public double DisExtWeight { get; set; }
        /// <summary>
        /// DisRectWeight
        /// </summary>
        [Column("DIS_RectWeight")]
        [Comment("DisRectWeight")][Required]
        public double DisRectWeight { get; set; }
        /// <summary>
        /// DisIsDuctPart
        /// </summary>
        [Column("DIS_IsDuctPart")]
        [Comment("DisIsDuctPart")][Required]
        public bool DisIsDuctPart { get; set; }
        /// <summary>
        /// DisUData1Prt
        /// </summary>
        [Column("DIS_UData1_Prt")]
        [Comment("DisUData1Prt")][Required]
        public string DisUData1Prt { get; set; }
        /// <summary>
        /// DisUData2Prt
        /// </summary>
        [Column("DIS_UData2_Prt")]
        [Comment("DisUData2Prt")][Required]
        public string DisUData2Prt { get; set; }
        /// <summary>
        /// DisUData3Prt
        /// </summary>
        [Column("DIS_UData3_Prt")]
        [Comment("DisUData3Prt")][Required]
        public string DisUData3Prt { get; set; }
        /// <summary>
        /// DisUData4Prt
        /// </summary>
        [Column("DIS_UData4_Prt")]
        [Comment("DisUData4Prt")][Required]
        public string DisUData4Prt { get; set; }
        /// <summary>
        /// DisUData5Prt
        /// </summary>
        [Column("DIS_UData5_Prt")]
        [Comment("DisUData5Prt")][Required]
        public string DisUData5Prt { get; set; }
        /// <summary>
        /// DisUData6Prt
        /// </summary>
        [Column("DIS_UData6_Prt")]
        [Comment("DisUData6Prt")][Required]
        public string DisUData6Prt { get; set; }
        /// <summary>
        /// DisUData7Prt
        /// </summary>
        [Column("DIS_UData7_Prt")]
        [Comment("DisUData7Prt")][Required]
        public string DisUData7Prt { get; set; }
        /// <summary>
        /// DisUData8Prt
        /// </summary>
        [Column("DIS_UData8_Prt")]
        [Comment("DisUData8Prt")][Required]
        public string DisUData8Prt { get; set; }
        /// <summary>
        /// DisIsDraft
        /// </summary>
        [Column("DIS_IsDraft")]
        [Comment("DisIsDraft")][Required]
        public bool DisIsDraft { get; set; }
        /// <summary>
        /// DisDuctDINCode
        /// </summary>
        [Column("DIS_DuctDINCode")]
        [Comment("DisDuctDINCode")][Required]
        public int DisDuctDINCode { get; set; }
        /// <summary>
        /// DisDuctDINRef
        /// </summary>
        [Column("DIS_DuctDINRef")]
        [Comment("DisDuctDINRef")][Required]
        public string DisDuctDINRef { get; set; }
        /// <summary>
        /// DisSeamL
        /// </summary>
        [Column("DIS_SeamL")]
        [Comment("DisSeamL")][Required]
        public double DisSeamL { get; set; }
        /// <summary>
        /// DisCnnL
        /// </summary>
        [Column("DIS_CnnL")]
        [Comment("DisCnnL")][Required]
        public double DisCnnL { get; set; }
        /// <summary>
        /// DisFPosition
        /// </summary>
        [Column("DIS_FPosition")]
        [Comment("DisFPosition")][Required]
        public string DisFPosition { get; set; }
        /// <summary>
        /// DisCArea
        /// </summary>
        [Column("DIS_CArea")]
        [Comment("DisCArea")][Required]
        public double DisCArea { get; set; }
        /// <summary>
        /// DisUMAX
        /// </summary>
        [Column("DIS_UMAX")]
        [Comment("DisUMAX")][Required]
        public double DisUMAX { get; set; }
        /// <summary>
        /// DisLMAX
        /// </summary>
        [Column("DIS_LMAX")]
        [Comment("DisLMAX")][Required]
        public double DisLMAX { get; set; }
        /// <summary>
        /// DisSMAX
        /// </summary>
        [Column("DIS_SMAX")]
        [Comment("DisSMAX")][Required]
        public double DisSMAX { get; set; }
        /// <summary>
        /// DisPrssIndex
        /// </summary>
        [Column("DIS_PrssIndex")]
        [Comment("DisPrssIndex")][Required]
        public int DisPrssIndex { get; set; }
        /// <summary>
        /// DisIsRemnant
        /// </summary>
        [Column("DIS_IsRemnant")]
        [Comment("DisIsRemnant")][Required]
        public bool DisIsRemnant { get; set; }
        /// <summary>
        /// DisPrcRmntPrice
        /// </summary>
        [Column("DIS_PrcRmntPrice")]
        [Comment("DisPrcRmntPrice")][Required]
        public float DisPrcRmntPrice { get; set; }
        /// <summary>
        /// DisPrcScrpPrice
        /// </summary>
        [Column("DIS_PrcScrpPrice")]
        [Comment("DisPrcScrpPrice")][Required]
        public float DisPrcScrpPrice { get; set; }
        /// <summary>
        /// DisRPriority
        /// </summary>
        [Column("DIS_RPriority")]
        [Comment("DisRPriority")][Required]
        public int DisRPriority { get; set; }
        /// <summary>
        /// DisIsLocked
        /// </summary>
        [Column("DIS_IsLocked")]
        [Comment("DisIsLocked")][Required]
        public bool DisIsLocked { get; set; }
        /// <summary>
        /// DisCamQuan
        /// </summary>
        [Column("DIS_CamQuan")]
        [Comment("DisCamQuan")][Required]
        public int DisCamQuan { get; set; }
        /// <summary>
        /// DisShtRefOrg
        /// </summary>
        [Column("DIS_ShtRefOrg")]
        [Comment("DisShtRefOrg")][Required]
        public string DisShtRefOrg { get; set; }
        /// <summary>
        /// DisUData1Sht
        /// </summary>
        [Column("DIS_UData1_Sht")]
        [Comment("DisUData1Sht")][Required]
        public string DisUData1Sht { get; set; }
        /// <summary>
        /// DisUData2Sht
        /// </summary>
        [Column("DIS_UData2_Sht")]
        [Comment("DisUData2Sht")][Required]
        public string DisUData2Sht { get; set; }
        /// <summary>
        /// DisUData3Sht
        /// </summary>
        [Column("DIS_UData3_Sht")]
        [Comment("DisUData3Sht")][Required]
        public string DisUData3Sht { get; set; }
        /// <summary>
        /// DisPrice
        /// </summary>
        [Column("DIS_Price")]
        [Comment("DisPrice")][Required]
        public double DisPrice { get; set; }
        /// <summary>
        /// DisInProgress
        /// </summary>
        [Column("DIS_InProgress")]
        [Comment("DisInProgress")][Required]
        public int DisInProgress { get; set; }
        /// <summary>
        /// DisFactor
        /// </summary>
        [Column("DIS_Factor")]
        [Comment("DisFactor")][Required]
        public double DisFactor { get; set; }
        /// <summary>
        /// DisVl
        /// </summary>
        [Column("DIS_Vl")]
        [Comment("DisVl")][Required]
        public double DisVl { get; set; }
        /// <summary>
        /// DisVh
        /// </summary>
        [Column("DIS_Vh")]
        [Comment("DisVh")][Required]
        public double DisVh { get; set; }
        /// <summary>
        /// DisVg
        /// </summary>
        [Column("DIS_Vg")]
        [Comment("DisVg")][Required]
        public double DisVg { get; set; }
        /// <summary>
        /// DisVp
        /// </summary>
        [Column("DIS_Vp")]
        [Comment("DisVp")][Required]
        public double DisVp { get; set; }
        /// <summary>
        /// DisBPrice
        /// </summary>
        [Column("DIS_BPrice")]
        [Comment("DisBPrice")][Required]
        public double DisBPrice { get; set; }
        /// <summary>
        /// DisCPrice
        /// </summary>
        [Column("DIS_CPrice")]
        [Comment("DisCPrice")][Required]
        public double DisCPrice { get; set; }
        /// <summary>
        /// DisAPrice
        /// </summary>
        [Column("DIS_APrice")]
        [Comment("DisAPrice")][Required]
        public double DisAPrice { get; set; }
        /// <summary>
        /// DisFormatRef
        /// </summary>
        [Column("DIS_FormatRef")]
        [Comment("DisFormatRef")][Required]
        public string DisFormatRef { get; set; }
        /// <summary>
        /// DisProfileRef
        /// </summary>
        [Column("DIS_ProfileRef")]
        [Comment("DisProfileRef")][Required]
        public string DisProfileRef { get; set; }
        /// <summary>
        /// DisWSA
        /// </summary>
        [Column("DIS_WSA")]
        [Comment("DisWSA")][Required]
        public double DisWSA { get; set; }
        /// <summary>
        /// DisWEA
        /// </summary>
        [Column("DIS_WEA")]
        [Comment("DisWEA")][Required]
        public double DisWEA { get; set; }
        /// <summary>
        /// DisFSA
        /// </summary>
        [Column("DIS_FSA")]
        [Comment("DisFSA")][Required]
        public double DisFSA { get; set; }
        /// <summary>
        /// DisFEA
        /// </summary>
        [Column("DIS_FEA")]
        [Comment("DisFEA")][Required]
        public double DisFEA { get; set; }
        /// <summary>
        /// DisPClass
        /// </summary>
        [Column("DIS_PClass")]
        [Comment("DisPClass")][Required]
        public int DisPClass { get; set; }
        /// <summary>
        /// DisIsQuote
        /// </summary>
        [Column("DIS_IsQuote")]
        [Comment("DisIsQuote")][Required]
        public bool DisIsQuote { get; set; }
        /// <summary>
        /// DisVolume
        /// </summary>
        [Column("DIS_Volume")]
        [Comment("DisVolume")][Required]
        public double DisVolume { get; set; }
        /// <summary>
        /// DisCommonPartIni
        /// </summary>
        [Column("DIS_CommonPartIni")]
        [Comment("DisCommonPartIni")][Required]
        public bool DisCommonPartIni { get; set; }
        /// <summary>
        /// DisCommonPartEnd
        /// </summary>
        [Column("DIS_CommonPartEnd")]
        [Comment("DisCommonPartEnd")][Required]
        public bool DisCommonPartEnd { get; set; }
        /// <summary>
        /// DisCommonProfileIni
        /// </summary>
        [Column("DIS_CommonProfileIni")]
        [Comment("DisCommonProfileIni")][Required]
        public bool DisCommonProfileIni { get; set; }
        /// <summary>
        /// DisCommonProfileEnd
        /// </summary>
        [Column("DIS_CommonProfileEnd")]
        [Comment("DisCommonProfileEnd")][Required]
        public bool DisCommonProfileEnd { get; set; }
        /// <summary>
        /// DisJobRef
        /// </summary>
        [Column("DIS_JobRef")]
        [Comment("DisJobRef")][Required]
        public string DisJobRef { get; set; }
        /// <summary>
        /// DisChecked
        /// </summary>
        [Column("DIS_Checked")]
        [Comment("DisChecked")][Required]
        public int DisChecked { get; set; }
        /// <summary>
        /// DisCanSplit
        /// </summary>
        [Column("DIS_CanSplit")]
        [Comment("DisCanSplit")][Required]
        public bool DisCanSplit { get; set; }
        /// <summary>
        /// DisPriceDate
        /// </summary>
        [Column("DIS_PriceDate")]
        [Comment("DisPriceDate")][Required]
        public DateTime DisPriceDate { get; set; }
        /// <summary>
        /// DisAreaByLength
        /// </summary>
        [Column("DIS_AreaByLength")]
        [Comment("DisAreaByLength")][Required]
        public double DisAreaByLength { get; set; }
        /// <summary>
        /// DisWeightByLength
        /// </summary>
        [Column("DIS_WeightByLength")]
        [Comment("DisWeightByLength")][Required]
        public double DisWeightByLength { get; set; }
        /// <summary>
        /// DisModelingBy
        /// </summary>
        [Column("DIS_ModelingBy")]
        [Comment("DisModelingBy")][Required]
        public int DisModelingBy { get; set; }
        /// <summary>
        /// DisModelingByID
        /// </summary>
        [Column("DIS_ModelingByID")]
        [Comment("DisModelingByID")][Required]
        public string DisModelingByID { get; set; }
        /// <summary>
        /// DisStrength
        /// </summary>
        [Column("DIS_Strength")]
        [Comment("DisStrength")][Required]
        public double DisStrength { get; set; }
        /// <summary>
        /// DisSimpleBends
        /// </summary>
        [Column("DIS_SimpleBends")]
        [Comment("DisSimpleBends")][Required]
        public int DisSimpleBends { get; set; }
        /// <summary>
        /// DisSpecialBends
        /// </summary>
        [Column("DIS_SpecialBends")]
        [Comment("DisSpecialBends")][Required]
        public int DisSpecialBends { get; set; }
        /// <summary>
        /// DisBendingToolChanges
        /// </summary>
        [Column("DIS_BendingToolChanges")]
        [Comment("DisBendingToolChanges")][Required]
        public int DisBendingToolChanges { get; set; }
        /// <summary>
        /// DisBendingRotations
        /// </summary>
        [Column("DIS_BendingRotations")]
        [Comment("DisBendingRotations")][Required]
        public int DisBendingRotations { get; set; }
        /// <summary>
        /// DisRouteAbbreviation
        /// </summary>
        [Column("DIS_RouteAbbreviation")]
        [Comment("DisRouteAbbreviation")][Required]
        public string DisRouteAbbreviation { get; set; }
        /// <summary>
        /// DisExternalKey
        /// </summary>
        [Column("DIS_ExternalKey")]
        [Comment("DisExternalKey")][Required]
        public string DisExternalKey { get; set; }
        /// <summary>
        /// RecState
        /// </summary>
        [Column("RecState")]
        [Comment("RecState")][Required]
        public int RecState { get; set; }
        /// <summary>
        /// CrtDate
        /// </summary>
        [Column("CrtDate")]
        [Comment("CrtDate")][Required]
        public DateTime CrtDate { get; set; }
        /// <summary>
        /// LastDate
        /// </summary>
        [Column("LastDate")]
        [Comment("LastDate")][Required]
        public DateTime LastDate { get; set; }
        /// <summary>
        /// CrtUser
        /// </summary>
        [Column("CrtUser")]
        [Comment("CrtUser")][Required]
        public string CrtUser { get; set; }
        /// <summary>
        /// LastUser
        /// </summary>
        [Column("LastUser")]
        [Comment("LastUser")][Required]
        public string LastUser { get; set; }
        /// <summary>
        /// Owner
        /// </summary>
        [Column("Owner")]
        [Comment("Owner")][Required]
        public string Owner { get; set; }
        /// <summary>
        /// RecEnt
        /// </summary>
        [Column("RecEnt")]
        [Comment("RecEnt")][Required]
        public string RecEnt { get; set; }
        /// <summary>
        /// RecOU
        /// </summary>
        [Column("RecOU")]
        [Comment("RecOU")][Required]
        public string RecOU { get; set; }
        /// <summary>
        /// RecSec
        /// </summary>
        [Column("RecSec")]
        [Comment("RecSec")][Required]
        public int RecSec { get; set; }
        /// <summary>
        /// CntID
        /// </summary>
        [Column("CntID")]
        [Comment("CntID")][Required]
        public int CntID { get; set; }
        /// <summary>
        /// RecID
        /// </summary>
        [Column("RecID")]
        [Comment("RecID")][Required]
        public int RecID { get; set; }
	#endregion
}
