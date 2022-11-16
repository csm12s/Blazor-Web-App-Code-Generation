
using Gardener.Lantek.Dto;
using Gardener.Lantek.IController;
using Gardener.Lantek.Server.Model;
using Gardener.Lantek.Server.Service;

namespace Gardener.Lantek.Server.Controller;

/// <summary>
/// BasePart
/// </summary>
public class LantekPartController : LantekBaseController<LantekPart, LantekPartDto>
    , ILantekPartController
{
    #region Init
    private readonly LantekPartService lantekPartService;
    public LantekPartController(LantekPartService _baseService) : base(_baseService)
    {
    lantekPartService = _baseService;
    }
    #endregion

}
