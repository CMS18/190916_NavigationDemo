using System.Collections.Generic;
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

            // Ladda in alla föräldrar
            var allAncestors = loader.GetAncestors(currentPageId);

            // Bygg en lista med MenuItem som view model för menyn.
            var list = new List<MenuItem>();
            foreach (var page in listWithPagesVisibleInNavigation)
            {
                var item = new MenuItem();
                item.Page = page;
                item.Active = allAncestors.Contains(page);

                list.Add(item);
            }


            IEnumerable<PageData> myListOfPages = new List<PageData>();
            // Hämta sidor under sidan som egenskapen IdOfParentPageToList pekar ut
            if (!ContentReference.IsNullOrEmpty(currentPage.IdOfParentPageToList))
            {
                myListOfPages = loader.GetChildren<PageData>(currentPage.IdOfParentPageToList);
            }

            var model = new DemoPageViewModel();
            model.CurrentPage = currentPage;
            model.MainMenuList = listWithPagesVisibleInNavigation;
            model.MainMenuListWithItems = list;
            model.MyListOfPages = myListOfPages;


            return View(model);
        }
    }
}