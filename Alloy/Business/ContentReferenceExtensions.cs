using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EPiServer;
using EPiServer.Core;
using EPiServer.ServiceLocation;

namespace Alloy.Business
{
    public static class ContentReferenceExtensions
    {

        public static T Get<T>(this ContentReference contentReference) where T : IContent
        {
            var loader = ServiceLocator.Current.GetInstance<IContentLoader>();

            return loader.Get<T>(contentReference);
        }
    }
}