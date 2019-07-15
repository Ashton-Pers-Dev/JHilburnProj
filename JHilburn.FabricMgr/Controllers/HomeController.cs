using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using JHilburn.FabricMgr.Models;
using JHilburn.FabricMgr.Services;
using Microsoft.AspNetCore.Http;

namespace JHilburn.FabricMgr.Controllers
{
    public class HomeController : Controller
    {
        private readonly FabricsService api = new FabricsService();
        // GET: Fabrics
        public IActionResult Index()
        {
            try
            {
                var res = api.GetAllAsync();
                var vm = new FabricListViewModel(res.Result.ToList());

                return View(vm);
            }
            catch(Exception ex)
            {
                var err = new ErrorViewModel();
                err.ErrorMessage = ex.Message;
                err.TargetSite = ex.TargetSite.ToString();
                err.Trace = ex.StackTrace.ToString();
                return View("Error", err);
            }
        }


        public IActionResult FabricDetail(int id)
        {
            try
            {
                var res = api.GetFabricAsync(id);
                return View(res.Result);
            }
            catch(Exception ex)
            {
                var err = new ErrorViewModel();
                err.ErrorMessage = ex.Message;
                err.TargetSite = ex.TargetSite.ToString();
                err.Trace = ex.StackTrace.ToString();
                return View("Error", err);
            }

        }

        public IActionResult AddNew()
        {
            var vm = new FabricViewModel();
            return View(vm);
        }


        public IActionResult Create(FabricViewModel fabric)
        {
            try
            {
                var res = api.AddFabricAsync(fabric);
                var uri = res.Result;

                return View("FabricDetail", fabric);
            }
            catch(Exception ex)
            {
                var err = new ErrorViewModel();
                err.ErrorMessage = ex.Message;
                err.TargetSite = ex.TargetSite.ToString();
                err.Trace = ex.StackTrace.ToString();
                return View("Error", err);
            }

        }

        public IActionResult Edit(FabricViewModel fabric)
        {
            return View(fabric);
        }

        public IActionResult UpdateFabric(FabricViewModel fabric)
        {

            try
            {
                var res = api.UpdateFabric(fabric);
                var fb = res.Result;
                return View("FabricDetail", fb);
            }
            catch(Exception ex)
            {
                var err = new ErrorViewModel();
                err.ErrorMessage = ex.Message;
                err.TargetSite = ex.TargetSite.ToString();
                err.Trace = ex.StackTrace.ToString();
                return View("Error", err);
            }

        }

        public IActionResult Delete(int id)
        {
            try
            {
                var res = api.Delete(id);
                var status = res.Result;

                var resp = api.GetAllAsync();
                var vm = new FabricListViewModel(resp.Result.ToList());

                return View("Index", vm);
            }
            catch(Exception ex)
            {
                var err = new ErrorViewModel();
                err.ErrorMessage = ex.Message;
                err.TargetSite = ex.TargetSite.ToString();
                err.Trace = ex.StackTrace.ToString();
                return View("Error", err);
            }

        }

    }
}
