using CollateralMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace CollateralMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
       /* public async Task<IActionResult> Details(int id)
        {
            using (var client = new HttpClient())
            {
                var CustomerLoan = new Customer_Loan();
                client.BaseAddress = new Uri("http://localhost:56588/api/Data/");
                HttpResponseMessage GetJob = await client.GetAsync($"{id}");
                
                
                if (GetJob.IsSuccessStatusCode)
                {
                    var result = GetJob.Content.ReadAsStringAsync().Result;
                    CustomerLoan = JsonConvert.DeserializeObject<Customer_Loan>(result);
                    //return RedirectToAction("Index");
                }
                return View(CustomerLoan);
            }
            

        }*/  
        /*[HttpPost]
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
                    return View(CustomerLoan);
                
                
            } 
        }*/

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
