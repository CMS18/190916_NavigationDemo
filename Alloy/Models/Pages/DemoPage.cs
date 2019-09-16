using System;
using System.ComponentModel.DataAnnotations;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;

namespace Alloy.Models.Pages
{
    [ContentType(DisplayName = "Demo Page", 
        GUID = "ccae87ae-be03-483b-a3f9-24cfc542290e", 
        Description = "")]
    public class DemoPage : PageData
    {
        public virtual XhtmlString MainBody { get; set; }

        public virtual ContentReference IdOfParentPageToList { get; set; }
    }
}