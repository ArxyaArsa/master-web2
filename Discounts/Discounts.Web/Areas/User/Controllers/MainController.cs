using Discounts.Web.Factories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Identity.Core;
using System.Security.Claims;

namespace Discounts.Web.Areas.User.Controllers
{
    public class MainController : UserBaseController
    {
        #region dependencies + constructor
        private readonly PartnerTypeFactory _partnerTypeFactory;
        private readonly PartnerFactory _partnerFactory;
        private readonly PartnerActionMapFactory _partnerActionMapFactory;

        public MainController(
            PartnerTypeFactory partnerTypeFactory, 
            PartnerFactory partnerFactory, 
            PartnerActionMapFactory partnerActionMapFactory
        ) {
            _partnerTypeFactory = partnerTypeFactory;
            _partnerFactory = partnerFactory;
            _partnerActionMapFactory = partnerActionMapFactory;
        }
        #endregion

        public IActionResult Index()
        {
            return RedirectToAction("ViewByCategory");
        }

        public IActionResult ViewByCategory()
        {
            var model = _partnerTypeFactory.GetCategoriesForViewByCategory();

            return View(model);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">Partner Type Id</param>
        /// <returns></returns>
        public IActionResult ViewByPartner(int id)
        {
            // add error handling
            var model = _partnerFactory.GetPartnersForViewByPartner(id);

            //ViewData["partner"] = _partnerTypeFactory.GetPartnerType(id).Name;
            //ViewData["CategoryId"] = id;

            return View(model);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">Partner Id</param>
        /// <returns></returns>
        public IActionResult ViewByAction(int id)
        {
            // add error handling

            int userId = int.Parse(User.FindFirst(x => x.Type == ClaimTypes.NameIdentifier).Value);

            var model = _partnerActionMapFactory.GetActionsForViewByAction(id, userId);

            //ViewData["Partner"] = _partnerFactory.GetPartner(id).Name;
            //ViewData["PartnerId"] = id;

            return View(model);
        }
    }
}
