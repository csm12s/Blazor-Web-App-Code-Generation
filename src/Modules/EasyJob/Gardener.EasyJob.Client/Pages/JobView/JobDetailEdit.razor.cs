// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using BlazorMonaco.Editor;
using Gardener.Client.AntDesignUi.Base.Components;
using Gardener.Client.Base;
using Gardener.Common;
using Gardener.EasyJob.Dtos;
using Gardener.EasyJob.Enums;
using Gardener.EasyJob.Resources;
using Microsoft.AspNetCore.Components.Forms;

namespace Gardener.EasyJob.Client.Pages.JobView
{
    /// <summary>
    /// 定时任务详情页
    /// </summary>
    public partial class JobDetailEdit : EditOperationDialogBase<SysJobDetailDto, int, EasyJobLocalResource>
    {
        private string tabActiveKey = "baseInfo";
        private const string HttpJobPropertiesKey = "HttpJob";

        private StandaloneCodeEditor _editor = null!;

        private HttpJobProperties? httpJobProperties;

        private string? _currentEditModelHttpMethod 
        {
            get 
            {
                return httpJobProperties?.HttpMethod.Method;
            }
            set
            { 
                if(value!=null && httpJobProperties != null)
                {
                    httpJobProperties.HttpMethod=new HttpMethod(value);
                }
            }
        }
        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            httpJobProperties = GetHttpJobProperties();
            if (this._editModel.CreateType.Equals(JobCreateType.Http))
            {
                RemoveHttpJobProperties();
            }
        }
        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        private async Task EditorOnDidInit()
        {
            await _editor.SetValue(_editModel.ScriptCode);
        }
        /// <summary>
        ///
        /// </summary>
        /// <param name="editor"></param>
        /// <returns></returns>
        private StandaloneEditorConstructionOptions EditorConstructionOptions(Editor editor)
        {
            return new StandaloneEditorConstructionOptions
            {
                AutomaticLayout = true,
                Language = "csharp"
            };
        }

        protected override async Task OnFormFinish(EditContext editContext)
        {
            if (JobCreateType.Script.Equals(_editModel.CreateType))
            {
                if (_editor == null)
                {
                    tabActiveKey = "script";
                    return;
                }
                string code = await _editor.GetValue();
                if (string.IsNullOrEmpty(code))
                {
                    tabActiveKey = "script";
                    return;
                }
                _editModel.ScriptCode = code;

            }
            else if (JobCreateType.Http.Equals(_editModel.CreateType))
            {
                AddHttpJobProperties();
            }
            await base.OnFormFinish(editContext);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private Dictionary<string, object?> GetPropertiesDic()
        {
            Dictionary<string, object?> properties = new Dictionary<string, object?>();
            if (!string.IsNullOrEmpty(_editModel.Properties))
            {
                var propertiesTemp = System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, object?>>(_editModel.Properties);
                if (propertiesTemp != null)
                {
                    properties = propertiesTemp;
                }
            }
            return properties;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private HttpJobProperties GetHttpJobProperties()
        {
            Dictionary<string, object?> properties = GetPropertiesDic();
            HttpJobProperties httpJobProperties = new HttpJobProperties("http://www.xxx.com");
            if (properties.ContainsKey(HttpJobPropertiesKey))
            {
                object? value = properties[HttpJobPropertiesKey];
                if (value != null)
                {
                    string? json = value.ToString();
                    if (!string.IsNullOrEmpty(json))
                    {
                        httpJobProperties = System.Text.Json.JsonSerializer.Deserialize<HttpJobProperties>(json) ?? httpJobProperties;
                    }
                }
            }
            return httpJobProperties;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="properties"></param>
        private void SetProperties(Dictionary<string, object?> properties)
        {
            _editModel.Properties = System.Text.Json.JsonSerializer.Serialize(properties, options: new System.Text.Json.JsonSerializerOptions()
            {
                WriteIndented = true,
            });
        }
        /// <summary>
        /// 
        /// </summary>
        private void RemoveHttpJobProperties()
        {
            Dictionary<string, object?> properties = GetPropertiesDic();
            if (properties.ContainsKey(HttpJobPropertiesKey))
            {
                properties.Remove(HttpJobPropertiesKey);
                SetProperties(properties);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        private void AddHttpJobProperties()
        {
            if (httpJobProperties == null) return;
            Dictionary<string, object?> properties = GetPropertiesDic();
            if (properties.ContainsKey(HttpJobPropertiesKey))
            {
                properties.Remove(HttpJobPropertiesKey);
            }
            properties.Add(HttpJobPropertiesKey, System.Text.Json.JsonSerializer.Serialize(httpJobProperties));
            SetProperties(properties);
        }

        /// <summary>
        /// 选中不同创建类型
        /// </summary>
        /// <param name="type"></param>
        private void OnChangeCreateType(JobCreateType type)
        {
        }
    }
}
