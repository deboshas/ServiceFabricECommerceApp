using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrontEndAPI
{
    
        public class ApiCart
        {
            public string UserId { get; set; }
            public ApiCartItem[] Items { get; set; }
        

        }

    public class ApiCartItem
    {
        public string ProductId { get; set; }
        public int Quantity { get; set; }

    }
}
