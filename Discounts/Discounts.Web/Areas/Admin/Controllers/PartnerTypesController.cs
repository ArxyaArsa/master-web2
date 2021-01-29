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
    public class PartnerTypesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PartnerTypesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin/PartnerTypes
        public async Task<IActionResult> Index()
        {
            return View(await _context.PartnerType.ToListAsync());
        }

        // GET: Admin/PartnerTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var partnerType = await _context.PartnerType
                .FirstOrDefaultAsync(m => m.Id == id);
            if (partnerType == null)
            {
                return NotFound();
            }

            return View(partnerType);
        }

        // GET: Admin/PartnerTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/PartnerTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] PartnerType partnerType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(partnerType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(partnerType);
        }

        // GET: Admin/PartnerTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var partnerType = await _context.PartnerType.FindAsync(id);
            if (partnerType == null)
            {
                return NotFound();
            }
            return View(partnerType);
        }

        // POST: Admin/PartnerTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] PartnerType partnerType)
        {
            if (id != partnerType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(partnerType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PartnerTypeExists(partnerType.Id))
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
            return View(partnerType);
        }

        // GET: Admin/PartnerTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var partnerType = await _context.PartnerType
                .FirstOrDefaultAsync(m => m.Id == id);
            if (partnerType == null)
            {
                return NotFound();
            }

            return View(partnerType);
        }

        // POST: Admin/PartnerTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var partnerType = await _context.PartnerType.FindAsync(id);
            _context.PartnerType.Remove(partnerType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PartnerTypeExists(int id)
        {
            return _context.PartnerType.Any(e => e.Id == id);
        }
    }
}
