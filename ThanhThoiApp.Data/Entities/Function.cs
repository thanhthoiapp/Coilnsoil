using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using ThanhThoiApp.Data.Enums;
using ThanhThoiApp.Data.Interfaces;
using ThanhThoiApp.Infrastructure.SharedKernel;

namespace ThanhThoiApp.Data.Entities
{
    [Table("Functions")]
    public class Function : DomainEntity<string>
    {
        public Function()
        {

        }
        //public Function(string name,string url,string parentId,string iconCss,int sortOrder)
        //{
        //    this.Name = name;
        //    this.URL = url;
        //    this.ParentId = parentId;
        //    this.IconCss = iconCss;
        //    this.SortOrder = sortOrder;
        //    this.Status = Status.Active;
        //}

        public Function(string name, string iconCss, string parentId, int sortOrder, Status status, string uRL)
        {
            Name = name;
            IconCss = iconCss;
            ParentId = parentId;
            SortOrder = sortOrder;
            this.Status = Status.Active;
            URL = uRL;
        }

        //public Function(string id, string name, string iconCss, string parentId, int sortOrder, Status status, string uRL)
        //{
        //    Id = id;
        //    Name = name;
        //    IconCss = iconCss;
        //    ParentId = parentId;
        //    SortOrder = sortOrder;
        //    URL = uRL;
        //}

        [Required]
        [StringLength(128)]
        public string Name { set; get; }

        [Required]
        [StringLength(250)]
        public string URL { set; get; }


        [StringLength(128)]
        public string ParentId { set; get; }

        public string IconCss { get; set; }
        public int SortOrder { set; get; }
        public Status Status { set; get; }
    }
}
