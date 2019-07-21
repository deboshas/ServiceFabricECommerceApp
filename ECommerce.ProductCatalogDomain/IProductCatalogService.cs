using Microsoft.ServiceFabric.Services.Remoting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.ProductCatalogDomain
{
  public   interface IProductCatalogService :IService
    {

        Task<Product[]> GetProduct();

        Task AddProduct(Product product);

    }
}
