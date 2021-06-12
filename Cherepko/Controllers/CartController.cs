using CherepkoLib.Data;
using Cherepko.Extensions;
using Cherepko.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cherepko.Controllers
{
    public class CartController : Controller
    {
        private ApplicationDbContext context;
        //private string cartKey = "cart";
        //private Cart _cart;
        //  Cart Cart = new Cart();

        private Cart _cart;
        public CartController(ApplicationDbContext Context, Cart cart)
        {
            context = Context;
            _cart = cart;
        }
        public IActionResult Index()
        {
            //  _cart = HttpContext.Session.Get<Cart>(cartKey);
            return View(_cart.Items.Values);
        }
        [Authorize]
        public IActionResult Add(int id, string returnUrl)
        {
            //  _cart = HttpContext.Session.Get<Cart>(cartKey);
            var item = context.Rods.Find(id);
            if (item != null)
            {
                _cart.AddToCart(item);
            }
            return Redirect(returnUrl);
        }
        public IActionResult Delete(int id)
        {
            _cart.RemoveFromCart(id);
            return RedirectToAction("Index");
        }
    }
}


