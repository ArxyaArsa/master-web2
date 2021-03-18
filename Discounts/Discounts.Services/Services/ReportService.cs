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
    public class ReportService : IReportService
    {
        #region dependencies & constructor
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ReportService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        #endregion

        public IEnumerable<Report> GetReports()
        {
            return _context.Reports.AsEnumerable();
        }

        public Report CreateReport(Report report)
        {
            using (var tran = _context.Database.BeginTransaction())
            {
                var newUser = _context.Reports.Add(report).Entity;
                _context.SaveChanges();

                tran.Commit();

                return newUser;
            }
        }
    }
}
