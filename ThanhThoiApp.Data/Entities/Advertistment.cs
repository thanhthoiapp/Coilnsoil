using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using ThanhThoiApp.Data.Enums;
using ThanhThoiApp.Data.Interfaces;
using ThanhThoiApp.Infrastructure.SharedKernel;

namespace ThanhThoiApp.Data.Entities
{
    [Table("Advertistments")]
    public class Advertistment : DomainEntity<int>, ISwitchable, ISortable
    {
        public Advertistment() { }
        public Advertistment(int id, string name, string description, string image, string url, int sortOrder, string positionId, Status status)
        {
            Id = id;
            Name = name;
            Description = description;
            Image = image;
            Url = url;
            SortOrder = sortOrder;
            PositionId = positionId;
            Status = status;
            //AdvertistmentPositions = new List<AdvertistmentPosition>();
        }

        [StringLength(250)]
        public string Name { get; set; }

        [StringLength(250)]
        public string Description { get; set; }

        [StringLength(250)]
        public string Image { get; set; }

        [StringLength(250)]
        public string Url { get; set; }

        [StringLength(20)]
        public string PositionId { get; set; }

        public Status Status { set; get; }
        public DateTime DateCreated { set; get; }
        public DateTime DateModified { set; get; }
        public int SortOrder { set; get; }

        [ForeignKey("PositionId")]
        public virtual AdvertistmentPosition AdvertistmentPosition { get; set; }
        //public virtual ICollection<AdvertistmentPosition> AdvertistmentPositions { set; get; }
    }
}
