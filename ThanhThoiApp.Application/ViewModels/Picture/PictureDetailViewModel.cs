using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ThanhThoiApp.Application.ViewModels.Picture
{
    public class PictureDetailViewModel
    {
        public int Id { get; set; }
        public int PictureId { get; set; }

        public PictureViewModel Picture { get; set; }

        [StringLength(255)]
        [Required]
        public string Name { get; set; }

        public bool? IsAvatar { get; set; }
        public bool? HomeFlag { get; set; }
    }
}
