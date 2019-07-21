using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECommerce.ProductCatalogDomain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.ServiceFabric.Services.Remoting.Client;
using UserActor.Interfaces;
using Microsoft.ServiceFabric.Actors.Client;
using Microsoft.ServiceFabric.Actors;

namespace FrontEndAPI.Controllers
{
    [Route("api/[controller]")]
    public class CartController : Controller
    {
        // GET api/values

       
     
        [HttpGet]
        public async Task<ApiCart> GetCart(string userId)
        {
            var productsId = await GetActor(userId).GetCart();

            return new ApiCart()
            {
                UserId = userId,
                Items = productsId.Select(product =>
                 {
                     return new ApiCartItem()
                     {
                         Quantity = product.Quantity,
                         ProductId = product.ProductId

                     };

                 }).ToArray() };

            //need to disposne actor upon exit 

        }





        // POST api/values
        [HttpPost]
        public async Task AddtoCart(string userId,[FromBody]ApiCartAddRequest productRequest)
        {
            var actor = GetActor(userId);

            await actor.AddToCart(new CartItem()
            {
                ProductId=productRequest.productid,
                Quantity=productRequest.Quantity

            }
                 );
         //need to disposne actor upon exit 

        }

      
        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task DeleteFromCart(string userid)
        {
            var actor = GetActor(userid);

            await actor.ClearCart();

            //need to disposne actor upon exit 

        }

        private IUserActor GetActor(string userId)
        {
            return ActorProxy.Create<IUserActor>(new ActorId(userId), new Uri("fabric:/E_Commerce/UserActorService"));

        }
    }
}
