using System;
using Autofac;
using Mozu.Api;
using Mozu.Api.Resources.Platform;
using Mozu.Api.ToolKit;
using Mozu.Api.ToolKit.Config;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MozuDataConnector.Test
{
    [TestClass]
    public class MozuDataConnectorTests
    {
        private IApiContext _apiContext;
        private IContainer _container;


        [TestInitialize]
        public void Init()
        {
            _container = new Bootstrapper().Bootstrap().Container;
            var appSetting = _container.Resolve<IAppSetting>();
            var tenantId = int.Parse(appSetting.Settings["TenantId"].ToString());
            var siteId = int.Parse(appSetting.Settings["SiteId"].ToString());

            _apiContext = new ApiContext(tenantId, siteId);
        }
    
        [TestMethod]
        public void Should_Connect_To_Tenant()
        {
            var tenantResource = new TenantResource(_apiContext);
        }
        
        [TestMethod]
        public void Get_Attributes()
        {
            var attributeHandler = new MozuDataConnector.Domain.Handlers.AttributeHandler();
            
            var attributes = attributeHandler.GetAttributes(_apiContext.TenantId, _apiContext.SiteId, 
                _apiContext.MasterCatalogId, 0, 20, null, null).Result;
        }
    }
}
