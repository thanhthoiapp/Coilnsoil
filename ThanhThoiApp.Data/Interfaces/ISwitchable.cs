using System;
using System.Collections.Generic;
using System.Text;
using ThanhThoiApp.Data.Enums;

namespace ThanhThoiApp.Data.Interfaces
{
    public interface ISwitchable
    {
        Status Status { set; get; }
    }
}
