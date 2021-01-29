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
    [Area("Admin")]
    public class UsedActionsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UsedActionsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin/UsedActions
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.UsedAction.Include(u => u.Action).Include(u => u.Partner).Include(u => u.User);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Admin/UsedActions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usedAction = await _context.UsedAction
                .Include(u => u.Action)
                .Include(u => u.Partner)
                .Include(u => u.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (usedAction == null)
            {
                return NotFound();
            }

            return View(usedAction);
        }

        // GET: Admin/UsedActions/Create
        public IActionResult Create()
        {
            ViewData["ActionId"] = new SelectList(_context.DiscountAction, "Id", "Name");
            ViewData["PartnerId"] = new SelectList(_context.Partner, "Id", "Name");
            ViewData["UserId"] = new SelectList(_context.DiscountUser, "Id", "UserName");
            return View();
        }

        // POST: Admin/UsedActions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserId,ActionId,PartnerId,ActionValue")] UsedAction usedAction)
        {
            if (ModelState.IsValid)
            {
                _context.Add(usedAction);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ActionId"] = new SelectList(_context.DiscountAction, "Id", "Name", usedAction.ActionId);
            ViewData["PartnerId"] = new SelectList(_context.Partner, "Id", "Name", usedAction.PartnerId);
            ViewData["UserId"] = new SelectList(_context.DiscountUser, "Id", "UserName", usedAction.UserId);
            return View(usedAction);
        }

        // GET: Admin/UsedActions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usedAction = await _context.UsedAction.FindAsync(id);
            if (usedAction == null)
            {
                return NotFound();
            }
            ViewData["ActionId"] = new SelectList(_context.DiscountAction, "Id", "Name", usedAction.ActionId);
            ViewData["PartnerId"] = new SelectList(_context.Partner, "Id", "Name", usedAction.PartnerId);
            ViewData["UserId"] = new SelectList(_context.DiscountUser, "Id", "UserName", usedAction.UserId);
            return View(usedAction);
        }

        // POST: Admin/UsedActions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserId,ActionId,PartnerId,ActionValue")] UsedAction usedAction)
        {
            if (id != usedAction.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(usedAction);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsedActionExists(usedAction.Id))
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
            ViewData["ActionId"] = new SelectList(_context.DiscountAction, "Id", "Name", usedAction.ActionId);
            ViewData["PartnerId"] = new SelectList(_context.Partner, "Id", "Name", usedAction.PartnerId);
            ViewData["UserId"] = new SelectList(_context.DiscountUser, "Id", "UserName", usedAction.UserId);
            return View(usedAction);
        }

        // GET: Admin/UsedActions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usedAction = await _context.UsedAction
                .Include(u => u.Action)
                .Include(u => u.Partner)
                .Include(u => u.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (usedAction == null)
            {
                return NotFound();
            }

            return View(usedAction);
        }

        // POST: Admin/UsedActions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var usedAction = await _context.UsedAction.FindAsync(id);
            _context.UsedAction.Remove(usedAction);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UsedActionExists(int id)
        {
            return _context.UsedAction.Any(e => e.Id == id);
        }
    }
}
