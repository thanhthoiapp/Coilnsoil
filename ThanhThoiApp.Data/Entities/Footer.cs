using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using ThanhThoiApp.Infrastructure.SharedKernel;

namespace ThanhThoiApp.Data.Entities
{
    [Table("Footers")]
    public class Footer : DomainEntity<string>
    {
        public Footer()
        {

        }
        public Footer(string id, string content)
        {
            Id = id;
            Content = content;
        }

        //public Footer(string content)
        //{
        //    Content = content;
        //}

        [Required]
        public string Content { set; get; }
    }
}
