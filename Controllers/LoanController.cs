using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CollateralMVC.Models;
using System.Net.Http;
using Newtonsoft.Json;

namespace CollateralMVC.Controllers
{
    public class LoanController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Collateral loan)
        {
            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri("http://localhost:56588/saveCollaterals");
                var postJob = client.PostAsJsonAsync<Collateral>("",loan);
                postJob.Wait();
                var postResult = postJob.Result;
                if (postResult.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }

            }
            ModelState.AddModelError(string.Empty, "Server error occured");


            return View(loan);
        }
        [HttpPost]
        public async Task<IActionResult> Subscribe(SubscribeModel model)
        {


            using (var client = new HttpClient())
            {
                var CustomerLoan = new Customer_Loan();
                client.BaseAddress = new Uri("http://localhost:56588/api/Data/");
                HttpResponseMessage GetJob = await client.GetAsync($"{model.Email}");


                if (GetJob.IsSuccessStatusCode)
                {
                    var result = GetJob.Content.ReadAsStringAsync().Result;
                    CustomerLoan = JsonConvert.DeserializeObject<Customer_Loan>(result);
                    //return RedirectToAction("Index");
                }
                
               /* if (CustomerLoan.Id == 0)
                {
                    return ModelState.Count
                }*/
                return View(CustomerLoan);
            }
        }
        /*public ActionResult GetLoanDetails()
        {
            return View();
        }
        [HttpPost]
        public ActionResult GetLoanDetails(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:56588/api/Data/");
                var postJob = client.PostAsJsonAsync(id.ToString(), id);
                postJob.Wait();
                var postResult = postJob.Result;
                if (postResult.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }

            }
            ModelState.AddModelError(string.Empty, "Server error occured");


            return View();
        }*/
        /* [HttpGet]
         public ActionResult Edit(int id)
         {
             return View();
         }
         public ActionResult Edit(Collateral collaterals)
         {
            return RedirectToAction("Index");
         }*/


    }
}
