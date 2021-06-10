using Cherepko.Extensions;
using Cherepko.Models;
using CherepkoLib.Data;
using CherepkoLib.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cherepko.Controllers
{
    public class ProductController:Controller
    {
        //public List<Rod> rods;
        //List<RodGroup> rodGroups;
        ApplicationDbContext context;
        int pageSize;

        public ProductController(ApplicationDbContext Context)
        {
            pageSize = 3;
            context = Context;
            //SetupData();
        }

        [Route("Catalog")]
        [Route("Catalog/Page_{pageNo}")]
        public IActionResult Index(int? group, int pageNo = 1)
        {
            //var items = rods
            //.Skip((pageNo - 1) * pageSize)
            //.Take(pageSize)
            //.ToList();
            //return View(rods);
            var rodsFiltered = context.Rods.Where(d => !group.HasValue || d.RodGroupId == group.Value);
            ViewData["Groups"] = context.RodGroups;
            ViewData["CurrentGroup"] = group ?? 0;
            //return View(ListViewModel<Rod>.GetModel(rodsFiltered, pageNo, pageSize));
            var model = ListViewModel<Rod>.GetModel(rodsFiltered, pageNo, pageSize);

            //if (Request.Headers["x-requested-with"].ToString().ToLower().Equals("xmlhttprequest"))
            //    return PartialView("_listpartial", model);
            //else
            //    return View(model);
            if (Request.IsAjaxRequest())
                return PartialView("_listpartial", model);
            else
                return View(model);
        }

        //private void SetupData()
        //{
        //    rodGroups = new List<RodGroup>
        //        {
        //        new RodGroup {RodGroupId=1, GroupName="Picker"},
        //        new RodGroup {RodGroupId=2, GroupName="Medium"},
        //        new RodGroup {RodGroupId=3, GroupName="Heavy"},
        //        };

        //    rods = new List<Rod>
        //    {
        //        new Rod {RodId = 1, RodName="ZEMEX Hi-Pro Super Feeder",
        //        Description="Длина 10ft тест 50g",
        //        Price =320.00f, RodGroupId=1, Image="hi_pro_new_image_10.jpg" },
        //        new Rod { RodId = 2, RodName="ZEMEX Razer F-1 Carp Mini Feeder",
        //        Description="Длина 11ft тест 60g",
        //        Price =560.00f, RodGroupId=1, Image="razer_new_image_11.jpg" },
        //        new Rod { RodId = 3, RodName="ZEMEX Iron Feeder",
        //        Description="Длина 10ft тест 40g",
        //        Price =180.00f, RodGroupId=1, Image="iron_feeder_image_10.jpg" },

        //        new Rod {RodId = 4, RodName="ZEMEX Hi-Pro Super Feeder",
        //        Description="Длина 12ft тест 80g",
        //        Price =350.00f, RodGroupId=2, Image="hi_pro_new_image_10.jpg" },
        //        new Rod { RodId = 5, RodName="ZEMEX Razer F-1 Carp Mini Feeder",
        //        Description="Длина 12ft тест 80g",
        //        Price =570.00f, RodGroupId=2, Image="razer_new_image_11.jpg" },
        //        new Rod { RodId = 6, RodName="ZEMEX Iron Feeder",
        //        Description="Длина 12ft тест 90g",
        //        Price =200.00f, RodGroupId=2, Image="iron_feeder_image_10.jpg" },

        //        new Rod {RodId = 7, RodName="ZEMEX Hi-Pro Super Feeder",
        //        Description="Длина 13ft тест 140g",
        //        Price =400.00f, RodGroupId=3, Image="hi_pro_new_image_10.jpg" },
        //        new Rod { RodId = 8, RodName="ZEMEX Razer Method Feeder",
        //        Description="Длина 14ft тест 140g",
        //        Price =700.00f, RodGroupId=3, Image="razer_new_image_11.jpg" },
        //        new Rod { RodId = 9, RodName="ZEMEX Iron Feeder",
        //        Description="Длина 13ft тест 140g",
        //        Price =220.00f, RodGroupId=3, Image="iron_feeder_image_10.jpg" }


        //    };
        //}
    }
}
