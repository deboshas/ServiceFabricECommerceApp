﻿using System;
using System.Collections.Generic;
using System.Fabric;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ECommerce.ProductCatalogDomain;
using Microsoft.ServiceFabric.Data.Collections;
using Microsoft.ServiceFabric.Services.Communication.Runtime;
using Microsoft.ServiceFabric.Services.Remoting.V2.FabricTransport.Runtime;
using Microsoft.ServiceFabric.Services.Runtime;

namespace ECommerce.ProductCatalog
{
    /// <summary>
    /// An instance of this class is created for each service replica by the Service Fabric runtime.
    /// </summary>
    internal sealed class ProductCatalog : StatefulService, IProductCatalogService
    {

        private IProductRepository _productRepo;
      
        public ProductCatalog(StatefulServiceContext context)
            : base(context)
        {
            _productRepo = new ProductRepository(this.StateManager);
        }

        public async Task AddProduct(Product product)
        {
            await _productRepo.AddProduct(product);
        }

        public async Task<Product[]> GetProduct()
        {
            return (await _productRepo.GetAllProducts()).ToArray();

        }

        /// <summary>
        /// Optional override to create listeners (e.g., HTTP, Service Remoting, WCF, etc.) for this service replica to handle client or user requests.
        /// </summary>
        /// <remarks>
        /// For more information on service communication, see https://aka.ms/servicefabricservicecommunication
        /// </remarks>
        /// <returns>A collection of listeners.</returns>
        protected override IEnumerable<ServiceReplicaListener> CreateServiceReplicaListeners()
        {
            return new[]
            {
                new ServiceReplicaListener(context =>
                {

                   return  new  FabricTransportServiceRemotingListener(context,this);
                })

            };
        }

        /// <summary>
        /// This is the main entry point for your service replica.
        /// This method executes when this replica of your service becomes primary and has write status.
        /// </summary>
        /// <param name="cancellationToken">Canceled when Service Fabric needs to shut down this service replica.</param>
        protected override async Task RunAsync(CancellationToken cancellationToken)
        {
            await _productRepo.AddProduct(new Product()
            {
                ProductID = new Guid(),
                Name = "product1",
                Price = 10,
                Description = "product1",
                Avialability = 20

            });

            await _productRepo.AddProduct(new Product()
            {
                ProductID = new Guid(),
                Name = "product2",
                Price = 20,
                Description = "product2",
                Avialability = 20

            });

            await _productRepo.AddProduct(new Product()
            {
                ProductID = new Guid(),
                Name = "product3",
                Price = 30,
                Description = "product3",
                Avialability = 20

            });

            await _productRepo.AddProduct(new Product()
            {
                ProductID = new Guid(),
                Name = "product4",
                Price = 40,
                Description = "product1",
                Avialability = 40

            });

            await _productRepo.AddProduct(new Product()
            {
                ProductID = new Guid(),
                Name = "product5",
                Price = 10,
                Description = "product5",
                Avialability = 20

            });

            await _productRepo.AddProduct(new Product()
            {
                ProductID = new Guid(),
                Name = "product6",
                Price = 10,
                Description = "product6",
                Avialability = 20

            });

            await _productRepo.AddProduct(new Product()
            {
                ProductID = new Guid(),
                Name = "product7",
                Price = 10,
                Description = "product7",
                Avialability = 20

            });


            await _productRepo.AddProduct(new Product()
            {
                ProductID = new Guid(),
                Name = "product8",
                Price = 10,
                Description = "product8",
                Avialability = 20

            });
        }
    }
}
