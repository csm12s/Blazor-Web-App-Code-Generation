// -----------------------------------------------------------------------------
// 文件头
// -----------------------------------------------------------------------------

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System;

namespace Gardener.Client.Components
{
    public class ResourceAuthorizeData : IAuthorizeData
    {

        private readonly ResourceAuthorize _component;

        public string Policy
        {
            get
            {
                return _component.Policy;
            }
            set
            {
                throw new NotSupportedException();
            }
        }

        public string Roles
        {
            get
            {
                return _component.Roles;
            }
            set
            {
                throw new NotSupportedException();
            }
        }

        public string AuthenticationSchemes
        {
            get
            {
                return null;
            }
            set
            {
                throw new NotSupportedException();
            }
        }

        public ResourceAuthorizeData(ResourceAuthorize component)
        {
            _component = (component ?? throw new ArgumentNullException("component"));
        }
    }

    public partial  class ResourceAuthorize : AuthorizeViewCore
    {
        private readonly IAuthorizeData[] selfAsAuthorizeData;

        public string Policy = "permission";

        public string Roles = null;
       
        public ResourceAuthorize()
        {
            IAuthorizeData[] array = selfAsAuthorizeData = new ResourceAuthorizeData[1]
            {
                new ResourceAuthorizeData(this)
            };
        }
        protected override IAuthorizeData[] GetAuthorizeData()
        {
            return selfAsAuthorizeData;
        }
        [Parameter]
        public string ResourceKey { get; set; }

        protected override void OnParametersSet()
        {
            base.Resource = ResourceKey;
        }
    }
}
