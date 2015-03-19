using Mozu.Api;
using Mozu.Api.Resources.Commerce.Catalog.Admin.Attributedefinition;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MozuDataConnector.Domain.Handlers
{
    public class AttributeHandler
    {
        private Mozu.Api.IApiContext _apiContext;

        public async Task<IEnumerable<Mozu.Api.Contracts.ProductAdmin.Attribute>> GetAttributes(int tenantId, int? siteId,
            int? masterCatalogId, int? startIndex, int? pageSize, string sortBy = null, string filter = null)
        {
            _apiContext = new ApiContext(tenantId, siteId, masterCatalogId);

            var attributeResource = new AttributeResource(_apiContext);
            var attributes = await attributeResource.GetAttributesAsync(startIndex, pageSize, sortBy, filter, null);

            return attributes.Items;
        }

        public async Task<Mozu.Api.Contracts.ProductAdmin.Attribute> AddAttribute(int tenantId, int? siteId,
            int? masterCatalogId, Mozu.Api.Contracts.ProductAdmin.Attribute attribute)
        {
            _apiContext = new ApiContext(tenantId, siteId, masterCatalogId);

            var attributeResource = new AttributeResource(_apiContext);
            var newAttribute = await attributeResource.AddAttributeAsync(attribute, null);

            return newAttribute;
        }

        public async Task<Mozu.Api.Contracts.ProductAdmin.Attribute> UpdateAttribute(int tenantId, int? siteId,
            int? masterCatalogId, Mozu.Api.Contracts.ProductAdmin.Attribute attribute)
        {
            _apiContext = new ApiContext(tenantId, siteId, masterCatalogId);

            var attributeResource = new AttributeResource(_apiContext);
            var updatedAttribute = await attributeResource.UpdateAttributeAsync(attribute, attribute.AttributeFQN, null);

            return updatedAttribute;
        }
    }
}
