using CollateralMVC.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace CollateralMVC.Controllers
{
    public class RiskAssessment : Controller
    {
        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(CollateralController));

        // GET: RiskAssessment
        public ActionResult Index()
        {
            IEnumerable<Collateral> collaterals = null;
            using (var client = new HttpClient())
            {
                _log4net.Info("Risk assessment started");
                client.BaseAddress = new Uri("http://localhost:2960/api/RiskData/");
                var responseTask = client.GetAsync("");
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    _log4net.Info("Risk assessment finished");
                    var readJob = result.Content.ReadAsAsync<IList<Collateral>>();
                    readJob.Wait();
                    collaterals = readJob.Result;
                    return View(collaterals);
                }
                else
                {
                    _log4net.Info("No Risks Found");
                    collaterals = Enumerable.Empty<Collateral>();
                    ModelState.AddModelError(string.Empty, "server error occured. please contact support");
                }
            }
            return View();
        }

        // GET: RiskAssessment/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }
        // GET: RiskAssessment/Create
        public ActionResult Create()
        {
            return View();
        }
    }
}
