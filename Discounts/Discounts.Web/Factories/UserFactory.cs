using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Discounts.DataLayer.Models;
using Discounts.Services.Interfaces;
using Discounts.Services.Models;

namespace Discounts.Web.Factories
{
    public class UserFactory
    {
        private IUserService _userService;
        private IMapper _mapper;

        public UserFactory(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        public IEnumerable<UserModel> GetAllUsers()
        {
            return _userService.GetUsers().Select(x => _mapper.Map<DiscountsUser, UserModel>(x)).AsEnumerable();
        }

        public UserModel GetUser(int? id)
        {
            return _mapper.Map<DiscountsUser, UserModel>(_userService.GetUsers().FirstOrDefault(x => x.Id == id));
        }

        public void UpdateUser(UserModel user)
        {
            var dUser = _userService.GetUsers().FirstOrDefault(x => x.Id == user.Id);

            dUser.ConcurrencyStamp = user.ConcurrencyStamp;
            dUser.LockoutEnabled = user.LockoutEnabled;
            dUser.LockoutEnd = user.LockoutEnd;
            dUser.PartnerId = user.PartnerId;
            dUser.Email = user.Email;
            dUser.PhoneNumber = user.PhoneNumber;

            _userService.UpdateUser(dUser, user.Roles);
        }

        public void CreateUser(UserModel user)
        {
            var dUser = new DiscountsUser();

            dUser.ConcurrencyStamp = user.ConcurrencyStamp;
            dUser.LockoutEnabled = user.LockoutEnabled;
            dUser.LockoutEnd = user.LockoutEnd;
            dUser.PartnerId = user.PartnerId;
            dUser.UserName = user.UserName;
            dUser.NormalizedUserName = user.UserName.ToUpper();
            dUser.Email = user.Email;
            dUser.NormalizedEmail = user.Email.ToUpper();
            dUser.PhoneNumber = user.PhoneNumber;

            _userService.CreateUser(dUser, user.Roles);
        }

        public IEnumerable<RoleModel> GetAllRoles()
        {
            return _userService.GetRoles().Select(x => _mapper.Map<DiscountsRole, RoleModel>(x)).AsEnumerable();
        }

        public bool UserExists(int id)
        {
            return _userService.GetUsers().Any(e => e.Id == id);
        }
    }
}
