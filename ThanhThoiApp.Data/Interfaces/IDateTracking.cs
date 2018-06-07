using System;
using System.Collections.Generic;
using System.Text;

namespace ThanhThoiApp.Data.Interfaces
{
    public interface IDateTracking
    {
        DateTime DateCreated { set; get; }

        DateTime DateModified { set; get; }
    }
}
