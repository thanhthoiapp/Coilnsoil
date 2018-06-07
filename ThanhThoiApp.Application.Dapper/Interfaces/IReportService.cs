using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ThanhThoiApp.Application.Dapper.ViewModels;

namespace ThanhThoiApp.Application.Dapper.Interfaces
{
    public interface IReportService
    {
        Task<IEnumerable<RevenueReportViewModel>> GetReportAsync(string fromDate, string toDate);
    }
}
