using Mozu.Api;
using Mozu.Api.Resources.Commerce.Catalog.Admin.Attributedefinition;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MozuDataConnector.Domain.Handlers
{
    public class ProductTypeHandler
    {
        private Mozu.Api.IApiContext _apiContext;
    
        public async Task<IEnumerable<Mozu.Api.Contracts.ProductAdmin.ProductType>> GetProductTypes(int tenantId, int? siteId,
            int? masterCatalogId, int? startIndex, int? pageSize, string sortBy = null, string filter = null)
        {
            _apiContext = new ApiContext(tenantId, siteId, masterCatalogId);

            var productTypeResource = new ProductTypeResource(_apiContext);
            var productTypes = await productTypeResource.GetProductTypesAsync(startIndex, pageSize, sortBy, filter, null);

            return productTypes.Items;
        }

        public async Task<Mozu.Api.Contracts.ProductAdmin.ProductType> AddProductType(int tenantId, int? siteId,
            int? masterCatalogId, Mozu.Api.Contracts.ProductAdmin.ProductType productType)
        {
            _apiContext = new ApiContext(tenantId, siteId, masterCatalogId);

            var productTypeResource = new ProductTypeResource(_apiContext);
            var newProductType = await productTypeResource.AddProductTypeAsync(productType, null);

            return newProductType;
        }

        public async Task<Mozu.Api.Contracts.ProductAdmin.ProductType> UpdateProductType(int tenantId, int? siteId,
            int? masterCatalogId, Mozu.Api.Contracts.ProductAdmin.ProductType productType)
        {
            _apiContext = new ApiContext(tenantId, siteId, masterCatalogId);

            var productTypeResource = new ProductTypeResource(_apiContext);
            var updatedProductType = await productTypeResource.UpdateProductTypeAsync(productType, 
                Convert.ToInt32(productType.Id), null);

            return updatedProductType;
        }
    }
}
