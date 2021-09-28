using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CollateralMVC.Controllers
{
    public class LoanByID : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
