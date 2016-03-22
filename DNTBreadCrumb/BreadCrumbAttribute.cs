using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;

namespace DNTBreadCrumb
{
    /// <summary>
    /// BreadCrumb Action Filter. It can be applied to action methods or controllers.
    /// </summary>
    [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
    public class BreadCrumbAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// Use this property to remove all of the previous items of the current stack
        /// </summary>
        public bool ClearStack { get; set; }

        /// <summary>
        /// An optional glyph icon of the current item
        /// </summary>
        public string GlyphIcon { get; set; }

        /// <summary>
        /// If UseDefaultRouteUrl is set to true, this property indicated all of the route items should be removed from the final URL
        /// </summary>
        public bool RemoveAllDefaultRouteValues { get; set; }

        /// <summary>
        /// If UseDefaultRouteUrl is set to true, this property indicated which route items should be removed from the final URL
        /// </summary>
        public string[] RemoveRouteValues { get; set; }

        /// <summary>
        /// Title of the current item
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// A constant URL of the current item. If one of the UseDefaultRouteUrl or UseCurrentRouteUrl properties is set to true, its value will be ignored
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// This property is useful when you need a back functionality. If it's true, the Url value will be previous Url using UrlReferrer
        /// </summary>
        public bool UsePreviousUrl { get; set; }

        /// <summary>
        /// This property is useful for controller level bread crumbs. If it's true, the Url value will be calculated automatically from the DefaultRoute
        /// </summary>
        public bool UseDefaultRouteUrl { get; set; }

        /// <summary>
        /// This property is useful for controller level bread crumbs. If it's true, the Url value will be calculated automatically from the urrentRoute
        /// </summary>
        public bool UseCurrentRouteUrl { get; set; }

        /// <summary>
        /// Adds the current item to the stack
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (UsePreviousUrl)
            {
                Url = filterContext.HttpContext.Request.UrlReferrer.AbsolutePath;
            }

            if (shouldIgnore(filterContext))
            {
                return;
            }

            if (ClearStack)
            {
                filterContext.HttpContext.ClearBreadCrumbs();
            }

            var url = string.IsNullOrWhiteSpace(Url) ? filterContext.HttpContext.Request.Url.ToString() : Url;

            if (UseDefaultRouteUrl || UseCurrentRouteUrl)
            {
                url = getDefaultControllerActionUrl(filterContext);
            }

            filterContext.HttpContext.AddBreadCrumb(new BreadCrumb
            {
                Url = url,
                Title = Title,
                Order = Order,
                GlyphIcon = GlyphIcon
            });

            base.OnActionExecuting(filterContext);
        }

        private static bool shouldIgnore(ControllerContext filterContext)
        {
            return filterContext.IsChildAction ||
                   !string.Equals(filterContext.HttpContext.Request.HttpMethod, "GET", StringComparison.OrdinalIgnoreCase);
        }

        private string getDefaultControllerActionUrl(ActionExecutingContext filterContext)
        {
            var defaultAction = UseCurrentRouteUrl ?
                ((Route)filterContext.RequestContext.RouteData.Route).Defaults["action"] as string :
                ((Route)RouteTable.Routes["Default"]).Defaults["action"] as string;

            if (RemoveAllDefaultRouteValues)
            {
                return new UrlHelper(filterContext.RequestContext).ActionWithoutRouteValues(defaultAction);
            }

            if (RemoveRouteValues == null || !RemoveRouteValues.Any())
            {
                return new UrlHelper(filterContext.RequestContext).Action(defaultAction);
            }

            return new UrlHelper(filterContext.RequestContext).ActionWithoutRouteValues(defaultAction, RemoveRouteValues);
        }
    }
}