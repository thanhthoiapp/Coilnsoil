using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using ThanhThoiApp.Infrastructure.SharedKernel;

namespace ThanhThoiApp.Data.Entities
{
    public class BlogTag : DomainEntity<int>
    {
        public int BlogId { set; get; }

        [StringLength(50)]
        [Column(TypeName = "varchar")]
        public string TagId { set; get; }

        [ForeignKey("BlogId")]
        public virtual Blog Blog { set; get; }

        [ForeignKey("TagId")]
        public virtual Tag Tag { set; get; }
    }
}
