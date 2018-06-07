using System;
using System.Collections.Generic;
using System.Text;
using ThanhThoiApp.Application.ViewModels.Common;

namespace ThanhThoiApp.Application.Interfaces
{
    public interface ICommonService
    {
        FooterViewModel GetFooter();
        FooterViewModel GetIntro();
        List<SlideViewModel> GetSlides(string groupAlias);
        SystemConfigViewModel GetSystemConfig(string code);
    }
}
