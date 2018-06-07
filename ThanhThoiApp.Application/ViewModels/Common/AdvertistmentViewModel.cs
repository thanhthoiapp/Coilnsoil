using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using ThanhThoiApp.Data.Enums;

namespace ThanhThoiApp.Application.ViewModels.Common
{
    public class AdvertistmentViewModel
    {
        public int Id { set; get; }

        public string Name { set; get; }

        public string PositionId { get; set; }


        public string Image { set; get; }
        public string Url { set; get; }
        public int SortOrder { set; get; }
        public string Description { set; get; }

        public AdvertistmentPositionViewModel Position { set; get; }
        public DateTime DateCreated { set; get; }
        public DateTime DateModified { set; get; }
        public Status Status { set; get; }

       
    }
}
