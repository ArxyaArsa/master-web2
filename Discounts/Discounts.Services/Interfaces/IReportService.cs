using Discounts.DataLayer.Models;
using Discounts.Services.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Discounts.Services.Interfaces
{
    public interface IReportService
    {
        IEnumerable<Report> GetReports();
        Report CreateReport(Report report);
    }
}
