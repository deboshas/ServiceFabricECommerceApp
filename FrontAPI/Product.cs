using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrontAPI
{
    public class ApiProduct
    {
        public Guid ProductID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public bool Avialability { get; set; }

    }
}
