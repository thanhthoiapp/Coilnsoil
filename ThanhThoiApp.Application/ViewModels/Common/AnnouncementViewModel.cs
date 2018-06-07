﻿using System;
using System.Collections.Generic;
using System.Text;
using ThanhThoiApp.Data.Enums;

namespace ThanhThoiApp.Application.ViewModels.Common
{
    public class AnnouncementViewModel
    {
        public string Id { get; set; }

        public string Title { set; get; }

        public string Content { set; get; }


        public DateTime DateCreated { set; get; }

        public DateTime DateModified { set; get; }

        public Status Status { set; get; }

        public Guid UserId { set; get; }

    }
}
