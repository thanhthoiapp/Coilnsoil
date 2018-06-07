using ThanhThoiApp.Data.Interfaces;
using ThanhThoiApp.Infrastructure.SharedKernel;
using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ThanhThoiApp.Data.Enums;

namespace ThanhThoiApp.Data.Entities
{
    [Table("SystemConfigs")]
    public class SystemConfig : DomainEntity<string>, ISwitchable
    {
        public SystemConfig() { }
        public SystemConfig(string id, string name, Status status, string value1, int? value2, bool? value3, DateTime? value4, decimal? value5)
        {
            Id = id;
            Name = name;
            Status = status;
            Value1 = value1;
            Value2 = value2;
            Value3 = value3;
            Value4 = value4;
            Value5 = value5;
        }

        [Required]
        [StringLength(128)]
        public string Name { get; set; }

        public string Value1 { get; set; }
        public int? Value2 { get; set; }

        public bool? Value3 { get; set; }

        public DateTime? Value4 { get; set; }

        public decimal? Value5 { get; set; }
        public Status Status { get; set; }
    }
}
