﻿using AutoMapper;
using Discounts.DataLayer;
using Discounts.DataLayer.Models;
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
    public class PartnerService : IPartnerService
    {
        #region dependencies & constructor
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public PartnerService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        #endregion

        public IEnumerable<Partner> GetPartners()
        {
            return _context.Partner.AsEnumerable();
        }
    }
}
