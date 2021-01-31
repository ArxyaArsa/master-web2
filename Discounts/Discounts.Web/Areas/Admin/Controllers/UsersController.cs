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
    [Area("Admin")]
    public class UsersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserFactory _userFactory;

        public UsersController(ApplicationDbContext context, UserFactory userFactory)
        {
            _context = context;
            _userFactory = userFactory;
        }

        // GET: Admin/Users
        public async Task<IActionResult> Index()
        {
            var users = _userFactory.GetAllUsers();
            return View(users);
        }

        // GET: Admin/Users/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userModel = _userFactory.GetUser(id);
            if (userModel == null)
            {
                return NotFound();
            }

            return View(userModel);
        }

        // GET: Admin/Users/Create
        public IActionResult Create()
        {
            ViewData["PartnerId"] = new SelectList(_context.Set<Partner>(), "Id", "Name");
            ViewData["Roles"] = new SelectList(_userFactory.GetAllRoles(), "Name", "Name", new List<string>());
            return View();
        }

        // POST: Admin/Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PartnerId,Id,UserName,NormalizedUserName,Email,NormalizedEmail,EmailConfirmed,PasswordHash,SecurityStamp,ConcurrencyStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEnd,LockoutEnabled,AccessFailedCount,Roles")] UserModel userModel)
        {
            if (ModelState.IsValid)
            {
                _userFactory.CreateUser(userModel);
                return RedirectToAction(nameof(Index));
            }
            ViewData["PartnerId"] = new SelectList(_context.Set<Partner>(), "Id", "Name", userModel.PartnerId);
            return View(userModel);
        }

        // GET: Admin/Users/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userModel = _userFactory.GetUser(id);
            if (userModel == null)
            {
                return NotFound();
            }
            ViewData["PartnerId"] = new SelectList(_context.Set<Partner>(), "Id", "Name", userModel.PartnerId);
            ViewData["Roles"] = new SelectList(_userFactory.GetAllRoles(), "Name", "Name", userModel.Roles);
            return View(userModel);
        }

        // POST: Admin/Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PartnerId,Id,UserName,NormalizedUserName,Email,NormalizedEmail,EmailConfirmed,PasswordHash,SecurityStamp,ConcurrencyStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEnd,LockoutEnabled,AccessFailedCount,Roles")] UserModel userModel)
        {
            if (id != userModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _userFactory.UpdateUser(userModel);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_userFactory.UserExists(userModel.Id))
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
            ViewData["PartnerId"] = new SelectList(_context.Set<Partner>(), "Id", "Name", userModel.PartnerId);
            ViewData["Roles"] = new SelectList(_userFactory.GetAllRoles(), "Name", "Name", userModel.Roles);
            return View(userModel);
        }

        // GET: Admin/Users/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var discountsUser = await _context.Users
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
            var discountsUser = await _context.Users.FindAsync(id);
            _context.Users.Remove(discountsUser);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
