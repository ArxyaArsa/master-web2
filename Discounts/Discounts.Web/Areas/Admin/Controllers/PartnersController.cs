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
    public class PartnersController : AdminBaseController
    {
        private readonly PartnerFactory _partnerFactory;
        private readonly PartnerTypeFactory _partnerTypeFactory;

        public PartnersController(PartnerFactory partnerFactory, PartnerTypeFactory partnerTypeFactory)
        {
            _partnerFactory = partnerFactory;
            _partnerTypeFactory = partnerTypeFactory;
        }

        // GET: Admin/Partners
        public async Task<IActionResult> Index()
        {
            var partners = _partnerFactory.GetAll();
            return View(partners);
        }

        // GET: Admin/Partners/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var partner = _partnerFactory.GetPartner(id);
            if (partner == null)
            {
                return NotFound();
            }

            return View(partner);
        }

        // GET: Admin/Partners/Create
        public IActionResult Create()
        {
            ViewData["PartnerTypeId"] = new SelectList(_partnerTypeFactory.GetAllPartnerTypes(), "Id", "Name");
            return View();
        }

        // POST: Admin/Partners/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PartnerTypeId,Name,StartDate,EndDate")] PartnerModel partner)
        {
            if (ModelState.IsValid)
            {
                partner = _partnerFactory.CreatePartner(partner);
                return RedirectToAction(nameof(Index));
            }
            ViewData["PartnerTypeId"] = new SelectList(_partnerTypeFactory.GetAllPartnerTypes(), "Id", "Name", partner.PartnerTypeId);
            return View(partner);
        }

        // GET: Admin/Partners/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var partner = _partnerFactory.GetPartner(id);
            if (partner == null)
            {
                return NotFound();
            }
            ViewData["PartnerTypeId"] = new SelectList(_partnerTypeFactory.GetAllPartnerTypes(), "Id", "Name", partner.PartnerTypeId);
            return View(partner);
        }

        // POST: Admin/Partners/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,PartnerTypeId,Name,StartDate,EndDate")] PartnerModel partner)
        {
            if (id != partner.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _partnerFactory.UpdatePartner(partner);
                return RedirectToAction(nameof(Index));
            }
            ViewData["PartnerTypeId"] = new SelectList(_partnerTypeFactory.GetAllPartnerTypes(), "Id", "Name", partner.PartnerTypeId);
            return View(partner);
        }

        // GET: Admin/Partners/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var model = _partnerFactory.GetPartner(id);

            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }

        // POST: Admin/Partners/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            try
            {
                _partnerFactory.DeletePartner(id);
                return RedirectToAction(nameof(Index));
            }
            catch (InvalidOperationException e)
            {
                // log error here

                var allowedErrors = new List<string>()
                {
                    ServicesConstants.DeletePartner_NotAllowedReasonMessage_PartnerHasActions,
                    ServicesConstants.DeletePartner_NotAllowedReasonMessage_PartnerHasUsedActions,
                    ServicesConstants.DeletePartner_NotAllowedReasonMessage_PartnerHasUsers
                };

                if (allowedErrors.Contains(e.Message ?? ""))
                {
                    return View("CustomError", new CustomErrorViewModel()
                    {
                        HeaderMessage = "Not allowed",
                        Message = e.Message,
                        ReturnUrls = new Dictionary<string, string>()
                         {
                             { "Back to Delete Partner page", Url.Action("Delete", "Partners", new { area = "Admin", id = id }) },
                             { "Go to this Partner details", Url.Action("Details", "Partners", new { area = "Admin", id = id }) },
                             { "Go to Partners", Url.Action("Index", "Partners", new { area = "Admin" }) },
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
