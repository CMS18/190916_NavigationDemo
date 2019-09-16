﻿using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Alloy.Models.Pages;
using Alloy.Models.ViewModels;
using EPiServer;
using EPiServer.Core;
using EPiServer.Filters;
using EPiServer.Framework.DataAnnotations;
using EPiServer.ServiceLocation;
using EPiServer.Web.Mvc;

namespace Alloy.Controllers
{
    public class DemoPageController : PageController<DemoPage>
    {
        private readonly IContentLoader loader;

        public DemoPageController(IContentLoader loader) // Constructor Injection
        {
            this.loader = loader;
        }

        public ActionResult Index(DemoPage currentPage)
        {
            // Andrahandsalternativ till Constructor Injection
            // var loader1 = ServiceLocator.Current.GetInstance<IContentLoader>();

            var currentPageId = currentPage.ContentLink; // ID of a page is always in ContentLink property

            var startPageId = ContentReference.StartPage;
            IEnumerable<IContent> pages = loader.GetChildren<IContent>(startPageId);

            //Tvättar bort sidor som man inte får eller kan länka till
            IEnumerable<PageData> filteredListOfPages = FilterForVisitor.Filter(pages).Cast<PageData>();

            //Tvätta bort sidor som inte ska synas i navigeringen.
            IEnumerable<PageData> listWithPagesVisibleInNavigation = filteredListOfPages.Where(p => p.VisibleInMenu == true);


            var model = new DemoPageViewModel();
            model.CurrentPage = currentPage;
            model.MainMenuList = listWithPagesVisibleInNavigation;


            return View(model);
        }
    }
}