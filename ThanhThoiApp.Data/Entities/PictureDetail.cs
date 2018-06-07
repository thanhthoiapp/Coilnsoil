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
    [Table("PictureDetails")]
    public class PictureDetail : DomainEntity<int>
    {
        public int PictureId { get; set; }

        [ForeignKey("PictureId")]
        public virtual Picture Picture { get; set; }

        [StringLength(255)]
        [Required]
        public string Name { get; set; }

        public bool? IsAvatar { get; set; }
        public bool? HomeFlag { get; set; }
    }
}
