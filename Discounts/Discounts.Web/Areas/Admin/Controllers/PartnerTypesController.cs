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
using Discounts.Web.Areas.Admin.Models;
using Discounts.Services.Helpers;

namespace Discounts.Web.Areas.Admin.Controllers
{
    public class PartnerTypesController : AdminBaseController
    {
        private readonly PartnerTypeFactory _partnerTypeFactory;

        public PartnerTypesController(PartnerTypeFactory partnerTypeFactory)
        {
            _partnerTypeFactory = partnerTypeFactory;
        }

        // GET: Admin/PartnerTypes
        public async Task<IActionResult> Index()
        {
            return View(_partnerTypeFactory.GetAllPartnerTypes());
        }

        // GET: Admin/PartnerTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var partnerType = _partnerTypeFactory.GetPartnerType(id);
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
        public async Task<IActionResult> Create([Bind("Id,Name")] PartnerTypeModel partnerType)
        {
            if (ModelState.IsValid)
            {
                partnerType = _partnerTypeFactory.CraetePartnerType(partnerType);
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

            var partnerType = _partnerTypeFactory.GetPartnerType(id);
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
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] PartnerTypeModel partnerType)
        {
            if (id != partnerType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                partnerType = _partnerTypeFactory.UpdatePartnerType(partnerType);
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

            var partnerType = _partnerTypeFactory.GetPartnerType(id);
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
            try
            {
                _partnerTypeFactory.DeletePartnerType(id);
                return RedirectToAction(nameof(Index));
            }
            catch (InvalidOperationException e)
            {
                // log error here

                var allowedErrors = new List<string>()
                {
                    ServicesConstants.DeletePartnerType_NotAllowedReasonMessage_PartnerTypeHasPartners,
                };

                if (allowedErrors.Contains(e.Message ?? ""))
                {
                    return View("CustomError", new CustomErrorViewModel()
                    {
                        HeaderMessage = "Not allowed",
                        Message = e.Message,
                        ReturnUrls = new Dictionary<string, string>()
                         {
                             { "Back to Delete Partner Type page", Url.Action("Delete", "PartnerTypes", new { area = "Admin", id = id }) },
                             { "Go to this Partner Type details", Url.Action("Details", "PartnerTypes", new { area = "Admin", id = id }) },
                             { "Go to Partner Types", Url.Action("Index", "PartnerTypes", new { area = "Admin" }) },
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
