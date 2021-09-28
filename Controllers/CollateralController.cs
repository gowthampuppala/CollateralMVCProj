using CollateralMVC.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace CollateralMVC.Controllers
{
    public class CollateralController : Controller
    {
        public IActionResult Index()
        {
            IEnumerable<Collateral> collaterals = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:25168/api/");
                var responseTask = client.GetAsync("Collateral");
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readJob = result.Content.ReadAsAsync<IList<Collateral>>();
                    readJob.Wait();
                    collaterals = readJob.Result;
                }
                else
                {
                    collaterals = Enumerable.Empty<Collateral>();
                    ModelState.AddModelError(string.Empty, "server error occured. please contact support");
                }
            }
            return View(collaterals);
        }
    }
}
