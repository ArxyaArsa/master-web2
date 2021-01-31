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

namespace Discounts.Web.Areas.Admin.Controllers
{
    public class UsedActionsController : AdminBaseController
    {
        private readonly UsedActionFactory _factory;
        private readonly PartnerFactory _partnerFactory;
        private readonly ActionFactory _actionFactory;
        private readonly UserFactory _userFactory;

        public UsedActionsController(UsedActionFactory factory, UserFactory userFactory, ActionFactory actionFactory, PartnerFactory partnerFactory)
        {
            _factory = factory;
            _partnerFactory = partnerFactory;
            _actionFactory = actionFactory;
            _userFactory = userFactory;
        }

        // GET: Admin/UsedActions
        public async Task<IActionResult> Index()
        {
            return View(_factory.GetAll());
        }

        // GET: Admin/UsedActions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usedAction = _factory.Get(id);
            if (usedAction == null)
            {
                return NotFound();
            }

            return View(usedAction);
        }

        // GET: Admin/UsedActions/Create
        public IActionResult Create()
        {
            ViewData["ActionId"] = new SelectList(_actionFactory.GetAll(), "Id", "Name");
            ViewData["PartnerId"] = new SelectList(_partnerFactory.GetAll(), "Id", "Name");
            ViewData["UserId"] = new SelectList(_userFactory.GetAllUsers(), "Id", "UserName");
            return View();
        }

        // POST: Admin/UsedActions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserId,ActionId,PartnerId,ActionValue")] UsedActionModel usedAction)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    usedAction = _factory.Create(usedAction);
                    return RedirectToAction(nameof(Index));
                }
                catch (InvalidOperationException e)
                {
                    // log error here

                    var allowedErrors = new List<string>()
                    {
                        ServicesConstants.CreateUsedAction_NotAllowedReasonMessage_UserDoesNotExist,
                        ServicesConstants.CreateUsedAction_NotAllowedReasonMessage_PartnerDoesNotExist,
                        ServicesConstants.CreateUsedAction_NotAllowedReasonMessage_MapAlreadyExists,
                        ServicesConstants.CreateUsedAction_NotAllowedReasonMessage_ActionDoesNotExist,
                    };

                    if (allowedErrors.Contains(e.Message ?? ""))
                    {
                        return View("CustomError", new CustomErrorViewModel()
                        {
                            HeaderMessage = "Not allowed",
                            Message = e.Message,
                            ReturnUrls = new Dictionary<string, string>()
                         {
                             { "Back to Create Used Action Record page", Url.Action("Create", "UsedActions", new { area = "Admin" }) },
                             { "Go to Used Action Record List", Url.Action("Index", "UsedActions", new { area = "Admin" }) },
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
            else
            {
                ViewData["ActionId"] = new SelectList(_actionFactory.GetAll(), "Id", "Name", usedAction.ActionId);
                ViewData["PartnerId"] = new SelectList(_partnerFactory.GetAll(), "Id", "Name", usedAction.PartnerId);
                ViewData["UserId"] = new SelectList(_userFactory.GetAllUsers(), "Id", "UserName", usedAction.UserId);
                return View(usedAction);
            }
        }

        // GET: Admin/UsedActions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usedAction = _factory.Get(id);
            if (usedAction == null)
            {
                return NotFound();
            }
            ViewData["ActionId"] = new SelectList(_actionFactory.GetAll(), "Id", "Name", usedAction.ActionId);
            ViewData["PartnerId"] = new SelectList(_partnerFactory.GetAll(), "Id", "Name", usedAction.PartnerId);
            ViewData["UserId"] = new SelectList(_userFactory.GetAllUsers(), "Id", "UserName", usedAction.UserId);
            return View(usedAction);
        }

        // POST: Admin/UsedActions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserId,ActionId,PartnerId,ActionValue")] UsedActionModel usedAction)
        {
            if (id != usedAction.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _factory.Update(usedAction);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (_factory.Get(usedAction.Id) != null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                catch (InvalidOperationException e)
                {
                    // log error here

                    var allowedErrors = new List<string>()
                    {
                        ServicesConstants.CreateUsedAction_NotAllowedReasonMessage_UserDoesNotExist,
                        ServicesConstants.CreateUsedAction_NotAllowedReasonMessage_PartnerDoesNotExist,
                        ServicesConstants.CreateUsedAction_NotAllowedReasonMessage_MapAlreadyExists,
                        ServicesConstants.CreateUsedAction_NotAllowedReasonMessage_ActionDoesNotExist,
                    };

                    if (allowedErrors.Contains(e.Message ?? ""))
                    {
                        return View("CustomError", new CustomErrorViewModel()
                        {
                            HeaderMessage = "Not allowed",
                            Message = e.Message,
                            ReturnUrls = new Dictionary<string, string>()
                            {
                                { "Back to Edit Used Action Record page", Url.Action("Edit", "UsedActions", new { area = "Admin", id = id }) },
                                { "Go to Used Action Record List", Url.Action("Index", "UsedActions", new { area = "Admin" }) },
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
                return RedirectToAction(nameof(Index));
            }
            else
            {
                ViewData["ActionId"] = new SelectList(_actionFactory.GetAll(), "Id", "Name", usedAction.ActionId);
                ViewData["PartnerId"] = new SelectList(_partnerFactory.GetAll(), "Id", "Name", usedAction.PartnerId);
                ViewData["UserId"] = new SelectList(_userFactory.GetAllUsers(), "Id", "UserName", usedAction.UserId);
                return View(usedAction);
            }
        }

        // GET: Admin/UsedActions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usedAction = _factory.Get(id);
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
            _factory.DeleteMap(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
