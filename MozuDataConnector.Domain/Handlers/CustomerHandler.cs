using Mozu.Api;
using Mozu.Api.Resources.Commerce.Catalog.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MozuDataConnector.Domain.Handlers
{
    public class ProductHandler
    { 
        private Mozu.Api.IApiContext _apiContext;

        public async Task<Mozu.Api.Contracts.ProductAdmin.Product> GetProduct(int tenantId, int? siteId,
            int? masterCatalogId, string productCode)
        {
            _apiContext = new ApiContext(tenantId, siteId, masterCatalogId);

            var productResource = new ProductResource(_apiContext);
            var product = await productResource.GetProductAsync(productCode, null);

            return product;
        }

        public async Task<IEnumerable<Mozu.Api.Contracts.ProductAdmin.Product>> GetProducts(int tenantId, int? siteId,
            int? masterCatalogId, int? startIndex, int? pageSize, string sortBy = null, string filter = null)
        {
            _apiContext = new ApiContext(tenantId, siteId, masterCatalogId);

            var productResource = new ProductResource(_apiContext);
            var products = await productResource.GetProductsAsync(startIndex, pageSize, sortBy, filter, null);

            return products.Items;
        }

        public async Task<Mozu.Api.Contracts.ProductAdmin.Product> AddProduct(int tenantId, int? siteId,
            int? masterCatalogId, Mozu.Api.Contracts.ProductAdmin.Product product)
        {
            _apiContext = new ApiContext(tenantId, siteId);

            var productResource = new ProductResource(_apiContext);
            var newProduct = await productResource.AddProductAsync(product);
          
            return newProduct;
        }

        public async Task<Mozu.Api.Contracts.ProductAdmin.Product> UpdateProduct(int tenantId, int? siteId,
            int? masterCatalogId, Mozu.Api.Contracts.ProductAdmin.Product product)
        {
            _apiContext = new ApiContext(tenantId, siteId, masterCatalogId);

            var productResource = new ProductResource(_apiContext);
            var updatedProduct = await productResource.UpdateProductAsync(product, 
                product.ProductCode, null);

            return updatedProduct;
        }
    }
}
