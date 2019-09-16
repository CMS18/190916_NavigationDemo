using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Alloy.Models.Pages;
using EPiServer.Core;

namespace Alloy.Models.ViewModels
{
    public class MenuItem
    {
        public PageData Page { get; set; }

        public bool Active { get; set; }
    }

    public class DemoPageViewModel
    {
        public DemoPage CurrentPage { get; set; }
        public IEnumerable<PageData> MainMenuList { get; internal set; }

        public IEnumerable<MenuItem> MainMenuListWithItems { get; internal set; }

        public IEnumerable<PageData> MyListOfPages { get; set; }

    }

}