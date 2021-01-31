using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Discounts.DataLayer;
using Discounts.DataLayer.Models;
using Discounts.Web.Factories;
using Discounts.Services.Models;

namespace Discounts.Web.Areas.Admin.Controllers
{
    public class ActionsController : AdminBaseController
    {
        private readonly ActionFactory _actionFactory;

        public ActionsController(ActionFactory actionFactory)
        {
            _actionFactory = actionFactory;
        }

        // GET: Admin/Actions
        public async Task<IActionResult> Index()
        {
            return View(_actionFactory.GetAllActions());
        }

        // GET: Admin/Actions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var discountAction = _actionFactory.GetAction(id);
            if (discountAction == null)
            {
                return NotFound();
            }

            return View(discountAction);
        }

        // GET: Admin/Actions/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Actions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,CashValue,PercentValue,CreatedDate,StartDate,EndDate,IsCanceled,CancelDate,CancelReason")] ActionModel discountAction)
        {
            if (ModelState.IsValid)
            {
                _actionFactory.CreateAction(discountAction);
                return RedirectToAction(nameof(Index));
            }
            return View(discountAction);
        }

        // GET: Admin/Actions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var discountAction = _actionFactory.GetAction(id);
            if (discountAction == null)
            {
                return NotFound();
            }
            return View(discountAction);
        }

        // POST: Admin/Actions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,CashValue,PercentValue,CreatedDate,StartDate,EndDate,IsCanceled,CancelDate,CancelReason")] ActionModel discountAction)
        {
            if (id != discountAction.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _actionFactory.UpdateAction(discountAction);
                return RedirectToAction(nameof(Index));
            }
            return View(discountAction);
        }

        // GET: Admin/Actions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var discountAction = _actionFactory.GetAction(id);
            if (discountAction == null)
            {
                return NotFound();
            }

            return View(discountAction);
        }

        // POST: Admin/Actions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            _actionFactory.DeleteAction(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
