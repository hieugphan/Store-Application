using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebUI.Controllers
{
    public class PortalController : Controller
    {

        public PortalController()
        {
        }

        public IActionResult OpenCustomerPortal()
        {
            return View();
        }

        public IActionResult OpenManagerPortal()
        {
            return View();
        }


        //-------------------------------------------------------------------------------------------------------
        // GET: ProtalController
        public ActionResult Index()
        {
            return View();
        }

        // GET: ProtalController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ProtalController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProtalController/Create
        [HttpPost]
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
        }

        // GET: ProtalController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ProtalController/Edit/5
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

        // GET: ProtalController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ProtalController/Delete/5
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
