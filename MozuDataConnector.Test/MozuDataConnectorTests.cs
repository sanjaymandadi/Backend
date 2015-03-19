using System;
using Autofac;
using Mozu.Api;
using Mozu.Api.Resources.Platform;
using Mozu.Api.ToolKit;
using Mozu.Api.ToolKit.Config;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using Mozu.Api.Resources.Commerce.Catalog.Admin.Attributedefinition;
using System.Linq;
using Mozu.Api.Contracts.ProductAdmin;

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

        [TestMethod]
        public void Get_Popularity_Attribute()
        {
            var filter = "attributeFQN eq " + "'tenant~popularity'";

            var attributeHandler = new MozuDataConnector.Domain.Handlers.AttributeHandler();

            var attributes = attributeHandler.GetAttributes(_apiContext.TenantId, _apiContext.SiteId, 
                _apiContext.MasterCatalogId, 0, 20, null, filter).Result;
        }

        [TestMethod]
        public void Add_SunGlass_Style_Attribute()
        {
            var attributeFQN = "tenant~sunglass-style";
            var filter = string.Format("attributeFQN eq '{0}'", attributeFQN);

            var attributeHandler = new MozuDataConnector.Domain.Handlers.AttributeHandler();

            var attributes = attributeHandler.GetAttributes(_apiContext.TenantId, _apiContext.SiteId, 
                _apiContext.MasterCatalogId, 0, 20, null, filter).Result;
            
            //add a using clause for System.Linq;
            var existingAttribute = attributes.SingleOrDefault(a => a.AttributeFQN == attributeFQN);

            if (existingAttribute == null)
            {
                var attributeCode = attributeFQN.Replace("tenant~", string.Empty);

                var attribute = new Mozu.Api.Contracts.ProductAdmin.Attribute()
                {
                    AdminName = attributeCode,
                    AttributeFQN = attributeFQN,
                    AttributeCode = attributeCode,
                    Content = new AttributeLocalizedContent()
                    {
                        LocaleCode = "en-US",
                        Name = attributeCode.Replace("-", " ")
                    },
                    DataType = "String",
                    InputType = "List",
                    IsExtra = false,
                    IsOption = true,
                    IsProperty = true,
                    LocalizedContent = null,
                    MasterCatalogId = _apiContext.MasterCatalogId,
                    Namespace = "tenant",
                    SearchSettings = new AttributeSearchSettings()
                    {
                        SearchableInAdmin = true,
                        SearchableInStorefront = true,
                        SearchDisplayValue = true
                    },
                    ValueType = "Predefined",
                    VocabularyValues = new System.Collections.Generic.List<AttributeVocabularyValue>() 
                    {
                        new AttributeVocabularyValue()
                        {
                            Value = "Pilot",
                            Content = new Mozu.Api.Contracts.ProductAdmin.AttributeVocabularyValueLocalizedContent()
                            { 
                                  LocaleCode = "en-US",
                                  StringValue = "Pilot"
                            }
                        }
                    }

                };

                var newAttribute = attributeHandler.AddAttribute(_apiContext.TenantId, _apiContext.SiteId, 
                    _apiContext.MasterCatalogId, attribute).Result;
            };
        }

        [TestMethod]
        public void Update_SunGlass_Style_Attribute()
        {
            var attributeFQN = "tenant~sunglass-style";
            var attributeValues = "Rectangle|Rimless|Butterfly|Oval|Wrap";
            var filter = string.Format("attributeFQN eq '{0}'", attributeFQN);

            var attributeHandler = new MozuDataConnector.Domain.Handlers.AttributeHandler();
            var attributes = attributeHandler.GetAttributes(_apiContext.TenantId, _apiContext.SiteId, 
                _apiContext.MasterCatalogId, 0, 1, null, filter).Result;

            var existingAttribute = attributes.SingleOrDefault(a => a.AttributeFQN == attributeFQN);

            if (existingAttribute != null && existingAttribute.VocabularyValues != null)
            {
                foreach (var value in attributeValues.Split('|'))
                {
                    existingAttribute.VocabularyValues.Add(new AttributeVocabularyValue()
                    {
                        Value = value,
                        Content = new AttributeVocabularyValueLocalizedContent()
                        {
                            LocaleCode = "en-US",
                            StringValue = value
                        }
                    });
                }

                var newAttribute = attributeHandler.UpdateAttribute(_apiContext.TenantId, _apiContext.SiteId, 
                    _apiContext.MasterCatalogId, existingAttribute).Result;
            }
        }

        [TestMethod]
        public void Add_SunGlass_Protection_Attribute()
        {
            var attributeFQN = "tenant~sunglass-protection";
            var filter = string.Format("attributeFQN eq '{0}'", attributeFQN);

            var attributeHandler = new MozuDataConnector.Domain.Handlers.AttributeHandler();
            var attributes = attributeHandler.GetAttributes(_apiContext.TenantId, _apiContext.SiteId, 
                _apiContext.MasterCatalogId, 0, 1, null, filter).Result;

            var existingAttribute = attributes.SingleOrDefault(a => a.AttributeFQN == attributeFQN);

            if (existingAttribute == null)
            {
                var attributeCode = attributeFQN.Replace("tenant~", string.Empty);

                var attribute = new Mozu.Api.Contracts.ProductAdmin.Attribute()
                {
                    AdminName = attributeCode,
                    AttributeFQN = attributeFQN,
                    AttributeCode = attributeCode,
                    Content = new AttributeLocalizedContent()
                    {
                        LocaleCode = "en-US",
                        Name = attributeCode.Replace("-", " ")
                    },
                    DataType = "String",
                    InputType = "TextBox",
                    IsExtra = false,
                    IsOption = false,
                    IsProperty = true,
                    LocalizedContent = null,
                    MasterCatalogId = _apiContext.MasterCatalogId,
                    Namespace = "tenant",
                    SearchSettings = new AttributeSearchSettings()
                    {
                        SearchableInAdmin = true,
                        SearchableInStorefront = true,
                        SearchDisplayValue = true
                    },
                    ValueType = "AdminEntered",
                    VocabularyValues = null
                };

                var newAttribute = attributeHandler.AddAttribute(_apiContext.TenantId, _apiContext.SiteId,
                    _apiContext.MasterCatalogId, attribute).Result;
            };
        }

        [TestMethod]
        public void Get_ProductTypes()
        {
            var productTypeHandler = new MozuDataConnector.Domain.Handlers.ProductTypeHandler();

            var productTypes = productTypeHandler.GetProductTypes(_apiContext.TenantId, _apiContext.SiteId,
                _apiContext.MasterCatalogId, 0, 20, null, null).Result;
        }

        [TestMethod]
        public void Get_Purse_ProductType()
        {
            var filter = "name eq " + "'Purse'";

            var productTypeHandler = new MozuDataConnector.Domain.Handlers.ProductTypeHandler();

            var productTypes = productTypeHandler.GetProductTypes(_apiContext.TenantId, _apiContext.SiteId,
                _apiContext.MasterCatalogId, 0, 20, null, filter).Result;
        }

        [TestMethod]
        public void Add_Sunglasses_ProductType()
        {
            var productTypeName = "Sunglasses";
            var filter = string.Format("name eq '{0}'", productTypeName);

            var productTypeHandler = new MozuDataConnector.Domain.Handlers.ProductTypeHandler();

            var productTypes = productTypeHandler.GetProductTypes(_apiContext.TenantId, _apiContext.SiteId,
                _apiContext.MasterCatalogId, 0, 20, null, null).Result;

            var existingProductType = productTypes.SingleOrDefault(a => a.Name == productTypeName);

            if (existingProductType == null)
            {
                var productType = new ProductType()
                {
                    Name = productTypeName,
                    GoodsType = "Physical",
                    MasterCatalogId = _apiContext.MasterCatalogId,
                    Options = new System.Collections.Generic.List<AttributeInProductType>(),
                    Properties = new System.Collections.Generic.List<AttributeInProductType>(),
                    Extras = null,
                    IsBaseProductType = false,
                    ProductUsages = new System.Collections.Generic.List<string>
                     {
                        "Standard",
                        "Configurable",
                        "Bundle",
                        "Component"
                     },
                };

                var newProductType = productTypeHandler.AddProductType(_apiContext.TenantId, _apiContext.SiteId,
                    _apiContext.MasterCatalogId, productType).Result;
            }
        }

        [TestMethod]
        public void Update_Sunglasses_Color_Options_ProductType()
        {
            var productTypeName = "Sunglasses";
            var filter = string.Format("name eq '{0}'", productTypeName);

            var productTypeHandler = new MozuDataConnector.Domain.Handlers.ProductTypeHandler();

            var productTypes = productTypeHandler.GetProductTypes(_apiContext.TenantId, _apiContext.SiteId,
                _apiContext.MasterCatalogId, 0, 20, null, null).Result;

            var existingProductType = productTypes.SingleOrDefault(a => a.Name == productTypeName);

            if (existingProductType != null)
            {
                var attributeHandler = new MozuDataConnector.Domain.Handlers.AttributeHandler();

                var attributeColor = attributeHandler.GetAttributes(_apiContext.TenantId, _apiContext.SiteId, 
                    _apiContext.MasterCatalogId, 0, 1, null, "attributeFQN eq 'tenant~color'").Result.First();

                AttributeInProductType productTypeOptionColor = new AttributeInProductType()
                {
                    AttributeFQN = attributeColor.AttributeFQN, 
                    IsInheritedFromBaseType = false, 
                    AttributeDetail = attributeColor,
                    IsHiddenProperty = false,
                    IsMultiValueProperty = false,
                    IsRequiredByAdmin = false,
                    Order = 0,
                    VocabularyValues = new System.Collections.Generic.List<AttributeVocabularyValueInProductType>()
                };

                productTypeOptionColor.VocabularyValues.Add(new AttributeVocabularyValueInProductType()
                {
                    Value = "Black",
                    Order = 0,
                    VocabularyValueDetail = new AttributeVocabularyValue()
                    {
                        Content = new AttributeVocabularyValueLocalizedContent()
                        {
                            LocaleCode = "en-US",
                            StringValue = "Black"
                        },
                        Value = "Black",
                        ValueSequence = 10
                    }
                });

                productTypeOptionColor.VocabularyValues.Add(new AttributeVocabularyValueInProductType()
                {
                    Value = "Brown",
                    Order = 0,
                    VocabularyValueDetail = new AttributeVocabularyValue()
                    {
                        Content = new AttributeVocabularyValueLocalizedContent()
                        {
                            LocaleCode = "en-US",
                            StringValue = "Brown"
                        },
                        Value = "Brown",
                        ValueSequence = 11
                    }
                });

                existingProductType.Options.Add(productTypeOptionColor);

                var newProductType = productTypeHandler.UpdateProductType(_apiContext.TenantId, _apiContext.SiteId,
                    _apiContext.MasterCatalogId, existingProductType).Result;
            }
        }

        [TestMethod]
        public void Update_Sunglasses_Protection_Property_ProductType()
        {
            var productTypeName = "Sunglasses";
            var filter = string.Format("name eq '{0}'", productTypeName);

            var productTypeHandler = new MozuDataConnector.Domain.Handlers.ProductTypeHandler();

            var productTypes = productTypeHandler.GetProductTypes(_apiContext.TenantId, _apiContext.SiteId,
                _apiContext.MasterCatalogId, 0, 20, null, null).Result;

            var existingProductType = productTypes.SingleOrDefault(a => a.Name == productTypeName);

            if (existingProductType != null)
            {
                var attributeHandler = new MozuDataConnector.Domain.Handlers.AttributeHandler();

                var attributeProtection = attributeHandler.GetAttributes(_apiContext.TenantId, _apiContext.SiteId,
                    _apiContext.MasterCatalogId, 0, 1, null, "attributeFQN eq 'tenant~sunglass-protection'").Result.First();

                AttributeInProductType productTypePropertyProtection = new AttributeInProductType()
                {
                    AttributeFQN = attributeProtection.AttributeFQN,
                    IsInheritedFromBaseType = false,
                    AttributeDetail = attributeProtection,
                    IsHiddenProperty = false,
                    IsMultiValueProperty = false,
                    IsRequiredByAdmin = false,
                    Order = 0
                };

                existingProductType.Properties.Add(productTypePropertyProtection);

                var newProductType = productTypeHandler.UpdateProductType(_apiContext.TenantId, _apiContext.SiteId,
                    _apiContext.MasterCatalogId, existingProductType).Result;
            }
        }

        [TestMethod]
        public void Update_Sunglasses_Style_Property_ProductType()
        {
            var productTypeName = "Sunglasses";
            var filter = string.Format("name eq '{0}'", productTypeName);

            var productTypeHandler = new MozuDataConnector.Domain.Handlers.ProductTypeHandler();

            var productTypes = productTypeHandler.GetProductTypes(_apiContext.TenantId, _apiContext.SiteId,
                _apiContext.MasterCatalogId, 0, 20, null, null).Result;

            var existingProductType = productTypes.SingleOrDefault(a => a.Name == productTypeName);

            if (existingProductType != null)
            {
                var attributeHandler = new MozuDataConnector.Domain.Handlers.AttributeHandler();

                var attributeStyle = attributeHandler.GetAttributes(_apiContext.TenantId, _apiContext.SiteId,
                    _apiContext.MasterCatalogId, 0, 1, null, "attributeFQN eq 'tenant~sunglass-style'").Result.First();

                AttributeInProductType productTypePropertyStyle = new AttributeInProductType()
                {
                    AttributeFQN = attributeStyle.AttributeFQN,
                    IsInheritedFromBaseType = false,
                    AttributeDetail = attributeStyle,
                    IsHiddenProperty = false,
                    IsMultiValueProperty = true,
                    IsRequiredByAdmin = false,
                    Order = 0,
                    VocabularyValues = new System.Collections.Generic.List<AttributeVocabularyValueInProductType>()
                };

                var productTypePropertyStyleValues = "Pilot|Rectangle|Rimless";
                var seq = 30;

                foreach(var value in productTypePropertyStyleValues.Split('|'))
                {
                    productTypePropertyStyle.VocabularyValues.Add(new AttributeVocabularyValueInProductType()
                    {
                        Value = value,
                        Order = 0,
                        VocabularyValueDetail = new AttributeVocabularyValue()
                        {
                            Content = new AttributeVocabularyValueLocalizedContent()
                            {
                                LocaleCode = "en-US",
                                StringValue = value
                            },
                            Value = value,
                            ValueSequence = seq++
                        }
                    });
                }
                
                existingProductType.Properties.Add(productTypePropertyStyle);

                var newProductType = productTypeHandler.UpdateProductType(_apiContext.TenantId, _apiContext.SiteId,
                    _apiContext.MasterCatalogId, existingProductType).Result;
            }
        }

        [TestMethod]
        public void Get_Products()
        {
            var productHandler = new MozuDataConnector.Domain.Handlers.ProductHandler();

            var products = productHandler.GetProducts(_apiContext.TenantId, _apiContext.SiteId,
                _apiContext.MasterCatalogId, 0, 20, null, null).Result;
        }

        [TestMethod]
        public void Get_Purse_Product()
        {
            var productHandler = new MozuDataConnector.Domain.Handlers.ProductHandler();

            var products = productHandler.GetProduct(_apiContext.TenantId, _apiContext.SiteId,
                _apiContext.MasterCatalogId, "LUC-TOP-001").Result;
        }

        [TestMethod]
        public void Add_Sunglasses_Product()
        {
            var productTypeHandler = new MozuDataConnector.Domain.Handlers.ProductTypeHandler();

            var productTypes = productTypeHandler.GetProductTypes(_apiContext.TenantId, _apiContext.SiteId,
                _apiContext.MasterCatalogId, 0, 20, null, "name eq 'Sunglasses'").Result;
            
            var existingProductType = productTypes.SingleOrDefault(a => a.Name == "Sunglasses");

            if (existingProductType != null)
            {
                var productCode = "LUC-SUN-001";

                var productHandler = new MozuDataConnector.Domain.Handlers.ProductHandler();
                var existingProduct = productHandler.GetProduct(_apiContext.TenantId, _apiContext.SiteId,
                    _apiContext.MasterCatalogId, productCode).Result;

                if (existingProduct == null)
                {
                    var product = new Product()
                    {
                        ProductCode = productCode,
                        ProductUsage = "Configurable",
                        FulfillmentTypesSupported = new System.Collections.Generic.List<string> { "DirectShip" },
                        MasterCatalogId = 1,//_apiContext.MasterCatalogId,
                        ProductTypeId = existingProductType.Id,
                        IsValidForProductType = true,         
                        ProductInCatalogs = new System.Collections.Generic.List<ProductInCatalogInfo>
                        {
                            new ProductInCatalogInfo()
                            { 
                                //CatalogId = Convert.ToInt32(_apiContext.CatalogId),
                                IsActive = true,
                                IsContentOverridden = false,
                                IsPriceOverridden = false,
                                IsseoContentOverridden = false
                            }
                        },
                        HasConfigurableOptions = true,
                        HasStandAloneOptions = false,
                        IsVariation = false,
                        Content = new ProductLocalizedContent()
                        {
                            LocaleCode = "en-US",
                            ProductShortDescription = "This minimalistic design is a great fit for those seeking adventure.",
                            ProductName = "Commander Sunglasses",
                        },
                        Price = new ProductPrice() 
                        {
                             Price = 685.00m,
                             SalePrice = 615.00m
                        },
                        PackageWeight = new Mozu.Api.Contracts.Core.Measurement()
                        {
                            Unit = "lbs",
                            Value = .5m
                        },
                        PackageLength = new Mozu.Api.Contracts.Core.Measurement()
                        {
                             Unit = "in",
                             Value = 3.75m
                        },
                        PackageWidth = new Mozu.Api.Contracts.Core.Measurement()
                        {
                             Unit = "in",
                             Value = 5.5m
                        },
                        PackageHeight = new Mozu.Api.Contracts.Core.Measurement()
                        {
                             Unit = "in",
                             Value = 1.85m
                        }
                    };

                    var newProduct = productHandler.AddProduct(_apiContext.TenantId, _apiContext.SiteId,
                        _apiContext.MasterCatalogId, product).Result;
                }
            }
        }
    }
}