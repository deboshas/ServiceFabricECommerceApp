using ECommerce.ProductCatalogDomain;
using Microsoft.ServiceFabric.Data;
using Microsoft.ServiceFabric.Data.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ECommerce.ProductCatalog
{
    public class ProductRepository : IProductRepository
    {
        IReliableStateManager _stateManager;
        List<Product> _products;

        public ProductRepository(IReliableStateManager stateManager )
        {
            _stateManager = stateManager;
            _products = new List<Product>();
        }

        public async Task AddProduct(Product product)
        {
             var productStorage=await  _stateManager.GetOrAddAsync<IReliableDictionary<Guid, Product >>("products");

            using(var tx= _stateManager.CreateTransaction())
            {
                await productStorage.AddOrUpdateAsync(tx, product.ProductID, product, (id, value) => product);
                await tx.CommitAsync();
            }
        }

        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            var productStorage = await _stateManager.GetOrAddAsync<IReliableDictionary<Guid, Product>>("products");

            using (var tx = _stateManager.CreateTransaction())
            {
                var allProducts =await  productStorage.CreateEnumerableAsync(tx, EnumerationMode.Unordered);
                using(var enumerator=  allProducts.GetAsyncEnumerator())
                {
                    while(await enumerator.MoveNextAsync(CancellationToken.None))
                    {
                        _products.Add(enumerator.Current.Value);

                    }


                }


            }


            return _products;
        }
    }
}
