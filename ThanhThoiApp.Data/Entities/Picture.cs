using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using ThanhThoiApp.Data.Enums;
using ThanhThoiApp.Data.Interfaces;
using ThanhThoiApp.Infrastructure.SharedKernel;

namespace ThanhThoiApp.Data.Entities
{
    [Table("Pictures")]
    public class Picture : DomainEntity<int>, ISwitchable, IDateTracking, IHasSeoMetaData
    {
        public Picture()
        {
            
        }

        public Picture(string name, int categoryId, string thumbnailImage,
            string description, string content, bool? homeFlag, 
            Status status, string seoPageTitle,
            string seoAlias, string seoMetaKeyword,
            string seoMetaDescription)
        {
            Name = name;
            CategoryId = categoryId;
            Image = thumbnailImage;
            Description = description;
            Content = content;
            HomeFlag = homeFlag;
            Status = status;
            SeoPageTitle = seoPageTitle;
            SeoAlias = seoAlias;
            SeoKeywords = seoMetaKeyword;
            SeoDescription = seoMetaDescription;
        }
        [StringLength(255)]
        [Required]
        public string Name { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [StringLength(255)]
        public string Image { get; set; }


        [StringLength(255)]
        public string Description { get; set; }

        public string Content { get; set; }

        public bool? HomeFlag { get; set; }

        public int? ViewCount { get; set; }


        [ForeignKey("CategoryId")]
        public virtual Category Category { set; get; }


        public string SeoPageTitle { set; get; }

        [Column(TypeName = "varchar(255)")]
        [StringLength(255)]
        public string SeoAlias { set; get; }

        [StringLength(255)]
        public string SeoKeywords { set; get; }

        [StringLength(255)]
        public string SeoDescription { set; get; }

        public DateTime DateCreated { set; get; }
        public DateTime DateModified { set; get; }

        public Status Status { set; get; }
    }
}
