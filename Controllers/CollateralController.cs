using CollateralMVC.Models;
using Microsoft.AspNetCore.Authorization;
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
        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(CollateralController));
 
        [Authorize]
        public IActionResult Index()
        {

            _log4net.Info("Displaying Index Method of Collateral Controller");
            IEnumerable<Collateral> collaterals = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:25168/api/");
                var responseTask = client.GetAsync("Collateral");
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    _log4net.Info("Get All Collaterals from web api");
                    var readJob = result.Content.ReadAsAsync<IList<Collateral>>();
                    readJob.Wait();
                    collaterals = readJob.Result;
                }
                else
                {
                    _log4net.Info("If Collateral for Loan Id is already added error is generated");
                    collaterals = Enumerable.Empty<Collateral>();
                    ModelState.AddModelError(string.Empty, "server error occured. please contact support");
                }
            }
            _log4net.Info("return collateral list to View");
            return View(collaterals);
        }
    }
}
