using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CollateralMVC.Models;
using System.Net.Http;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;

namespace CollateralMVC.Controllers
{
    public class LoanController : Controller
    {
        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(CollateralController));
 
        [Authorize]
        public IActionResult Index()
        {
            return View();
        }
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Collateral loan)
        {
            using (var client = new HttpClient())
            {
                _log4net.Info("View for Create collateral for a sanctioned loan");
                client.BaseAddress = new Uri("http://localhost:56588/saveCollaterals");
                var postJob = client.PostAsJsonAsync<Collateral>("", loan);
                postJob.Wait();
                var postResult = postJob.Result;
                if (postResult.IsSuccessStatusCode)
                {
                    _log4net.Info("Return to View if adding is succesfull");
                    return RedirectToAction("Index");
                }
                else
                {
                    _log4net.Error("Displaying Error when posting job is failed");
                    ModelState.AddModelError(string.Empty, "Collaterals already added to Specified Loan ID");
                }
            }  
            return View(loan);
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Subscribe(SubscribeModel model)
        { 
            using (var client = new HttpClient())
            {
                _log4net.Info("Get Loan By Id");
                var CustomerLoan = new Customer_Loan();
                client.BaseAddress = new Uri("http://localhost:56588/api/Data/");
                HttpResponseMessage GetJob = await client.GetAsync($"{model.Email}");
                if (GetJob.IsSuccessStatusCode)
                {
                    _log4net.Info("return Info To View");
                    var result = GetJob.Content.ReadAsStringAsync().Result;
                    CustomerLoan = JsonConvert.DeserializeObject<Customer_Loan>(result);
                    //return RedirectToAction("Index");
                    return View(CustomerLoan);
                }
                else
                {
                    _log4net.Info("Data Not found");
                    ModelState.AddModelError(string.Empty, "Collaterals already added to Specified Loan ID");
                    ViewBag.Message = String.Format("No Data Found");
                    return View();
                }

            }
        }
    }
}
