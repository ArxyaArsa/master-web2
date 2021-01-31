using AutoMapper;
using Discounts.DataLayer;
using Discounts.DataLayer.Models;
using Discounts.Services.Helpers;
using Discounts.Services.Interfaces;
using Discounts.Services.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discounts.Services.Services
{
    public class UserService : IUserService
    {
        #region dependencies & constructor
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public UserService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        #endregion

        public IEnumerable<DiscountsUser> GetUsers()
        {
            return _context.Users.AsEnumerable();
        }
        
        public void UpdateUser(DiscountsUser user, IList<string> roles)
        {
            using (var tran = _context.Database.BeginTransaction())
            {
                var roleObjs = _context.Roles.Where(x => roles.Contains(x.Name)).ToList();
                                
                var currentRoleMaps = _context.Users.Update(user).Entity.UserRoleMaps;

                var toRemove = currentRoleMaps.Where(x => !roles.Contains(x.Role.Name));
                if (toRemove.Count() > 0)
                    _context.UserRoles.RemoveRange(toRemove);

                var toAdd = roleObjs.Where(x => !currentRoleMaps.Select(y => y.RoleId).Contains(x.Id)).Select(x => new DiscountsUserRole()
                {
                    UserId = user.Id,
                    RoleId = x.Id
                });
                if (toAdd.Count() > 0)
                    _context.UserRoles.AddRange(toAdd);

                _context.SaveChanges();
                tran.Commit();
            }
        }

        public DiscountsUser CreateUser(DiscountsUser user, IList<string> roles)
        {
            using (var tran = _context.Database.BeginTransaction())
            {
                var roleObjs = _context.Roles.Where(x => roles.Contains(x.Name)).ToList();

                var newUser = _context.Users.Add(user).Entity;
                _context.SaveChanges();

                var toAdd = roleObjs.Select(x => new DiscountsUserRole()
                {
                    UserId = user.Id,
                    RoleId = x.Id
                });
                if (toAdd.Count() > 0)
                    _context.UserRoles.AddRange(toAdd);

                _context.SaveChanges();
                tran.Commit();

                return newUser;
            }
        }

        public void DeleteUser(int id)
        {
            var dUser = _context.Users.Find(id);
            if (dUser == null)
                return;

            if (_context.UsedAction.Where(x => x.UserId == id).Any())
                throw new InvalidOperationException(ServicesConstants.DeleteNotAllowedReasonMessage_UserHasUsedActions);

            _context.Users.Remove(dUser);
            _context.SaveChanges();
        }

        public IEnumerable<DiscountsRole> GetRoles()
        {
            return _context.Roles.AsEnumerable();
        }
    }
}
