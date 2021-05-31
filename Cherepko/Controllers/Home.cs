using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cherepko.Controllers
{
    public class Home : Controller
    {
        private List<DemoList> listDemo;
        public IActionResult Index()
        {
            FillDemoList();

            ViewData["Text"] = "Лабораторная работа 2";
            ViewData["Lst"] = new SelectList(listDemo, "ListItemValue", "ListItemText");

            return View();
        }

            public void FillDemoList()
            {
                listDemo = new List<DemoList>
                {
                new DemoList{ ListItemValue=1, ListItemText="Item 1"},
                new DemoList{ ListItemValue=2, ListItemText="Item 2"},
                new DemoList{ ListItemValue=3, ListItemText="Item 3"}
                };
        }
    }
}
