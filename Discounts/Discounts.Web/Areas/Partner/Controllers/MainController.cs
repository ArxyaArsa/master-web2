using Discounts.Web.Areas.Partner.Models;
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
        private readonly UsedActionFactory _usedActionFactory;

        public MainController(
            UserFactory userFactory, 
            PartnerFactory partnerFactory, 
            PartnerActionMapFactory partnerActionMapFactory,
            UsedActionFactory usedActionFactory
        )
        {
            _userFactory = userFactory;
            _partnerFactory = partnerFactory;
            _partnerActionMapFactory = partnerActionMapFactory;
            _usedActionFactory = usedActionFactory;
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

        public IActionResult PartnerUsedActions(int partnerId, int actionId)
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

            var model = _usedActionFactory.GetUsedActionsForPartnerUsedActionsView(partnerId, user.Id, actionId);

            return View(model);
        }

        public IActionResult LogActionUse()
        {
            int partnerId = -1;
            var user = _userFactory.GetUser(User.Identity.Name);
            if (User.IsInRole(WebConstants.PartnerRole))
            {
                if (user.PartnerId == null)
                {
                    throw new Exception("User with Partner role must have a partner assigned.");
                }

                partnerId = user.PartnerId.Value;
            }
            else
            {
                throw new Exception("User must be in Partner role to Access this page");
            }

            ViewBag.Actions = _partnerActionMapFactory.GetActionsForPartnerActionsView(partnerId, user.Id);
            ViewBag.Users = _userFactory.GetAllUsersInRole(WebConstants.UserRole);

            LogActionUseModel model = TempData.Get<LogActionUseModel>("LogActionUseModel");
            ViewBag.Errors = TempData["Errors"] as string;

            return View(model);
        }

        public IActionResult SubmitActionUse([Bind] LogActionUseModel model)
        {
            int partnerId = -1;
            var user = _userFactory.GetUser(User.Identity.Name);
            if (User.IsInRole(WebConstants.PartnerRole))
            {
                if (user.PartnerId == null)
                {
                    throw new Exception("User with Partner role must have a partner assigned.");
                }

                partnerId = user.PartnerId.Value;
            }
            else
            {
                throw new Exception("User must be in Partner role to Access this page");
            }

            try
            {
                int userId = -1;
                int actionId = -1;
                if (model.UserCode != null)
                {
                    var nums = model.UserCode.Split("-");
                    if (nums.Count() == 4)
                    {
                        int.TryParse(nums[2], out userId);
                        int.TryParse(nums[3], out actionId);
                    }
                }

                if (userId == -1 || actionId == -1)
                {
                    userId = model.UserId ?? -1;
                    actionId = model.ActionId ?? -1;
                }

                if (userId == -1 || actionId == -1)
                {
                    throw new Exception("Invalid code (Step 1a) or User and Action not chosen (Step 1b)");
                }

                if (model.OriginalValue <= 0)
                    throw new Exception("Bad Purchase Amount value (Step 2)");

                var passedUser = _userFactory.GetUser(userId);
                if (passedUser == null)
                    throw new Exception("No such User");
                if (!passedUser.Roles.Contains(WebConstants.UserRole))
                    throw new Exception("The User you are trying to bind the Discount to isn't a regular User");

                var action = _partnerActionMapFactory.GetActionsForPartnerActionsView(partnerId, user.Id).Where(x => x.Id == actionId).FirstOrDefault();
                if (action == null)
                    throw new Exception("No such Discount Action");

                if (action.CashValue == null && action.PercentValue == null)
                    throw new Exception("The Discount Action you are trying to use is badly defined. It may not be used until fixed. Both Percent and Cash value have no value set");

                string percentValueString = "none";
                string cashValueString = "none";

                decimal value;
                if (action.PercentValue != null)
                {
                    value = action.PercentValue.Value * model.OriginalValue;
                    percentValueString = "%" + action.PercentValue.Value.ToString();

                    // if both CashValue and PercentValue are defined in the DiscountAction
                    // the CashValue acts as the upper limit
                    if (action.CashValue != null)
                    {
                        cashValueString = "$" + action.CashValue.Value.ToString();
                        if (action.CashValue.Value < value)
                            value = action.CashValue.Value;
                    }
                }
                else
                {
                    value = action.CashValue.Value;
                    cashValueString = "$" + action.CashValue.Value.ToString();
                }

                var res = _usedActionFactory.Create(new Services.Models.UsedActionModel()
                {
                    ActionId = actionId,
                    ActionValue = value,
                    DateCreated = DateTime.UtcNow,
                    OriginalValue = model.OriginalValue,
                    PartnerId = partnerId,
                    UserId = userId
                });

                TempData["Message"] = $"Successfully added action [{res.ActionName}] to user [{res.UserName}]. Action value = [${res.ActionValue}]. Action value based on the Purchase Amount = [${model.OriginalValue}] and actions Percent Value = [{percentValueString}] and Cash Value = [{cashValueString}].";

                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                TempData.Put("LogActionUseModel", model);
                TempData["Errors"] = e.Message;

                return RedirectToAction(nameof(LogActionUse));
            }
        }
    }
}
