﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using ThanhThoiApp.Infrastructure.SharedKernel;

namespace ThanhThoiApp.Data.Entities
{
    [Table("Slides")]
    public class Slide : DomainEntity<int>
    {
        public Slide()
        {
        }

        public Slide(string name, string description, string image, string url, int? displayOrder, bool status, string content, string groupAlias)
        {
            Name = name;
            Description = description;
            Image = image;
            Url = url;
            DisplayOrder = displayOrder;
            Status = status;
            Content = content;
            GroupAlias = groupAlias;
        }

        //public Slide(int id, string name, string description, string image, string url, int? displayOrder, bool status, string content, string groupAlias)
        //{
        //    Id = id;
        //    Name = name;
        //    Description = description;
        //    Image = image;
        //    Url = url;
        //    DisplayOrder = displayOrder;
        //    Status = status;
        //    Content = content;
        //    GroupAlias = groupAlias;
        //}

        [StringLength(250)]
        [Required]
        public string Name { set; get; }

        [StringLength(250)]
        public string Description { set; get; }

        [StringLength(250)]
        [Required]
        public string Image { set; get; }

        [StringLength(250)]
        public string Url { set; get; }

        public int? DisplayOrder { set; get; }

        public bool Status { set; get; }

        public string Content { set; get; }

        [StringLength(25)]
        [Required]
        public string GroupAlias { get; set; }
    }
}
