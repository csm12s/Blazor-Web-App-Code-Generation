using System;
using System.Collections.Generic;
using System.Collections;
using Mapster;
using System.Linq.Expressions;

namespace Gardener.Common;

public static partial class Extension
{
    /// <summary>
    /// MapTo
    /// Class support
    /// Case insensitive
    /// Type insensitive
    /// </summary>
    /// <typeparam name="TDestination"></typeparam>
    /// <param name="source"></param>
    /// <returns></returns>
    public static TDestination? MapTo<TDestination>(this object source,
        TypeAdapterConfig? config = null)
    {
        if (source == null)
        {
            return default(TDestination);
        }

        // Mapster
        if (config == null)
        {
            return source.Adapt<TDestination>();
        }
        else
        {
            return source.Adapt<TDestination>(config);
        }

        // Json
        //var json = Newtonsoft.Json.JsonConvert.SerializeObject(source);
        //return Newtonsoft.Json.JsonConvert.DeserializeObject<TDestination>(json);

        // AutoMapper
        //Mapper mapper = new Mapper(new MapperConfiguration(cfg =>
        //    cfg.CreateMap(source.GetType(), typeof(TDestination))
        //    ));
        //return mapper.Map<TDestination>(source);
    }


    /// <summary>
    /// MapTo List
    /// Class support
    /// Case insensitive
    /// Type insensitive
    /// </summary>
    /// <typeparam name="TDestination"></typeparam>
    /// <param name="sourceList"></param>
    /// <returns></returns>
    public static List<TDestination>? MapTo<TDestination>(this IEnumerable sourceList,
    TypeAdapterConfig? config = null)
    {
        if (sourceList == null)
        {
            return default(List<TDestination>);
        }

        if (config == null)
        {
            return sourceList.Adapt<List<TDestination>>();
        }
        else
        {
            return sourceList.Adapt<List<TDestination>>(config);
        }

        //var json = Newtonsoft.Json.JsonConvert.SerializeObject(sourceList);
        //return Newtonsoft.Json.JsonConvert.DeserializeObject<List<TDestination>>(json);

        //var sourceType = sourceList.GetType().GetGenericArguments()[0];
        //Mapper mapper = new Mapper(new MapperConfiguration(cfg =>
        //	cfg.CreateMap(sourceType, typeof(TDestination))
        //	));
        //return mapper.Map<List<TDestination>>(sourceList);
    }

    /// <summary>
    /// List to SelectItems
    /// Input demo:
    /// it=> it.key, it=> it.label
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="sourceList"></param>
    /// <param name="keyValueField"></param>
    /// <param name="labelNameField"></param>
    /// <returns></returns>
    public static List<SelectItem>? ToSelectItems<TSource>(
        this IEnumerable<TSource> sourceList,
        Expression<Func<TSource, string>> keyValueField,
        Expression<Func<TSource, string>> labelNameField)
    {
        if (sourceList == null)
        {
            return default(List<SelectItem>);
        }

        // Mapper config
        var config = new TypeAdapterConfig();
        config.ForType<TSource, SelectItem>()
            .Map(dest => dest.ValueName, keyValueField)
            .Map(dest => dest.LabelName, labelNameField);

        return sourceList.MapTo<SelectItem>(config);
    }


}