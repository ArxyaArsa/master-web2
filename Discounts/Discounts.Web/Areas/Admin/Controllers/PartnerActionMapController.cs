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
    public class PartnerActionMapController : AdminBaseController
    {
        private readonly PartnerActionMapFactory _factory;
        private readonly PartnerFactory _partnerFactory;
        private readonly ActionFactory _actionFactory;

        public PartnerActionMapController(PartnerActionMapFactory factory, PartnerFactory partnerFactory, ActionFactory actionFactory)
        {
            _factory = factory;
            _partnerFactory = partnerFactory;
            _actionFactory = actionFactory;
        }

        // GET: Admin/PartnerActionMap
        public async Task<IActionResult> Index()
        {
            return View(_factory.GetAllMaps());
        }

        // GET: Admin/PartnerActionMap/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var partnerActionMap = _factory.GetMap(id);
            if (partnerActionMap == null)
            {
                return NotFound();
            }

            return View(partnerActionMap);
        }

        // GET: Admin/PartnerActionMap/Create
        public IActionResult Create()
        {
            ViewData["ActionId"] = new SelectList(_actionFactory.GetAll(), "Id", "Name");
            ViewData["PartnerId"] = new SelectList(_partnerFactory.GetAll(), "Id", "Name");
            return View();
        }

        // POST: Admin/PartnerActionMap/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PartnerId,ActionId,CreatedDate")] PartnerActionMapModel map)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _factory.CreateMap(map);
                    return RedirectToAction(nameof(Index));
                }
                catch (InvalidOperationException e)
                {
                    // log error here

                    var allowedErrors = new List<string>()
                    {
                        ServicesConstants.CreatePartnerActionMap_NotAllowedReasonMessage_ActionDoesNotExist,
                        ServicesConstants.CreatePartnerActionMap_NotAllowedReasonMessage_PartnerDoesNotExist,
                        ServicesConstants.CreatePartnerActionMap_NotAllowedReasonMessage_MapAlreadyExists,
                    };

                    if (allowedErrors.Contains(e.Message ?? ""))
                    {
                        return View("CustomError", new CustomErrorViewModel()
                        {
                            HeaderMessage = "Not allowed",
                            Message = e.Message,
                            ReturnUrls = new Dictionary<string, string>()
                         {
                             { "Back to Create Partner Action Map page", Url.Action("Create", "PartnerActionMap", new { area = "Admin" }) },
                             { "Go to Partner Action Maps List", Url.Action("Index", "PartnerActionMap", new { area = "Admin" }) },
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
            ViewData["ActionId"] = new SelectList(_actionFactory.GetAll(), "Id", "Name", map.ActionId);
            ViewData["PartnerId"] = new SelectList(_partnerFactory.GetAll(), "Id", "Name", map.PartnerId);
            return View(map);
        }

        // GET: Admin/PartnerActionMap/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var map = _factory.GetMap(id);
            if (map == null)
            {
                return NotFound();
            }
            ViewData["ActionId"] = new SelectList(_actionFactory.GetAll(), "Id", "Name", map.ActionId);
            ViewData["PartnerId"] = new SelectList(_partnerFactory.GetAll(), "Id", "Name", map.PartnerId);
            return View(map);
        }

        // POST: Admin/PartnerActionMap/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,PartnerId,ActionId,CreatedDate")] PartnerActionMapModel map)
        {
            if (id != map.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    map = _factory.UpdateMap(map);
                    return RedirectToAction(nameof(Index));
                }
                catch (InvalidOperationException e)
                {
                    // log error here

                    var allowedErrors = new List<string>()
                    {
                        ServicesConstants.UpdatePartnerActionMap_NotAllowedReasonMessage_ActionDoesNotExist,
                        ServicesConstants.UpdatePartnerActionMap_NotAllowedReasonMessage_PartnerDoesNotExist,
                        ServicesConstants.UpdatePartnerActionMap_NotAllowedReasonMessage_MapAlreadyExists,
                    };

                    if (allowedErrors.Contains(e.Message ?? ""))
                    {
                        return View("CustomError", new CustomErrorViewModel()
                        {
                            HeaderMessage = "Not allowed",
                            Message = e.Message,
                            ReturnUrls = new Dictionary<string, string>()
                             {
                                 { "Back to Edit Partner Action Map page", Url.Action("Edit", "PartnerActionMap", new { area = "Admin" }) },
                                 { "Go to Partner Action Maps List", Url.Action("Index", "PartnerActionMap", new { area = "Admin" }) },
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
            ViewData["ActionId"] = new SelectList(_actionFactory.GetAll(), "Id", "Name", map.ActionId);
            ViewData["PartnerId"] = new SelectList(_partnerFactory.GetAll(), "Id", "Name", map.PartnerId);
            return View(map);
        }

        // GET: Admin/PartnerActionMap/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var map = _factory.GetMap(id);
            if (map == null)
            {
                return NotFound();
            }

            return View(map);
        }

        // POST: Admin/PartnerActionMap/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            _factory.DeleteMap(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
