using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using ThanhThoiApp.Data.Enums;

namespace ThanhThoiApp.Application.ViewModels.Common
{
    public class AdvertistmentPositionViewModel
    {
        public string Id { set; get; }

        public string Name { set; get; }

        public string PageId { get; set; }
       
    }
}
