using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ThanhThoiApp.Application.Dapper.Interfaces;
using ThanhThoiApp.Application.Interfaces;
using ThanhThoiApp.Application.ViewModels.System;
using ThanhThoiApp.Extensions;

namespace ThanhThoiApp.Areas.Admin.Controllers
{
    public class ReportController : BaseController
    {
        private readonly IReportService _reportService;

        public ReportController(IReportService reportService)
        {
            _reportService = reportService;
        }

        public IActionResult Revenues()
        {
            var email = User.GetSpecificClaim("Email");
            return View();
        }

        public async Task<IActionResult> GetRevenue(string fromDate, string toDate)
        {
            return new OkObjectResult(await _reportService.GetReportAsync(fromDate, toDate));
        }
    }
}