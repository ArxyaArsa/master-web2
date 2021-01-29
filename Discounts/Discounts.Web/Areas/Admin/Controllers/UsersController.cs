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
    public class UsersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UsersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin/Users
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.DiscountUser.Include(d => d.Partner);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Admin/Users/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var discountsUser = await _context.DiscountUser
                .Include(d => d.Partner)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (discountsUser == null)
            {
                return NotFound();
            }

            return View(discountsUser);
        }

        // GET: Admin/Users/Create
        public IActionResult Create()
        {
            ViewData["PartnerId"] = new SelectList(_context.Set<Partner>(), "Id", "Id");
            return View();
        }

        // POST: Admin/Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PartnerId,Id,UserName,NormalizedUserName,Email,NormalizedEmail,EmailConfirmed,PasswordHash,SecurityStamp,ConcurrencyStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEnd,LockoutEnabled,AccessFailedCount")] DiscountsUser discountsUser)
        {
            if (ModelState.IsValid)
            {
                _context.Add(discountsUser);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PartnerId"] = new SelectList(_context.Set<Partner>(), "Id", "Id", discountsUser.PartnerId);
            return View(discountsUser);
        }

        // GET: Admin/Users/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var discountsUser = await _context.DiscountUser.FindAsync(id);
            if (discountsUser == null)
            {
                return NotFound();
            }
            ViewData["PartnerId"] = new SelectList(_context.Set<Partner>(), "Id", "Id", discountsUser.PartnerId);
            return View(discountsUser);
        }

        // POST: Admin/Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PartnerId,Id,UserName,NormalizedUserName,Email,NormalizedEmail,EmailConfirmed,PasswordHash,SecurityStamp,ConcurrencyStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEnd,LockoutEnabled,AccessFailedCount")] DiscountsUser discountsUser)
        {
            if (id != discountsUser.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(discountsUser);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DiscountsUserExists(discountsUser.Id))
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
            ViewData["PartnerId"] = new SelectList(_context.Set<Partner>(), "Id", "Id", discountsUser.PartnerId);
            return View(discountsUser);
        }

        // GET: Admin/Users/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var discountsUser = await _context.DiscountUser
                .Include(d => d.Partner)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (discountsUser == null)
            {
                return NotFound();
            }

            return View(discountsUser);
        }

        // POST: Admin/Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var discountsUser = await _context.DiscountUser.FindAsync(id);
            _context.DiscountUser.Remove(discountsUser);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DiscountsUserExists(int id)
        {
            return _context.DiscountUser.Any(e => e.Id == id);
        }
    }
}
