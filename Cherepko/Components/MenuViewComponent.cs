using Cherepko.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cherepko.Components
{
    public class MenuViewComponent : ViewComponent
    {
        private List<MenuItem> menuItems = new List<MenuItem>
        {
        new MenuItem{ Controller="Home", Action="Index", Text="Lab 2"},
        new MenuItem{ Controller="Product", Action="Index", Text="Каталог"},
        new MenuItem{ IsPage=true, Area="Admin", Page="/Index", Text="Администрирование"}
        };

        public IViewComponentResult Invoke()
        {
            var controller = ViewContext.RouteData.Values["controller"];
            var page = ViewContext.RouteData.Values["page"];
            var area = ViewContext.RouteData.Values["area"];
            foreach (var item in menuItems)
            {
                var matchController = controller?.Equals(item.Controller) ?? false;
                var matchArea = area?.Equals(item.Area) ?? false;
                if (matchController || matchArea)
                {
                    item.Active = "active";
                }
            }
            return View(menuItems);
        }
    }
}
