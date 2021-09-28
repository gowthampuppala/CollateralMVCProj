using CollateralMVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace CollateralMVC.Controllers
{
    public class RiskController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [Authorize]
        public IActionResult Subscribe(Risk model)
        {
            IEnumerable<Collateral> collaterals = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:2960/api/RiskData/");
                var responseTask = client.GetAsync($"{model.goldRate}/{model.LandRate}");
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
                return View(collaterals);
            }
            //return Ok();


            /*using (var client = new HttpClient())
            {
                var Collaterals= new Collateral();
                client.BaseAddress = new Uri("http://localhost:2960/api/RiskData/");
                HttpResponseMessage GetJob = await client.GetAsync($"{model.goldRate}/{model.LandRate}");


                if (GetJob.IsSuccessStatusCode)
                {
                    var result = GetJob.Content.ReadAsStringAsync().Result;
                    Collaterals = JsonConvert.DeserializeObject<Collateral>(result);
                    //return RedirectToAction("Index");
                }
                *//* if (CustomerLoan.Id == 0)
                 {
                     return ModelState.Count
                 }*//*
                return View(Collaterals);
            }*/
        }
    }
}
