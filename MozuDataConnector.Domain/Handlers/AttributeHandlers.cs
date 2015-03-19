﻿using Mozu.Api;
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
    }
}
