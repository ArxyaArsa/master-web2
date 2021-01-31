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
using Discounts.Services.Helpers;
using Discounts.Web.Areas.Admin.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Discounts.Web.Areas.Admin.Controllers
{
    public class UsersController : AdminBaseController
    {
        #region non-actions
        private readonly UserFactory _userFactory;
        private readonly PartnerFactory _partnerFactory;

        public UsersController(UserFactory userFactory, PartnerFactory partnerFactory)
        {
            _userFactory = userFactory;
            _partnerFactory = partnerFactory;
        }
        #endregion

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
            ViewData["PartnerId"] = new SelectList(_partnerFactory.GetAll(), "Id", "Name");
            ViewData["Roles"] = new SelectList(_userFactory.GetAllRoles(), "Name", "Name");
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
            ViewData["PartnerId"] = new SelectList(_partnerFactory.GetAll(), "Id", "Name");
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
            ViewData["PartnerId"] = new SelectList(_partnerFactory.GetAll(), "Id", "Name");
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
            ViewData["PartnerId"] = new SelectList(_partnerFactory.GetAll(), "Id", "Name");
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

            var userModel = _userFactory.GetUser(id);
            if (userModel == null)
            {
                return NotFound();
            }

            return View(userModel);
        }

        // POST: Admin/Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                _userFactory.DeleteUser(id);
                return RedirectToAction(nameof(Index));
            }
            catch (InvalidOperationException e)
            {
                // log error here

                if ((e.Message ?? "").Equals(ServicesConstants.DeleteUser_NotAllowedReasonMessage_UserHasUsedActions))
                {
                    return View("CustomError", new CustomErrorViewModel()
                    {
                         HeaderMessage = "Not allowed",
                         Message = e.Message,
                         ReturnUrls = new Dictionary<string, string>()
                         {
                             { "Back to Delete User page", Url.Action("Delete", "Users", new { area = "Admin", id = id }) },
                             { "Go to this User details", Url.Action("Details", "Users", new { area = "Admin", id = id }) },
                             { "Go to Users", Url.Action("Index", "Users", new { area = "Admin" }) },
                         }
                    });
                }
                else
                    return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                // log error here
                
                return RedirectToAction(nameof(Index));
            }
        }
    }
}
