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
        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(CollateralController));

        [Authorize]
        public IActionResult Index()
        {
            //ViewBag["Hi"] = "Hi";
           // ViewBag.Message = String.Format("Hello{0}.\\ncurrent Date and time:{1}", "Ra", DateTime.Now.ToString());
            return View();
        }
        
        [HttpPost]
        
        public IActionResult Subscribe(Risk model)
        {

           // ModelState.AddModelError(string.Empty, "server error occured. please contact support");
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
                
            }
        }
    }