
using Gardener.Base;
using Gardener.Common;
using Gardener.Lantek.Dto;
using Gardener.Lantek.IController;
using Gardener.Lantek.Server.Model;
using Gardener.Lantek.Server.Service;
using Microsoft.AspNetCore.Mvc;

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

    #region Search
    [HttpPost]
    public override async Task<Base.PagedList<LantekPartDto>> Search(PageRequest request)
    {
        var list = await _baseService.GetListAsync(request);
        var listDto = list.MapTo<LantekPartDto>();

        foreach (var item in listDto)
        {
            item.Image_Data = ImageHelper.ImageToBase64(item.Image);
        }

        return listDto.ToPageList(request);
    }
    #endregion
}
