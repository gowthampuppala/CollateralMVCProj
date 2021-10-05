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
    public class AddLoan : Controller
    {
        // GET: AddLoan
        public ActionResult Index()
        {
            IEnumerable<Customer_Loan> custLoans = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:56588/api/Data");
                var responseTask = client.GetAsync("");
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readJob = result.Content.ReadAsAsync<IList<Customer_Loan>>();
                    readJob.Wait();
                    custLoans = readJob.Result;
                }
                else
                {
                    custLoans = Enumerable.Empty<Customer_Loan>();
                    ModelState.AddModelError(string.Empty, "server error occured. please contact support");
                }
            }
            return View(custLoans);
            //return View();
        }

        // GET: AddLoan/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }
        // GET: AddLoan/Create
        [HttpPost]
        public ActionResult Create(Customer_Loan loan)
        {
            using (var client = new HttpClient())
            {
                //HttpClient client = new HttpClient();
                 client.DefaultRequestHeaders.Authorization =
                   new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6InN0cmluZyIsInJvbGUiOiJzdHJpbmciLCJuYmYiOjE2MzI4ODU5ODIsImV4cCI6MTYzMjg4NjI4MiwiaWF0IjoxNjMyODg1OTgyfQ.VlhJUS5D368UEQHOVt5oSfw3aMJrrLONMLmceTmQFO0");
                client.BaseAddress = new Uri("http://localhost:56588/api/Data/PostintoLoan");
                var postJob = client.PostAsJsonAsync<Customer_Loan>("", loan);
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

        // POST: AddLoan/Create
/*        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }*/

        // GET: AddLoan/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: AddLoan/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AddLoan/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AddLoan/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
