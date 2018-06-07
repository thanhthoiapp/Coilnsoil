using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ThanhThoiApp.Data.Enums;
using ThanhThoiApp.Data.Interfaces;
using ThanhThoiApp.Infrastructure.SharedKernel;

namespace ThanhThoiApp.Data.Entities
{
    [Table("Blogs")]
    public class Blog : DomainEntity<int>, ISwitchable, IDateTracking, IHasSeoMetaData
    {
        public Blog() {
            BlogTags = new List<BlogTag>();
        }
        //public Blog(string name,string thumbnailImage,
        //   string description, string content, bool? homeFlag, bool? hotFlag,
        //   string tags, Status status, string seoPageTitle,
        //   string seoAlias, string seoMetaKeyword,
        //   string seoMetaDescription)
        //{
        //    Name = name;
        //    Image = thumbnailImage;
        //    Description = description;
        //    Content = content;
        //    HomeFlag = homeFlag;
        //    HotFlag = hotFlag;
        //    Tags = tags;
        //    Status = status;
        //    SeoPageTitle = seoPageTitle;
        //    SeoAlias = seoAlias;
        //    SeoKeywords = seoMetaKeyword;
        //    SeoDescription = seoMetaDescription;
        //}

        public Blog(int id, int categoryId, string name,string thumbnailImage,
             string description, bool? homeFlag1, string content, bool? homeFlag, bool? hotFlag,
             string tags, Status status, string seoPageTitle,
             string seoAlias, string seoMetaKeyword,
             string seoMetaDescription, Guid? postById)
        {
            Id = id;
            CategoryId = categoryId;
            Name = name;
            Image = thumbnailImage;
            Description = description;
            Content = content;
            HomeFlag = homeFlag;
            HotFlag = hotFlag;
            Tags = tags;
            Status = status;
            SeoPageTitle = seoPageTitle;
            SeoAlias = seoAlias;
            SeoKeywords = seoMetaKeyword;
            SeoDescription = seoMetaDescription;
            PostById = postById;
        }

        public Blog(string name, int categoryId, string content, string description, bool? homeFlag, bool? hotFlag, string image, string seoAlias, string seoDescription, string seoKeywords, string seoPageTitle, Status status, string tags, int? viewCount, Guid? postById)
        {
            Name = name;
            CategoryId = categoryId;
            Content = content;
            Description = description;
            HomeFlag = homeFlag;
            HotFlag = hotFlag;
            Image = image;
            SeoAlias = seoAlias;
            SeoDescription = seoDescription;
            SeoKeywords = seoKeywords;
            SeoPageTitle = seoPageTitle;
            Tags = tags;
            ViewCount = viewCount;
            Status = status;
            BlogTags = new List<BlogTag>();
            PostById = postById;
        }

        [Required]
        [MaxLength(256)]
        public string Name { set; get; }

        [Required]
        public int CategoryId { get; set; }

        [MaxLength(256)]
        public string Image { set; get; }

        [MaxLength(500)]
        public string Description { set; get; }

        public string Content { set; get; }

        public bool? HomeFlag { set; get; }
        public bool? HotFlag { set; get; }
        public int? ViewCount { set; get; }

        public string Tags { get; set; }

        [ForeignKey("CategoryId")]
        public virtual Category Category { set; get; }
        public virtual ICollection<BlogTag> BlogTags { set; get; }
        public DateTime DateCreated { set; get; }
        public DateTime DateModified { set; get; }
        public Status Status { set; get; }

        [MaxLength(256)]
        public string SeoPageTitle { set; get; }

        [MaxLength(256)]
        public string SeoAlias { set; get; }

        [MaxLength(256)]
        public string SeoKeywords { set; get; }

        [MaxLength(256)]
        public string SeoDescription { set; get; }

        public Guid? PostById { set; get; }

        [ForeignKey("PostById")]
        public virtual AppUser User { set; get; }
    }
}