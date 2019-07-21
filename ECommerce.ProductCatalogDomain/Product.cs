using System;

namespace ECommerce.ProductCatalogDomain
{
    public class Product
    {
        public Guid ProductID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public int Avialability { get; set; }
     

    }
}
