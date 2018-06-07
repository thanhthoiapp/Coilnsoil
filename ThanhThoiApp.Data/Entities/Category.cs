using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using ThanhThoiApp.Data.Enums;
using ThanhThoiApp.Data.Interfaces;
using ThanhThoiApp.Infrastructure.SharedKernel;

namespace ThanhThoiApp.Data.Entities
{
    [Table("Category")]
    public class Category : DomainEntity<int>,
        IHasSeoMetaData, ISwitchable, ISortable, IDateTracking
    {
        public Category()
        {
            Products = new List<Product>();
        }

        public Category(string name,string description, int? parentId,int? homeOrder,
            string image,bool? homeFlag,int sortOrder,Status status,string seoPageTitle,string seoAlias,
            string seoKeywords, string seoDescription, int type, string path)
        {
            Name = name;
            Description = description;
            ParentId = parentId;
            HomeOrder = homeOrder;
            Image = image;
            HomeFlag = homeFlag;
            SortOrder = sortOrder;
            Status = status;
            SeoPageTitle = seoPageTitle;
            SeoAlias = seoAlias;
            SeoKeywords = seoKeywords;
            SeoDescription = seoDescription;
            Type = type;
            Path = path;
        }
        public string Name { get; set; }

        public string Description { get; set; }

        public int? ParentId { get; set; }

        public int? HomeOrder { get; set; }

        public string Image { get; set; }

        public bool? HomeFlag { get; set; }

        public DateTime DateCreated { set; get; }
        public DateTime DateModified { set; get; }
        public int SortOrder { set; get; }
        public Status Status { set; get; }
        public string SeoPageTitle { set; get; }
        public string SeoAlias { set; get; }
        public string SeoKeywords { set; get; }
        public string SeoDescription { set; get; }

        public int Type { set; get; }
        public string Path { set; get; }
        public virtual ICollection<Product> Products { set; get; }
        public virtual ICollection<Blog> Blogs { set; get; }
    }
}