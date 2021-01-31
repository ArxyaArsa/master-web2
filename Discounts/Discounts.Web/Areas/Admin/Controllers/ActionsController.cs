using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Discounts.DataLayer;
using Discounts.DataLayer.Models;

namespace Discounts.Web.Areas.Admin.Controllers
{
    public class ActionsController : AdminBaseController
    {
        private readonly ApplicationDbContext _context;

        public ActionsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin/Actions
        public async Task<IActionResult> Index()
        {
            return View(await _context.DiscountAction.ToListAsync());
        }

        // GET: Admin/Actions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var discountAction = await _context.DiscountAction
                .FirstOrDefaultAsync(m => m.Id == id);
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
        public async Task<IActionResult> Create([Bind("Id,Name,Description,CashValue,PercentValue,CreatedDate,StartDate,EndDate,IsCanceled,CancelDate,CancelReason")] DiscountAction discountAction)
        {
            if (ModelState.IsValid)
            {
                _context.Add(discountAction);
                await _context.SaveChangesAsync();
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

            var discountAction = await _context.DiscountAction.FindAsync(id);
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
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,CashValue,PercentValue,CreatedDate,StartDate,EndDate,IsCanceled,CancelDate,CancelReason")] DiscountAction discountAction)
        {
            if (id != discountAction.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(discountAction);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DiscountActionExists(discountAction.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
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

            var discountAction = await _context.DiscountAction
                .FirstOrDefaultAsync(m => m.Id == id);
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
            var discountAction = await _context.DiscountAction.FindAsync(id);
            _context.DiscountAction.Remove(discountAction);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DiscountActionExists(int id)
        {
            return _context.DiscountAction.Any(e => e.Id == id);
        }
    }
}
