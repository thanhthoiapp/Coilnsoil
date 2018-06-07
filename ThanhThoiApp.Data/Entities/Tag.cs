using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using ThanhThoiApp.Infrastructure.SharedKernel;

namespace ThanhThoiApp.Data.Entities
{
    public class Tag : DomainEntity<string>
    {
        //public Tag(string id, string name, string type)
        //{
        //    Id = id;
        //    Name = name;
        //    Type = type;
        //}

        //public Tag(string id, string name, string type)
        //{
        //    Id = id;
        //    Name = name;
        //    Type = type;
        //}

        //public Tag(int id, string name, string type)
        //{
        //    Id = id;
        //    Name = name;
        //    Type = type;
        //}

        [MaxLength(50)]
        [Required]
        public string Name { get; set; }

        [MaxLength(50)]
        [Required]
        public string Type { get; set; }
    }
}
