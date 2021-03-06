using Cherepko.Extensions;
using Cherepko.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cherepko.Components
{
    public class CartViewComponent: ViewComponent
    {
        private Cart _cart;
        public CartViewComponent(Cart cart)
        {
            _cart = cart;
        }
        public IViewComponentResult Invoke()
        {

            //  var cart = HttpContext.Session.Get<Cart>("cart");
            return View(_cart);
        }

    }
}
