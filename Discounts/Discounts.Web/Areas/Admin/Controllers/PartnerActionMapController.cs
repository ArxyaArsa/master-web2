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
    public class PartnerActionMapController : AdminBaseController
    {
        private readonly ApplicationDbContext _context;

        public PartnerActionMapController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin/PartnerActionMap
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.PartnerActionMap.Include(p => p.Action).Include(p => p.Partner);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Admin/PartnerActionMap/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var partnerActionMap = await _context.PartnerActionMap
                .Include(p => p.Action)
                .Include(p => p.Partner)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (partnerActionMap == null)
            {
                return NotFound();
            }

            return View(partnerActionMap);
        }

        // GET: Admin/PartnerActionMap/Create
        public IActionResult Create()
        {
            ViewData["ActionId"] = new SelectList(_context.DiscountAction, "Id", "Name");
            ViewData["PartnerId"] = new SelectList(_context.Partner, "Id", "Name");
            return View();
        }

        // POST: Admin/PartnerActionMap/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PartnerId,ActionId,CreatedDate")] PartnerActionMap partnerActionMap)
        {
            if (ModelState.IsValid)
            {
                _context.Add(partnerActionMap);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ActionId"] = new SelectList(_context.DiscountAction, "Id", "Name", partnerActionMap.ActionId);
            ViewData["PartnerId"] = new SelectList(_context.Partner, "Id", "Name", partnerActionMap.PartnerId);
            return View(partnerActionMap);
        }

        // GET: Admin/PartnerActionMap/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var partnerActionMap = await _context.PartnerActionMap.FindAsync(id);
            if (partnerActionMap == null)
            {
                return NotFound();
            }
            ViewData["ActionId"] = new SelectList(_context.DiscountAction, "Id", "Name", partnerActionMap.ActionId);
            ViewData["PartnerId"] = new SelectList(_context.Partner, "Id", "Id", partnerActionMap.PartnerId);
            return View(partnerActionMap);
        }

        // POST: Admin/PartnerActionMap/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,PartnerId,ActionId,CreatedDate")] PartnerActionMap partnerActionMap)
        {
            if (id != partnerActionMap.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(partnerActionMap);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PartnerActionMapExists(partnerActionMap.Id))
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
            ViewData["ActionId"] = new SelectList(_context.DiscountAction, "Id", "Name", partnerActionMap.ActionId);
            ViewData["PartnerId"] = new SelectList(_context.Partner, "Id", "Id", partnerActionMap.PartnerId);
            return View(partnerActionMap);
        }

        // GET: Admin/PartnerActionMap/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var partnerActionMap = await _context.PartnerActionMap
                .Include(p => p.Action)
                .Include(p => p.Partner)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (partnerActionMap == null)
            {
                return NotFound();
            }

            return View(partnerActionMap);
        }

        // POST: Admin/PartnerActionMap/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var partnerActionMap = await _context.PartnerActionMap.FindAsync(id);
            _context.PartnerActionMap.Remove(partnerActionMap);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PartnerActionMapExists(int id)
        {
            return _context.PartnerActionMap.Any(e => e.Id == id);
        }
    }
}
