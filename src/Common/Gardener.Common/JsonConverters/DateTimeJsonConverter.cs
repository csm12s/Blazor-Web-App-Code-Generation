﻿// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Gardener.Common.JsonConverters
{
    /// <summary>
    /// DateTime 类型序列化
    /// </summary>
    public class DateTimeJsonConverter : JsonConverter<DateTime>
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="format"></param>
        public DateTimeJsonConverter(string format="")
        {
            Format = format;
        }

        /// <summary>
        /// 时间格式化格式
        /// </summary>
        public string Format { get; private set; }

        /// <summary>
        /// 反序列化
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="typeToConvert"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var str= reader.GetString();
            if (str == null) return default(DateTime);
            return DateTime.Parse(str);
        }

        /// <summary>
        /// 序列化
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="options"></param>
        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            if (string.IsNullOrEmpty(Format))
            {
                writer.WriteStringValue(value.ToLocalTime());
            }
            else
            {
                writer.WriteStringValue(value.ToLocalTime().ToString(Format));
            }
        }
    }
}