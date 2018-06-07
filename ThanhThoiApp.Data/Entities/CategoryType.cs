using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using ThanhThoiApp.Infrastructure.SharedKernel;

namespace ThanhThoiApp.Data.Entities
{
    public class CategoryType

    {
        public CategoryType(int id, string type)
        {
            Id = id;
            Type = type;

        }
        [Required]
        public int Id { get; set; }

        [Required]
        public string Type { get; set; }

    }
}
