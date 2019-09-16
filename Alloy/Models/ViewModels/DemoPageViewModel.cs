using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Alloy.Models.Pages;
using EPiServer.Core;

namespace Alloy.Models.ViewModels
{
    public class DemoPageViewModel
    {
        public DemoPage CurrentPage { get; set; }
        public IEnumerable<PageData> MainMenuList { get; internal set; }
    }

}