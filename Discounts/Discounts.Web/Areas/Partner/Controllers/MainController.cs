﻿using Discounts.Web.Areas.Partner.Models;
using Discounts.Web.Factories;
using Discounts.Web.Helpers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Discounts.Web.Areas.Partner.Controllers
{
    public class MainController : PartnerBaseController
    {
        #region dependencies and constructor
        private readonly UserFactory _userFactory;
        private readonly PartnerFactory _partnerFactory;
        private readonly PartnerActionMapFactory _partnerActionMapFactory;

        public MainController(UserFactory userFactory, PartnerFactory partnerFactory, PartnerActionMapFactory partnerActionMapFactory)
        {
            _userFactory = userFactory;
            _partnerFactory = partnerFactory;
            _partnerActionMapFactory = partnerActionMapFactory;
        }
        #endregion

        #region helpers
        private bool IsUserAllowedForPartner(int partnerId)
        {
            if (User.IsInRole(WebConstants.AdminRole))
                return true;
            if (!User.IsInRole(WebConstants.PartnerRole))
                return false;

            var claim = User.FindFirst(WebConstants.PartnerClaimType);

            int claimPartnerId;
            if (claim != null && int.TryParse(claim.Value, out claimPartnerId))
                return partnerId == claimPartnerId;
            else
                return false;
        }
        #endregion

        public IActionResult Index()
        {
            try
            {
                if (User.IsInRole(WebConstants.AdminRole))
                {
                    var user = _userFactory.GetUser(User.Identity.Name);
                    if (user.PartnerId != null)
                        return RedirectToAction(nameof(Home), new { id = user.PartnerId.Value });
                    else
                        return RedirectToAction("Index", "Partners", new { area = "Admin" });
                }
                else if (User.IsInRole(WebConstants.PartnerRole))
                {
                    var user = _userFactory.GetUser(User.Identity.Name);

                    if (user == null)
                        throw new ApplicationException("No record of such user");

                    if (user.PartnerId == null)
                        throw new ApplicationException("The current user has the permissions of a Partner, but isn't tied to any");

                    return RedirectToAction(nameof(Home), new { id = user.PartnerId.Value });
                }
                else
                {
                    return Unauthorized();
                }
            }
            catch (Exception e)
            {
                return View("CustomError", new CustomErrorViewModel()
                {
                    HeaderMessage = "Error",
                    Message = e.Message,
                    ReturnUrls = null
                });
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">Partner Id</param>
        /// <returns></returns>
        public IActionResult Home(int id)
        {
            if (!IsUserAllowedForPartner(id))
                return Unauthorized();

            if (User.IsInRole(WebConstants.AdminRole))
            {
                var user = _userFactory.GetUser(User.Identity.Name);
                if (user.PartnerId != id)
                {
                    user.PartnerId = id;
                    _userFactory.UpdateUser(user);
                }    
            }

            var model = _partnerFactory.GetPartnerForPartnerHomeView(id);

            return View(model);
        }

        public IActionResult PartnerActions(int partnerId)
        {
            if (!IsUserAllowedForPartner(partnerId))
                return Unauthorized();

            var user = _userFactory.GetUser(User.Identity.Name);
            if (User.IsInRole(WebConstants.AdminRole))
            {
                if (user.PartnerId != partnerId)
                {
                    user.PartnerId = partnerId;
                    _userFactory.UpdateUser(user);
                }
            }

            var partner = _partnerFactory.GetPartner(partnerId);

            ViewData["PartnerId"] = partnerId;
            ViewData["PartnerName"] = partner.Name;

            var model = _partnerActionMapFactory.GetActionsForPartnerActionsView(partnerId, user.Id);

            return View(model);
        }

        public IActionResult PartnerActionDetails(int partnerId, int actionId)
        {
            if (!IsUserAllowedForPartner(partnerId))
                return Unauthorized();

            var user = _userFactory.GetUser(User.Identity.Name);
            if (User.IsInRole(WebConstants.AdminRole))
            {
                if (user.PartnerId != partnerId)
                {
                    user.PartnerId = partnerId;
                    _userFactory.UpdateUser(user);
                }
            }

            var partner = _partnerFactory.GetPartner(partnerId);

            ViewData["PartnerId"] = partnerId;
            ViewData["PartnerName"] = partner.Name;
            ViewData["ActionId"] = actionId;

            var model = _partnerActionMapFactory.GetActionForPartnerActionDetailsView(partnerId, user.Id, actionId);

            return View(model);
        }
    }
}
