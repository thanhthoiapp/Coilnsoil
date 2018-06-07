using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using ThanhThoiApp.Infrastructure.SharedKernel;

namespace ThanhThoiApp.Data.Entities
{
    [Table("Socials")]
    public class Social : DomainEntity<int>
    {
        public Social()
        {

        }      
        [StringLength(100)]
        public string Name { set; get; }

        [StringLength(250)]
        public string Link { set; get; }

        [StringLength(250)]
        public string Icon { set; get; }
    }
}
