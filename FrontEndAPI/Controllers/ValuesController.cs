using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECommerce.ProductCatalogDomain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.ServiceFabric.Services.Remoting.Client;

namespace FrontEndAPI.Controllers
{
    [Route("api/[controller]")]
    public class ProductController : Controller
    {
        // GET api/values

        private IProductCatalogService _productCatalogService;
        public ProductController()
        {
            var proxyFactory = new ServiceProxyFactory(context => new Microsoft.ServiceFabric.Services.Remoting.V2.FabricTransport.Client.FabricTransportServiceRemotingClientFactory());
            _productCatalogService = proxyFactory.CreateServiceProxy<IProductCatalogService>(
                new Uri("fabric:/E_Commerce/ECommerce.ProductCatalog"),
                new Microsoft.ServiceFabric.Services.Client.ServicePartitionKey(0)
                );




        }

        [HttpGet]
        public async Task<IEnumerable<ApiProduct>> GetProduct()
        {

            var products = await _productCatalogService.GetProduct();

            return products.Select<Product, ApiProduct>(product =>
            {
                return new ApiProduct()
                {
                    Name = product.Name,
                    Avialability = product.Avialability > 0,
                    ProductID = product.ProductID,
                    Description = product.Description,
                    Price = product.Price
                };


            });







        }


        // POST api/values
        [HttpPost]
        public async Task AddProduct([FromBody]ApiProduct product)
        {
            var newProduct = new Product()
            {
                Name = product.Name,
                ProductID = product.ProductID,
                Avialability = 100,
                Description = product.Description,
                Price = product.Price


            };

            await _productCatalogService.AddProduct(newProduct);

        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
