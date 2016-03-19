using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DNTBreadCrumb
{
    /// <summary>
    /// BreadCrumb Extentions
    /// </summary>
    public static class BreadCrumbExtentions
    {
        /// <summary>
        /// The key value of the current item in the ctx.Items
        /// </summary>
        public const string CurrentBreadCrumbKey = "Current_BreadCrumb_Key";

        /// <summary>
        /// Clears the stack of the current items
        /// </summary>
        /// <param name="ctx"></param>
        public static void ClearBreadCrumbs(this HttpContextBase ctx)
        {
            ctx.Items[CurrentBreadCrumbKey] = new List<BreadCrumb>();
        }

        /// <summary>
        /// Clears the stack of the current items
        /// </summary>
        /// <param name="ctx"></param>
        public static void ClearBreadCrumbs(this ControllerBase ctx)
        {
            ctx.ControllerContext.HttpContext.ClearBreadCrumbs();
        }

        /// <summary>
        /// Adds a custom bread crumb to the list
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="breadCrumb"></param>
        public static void AddBreadCrumb(this HttpContextBase ctx, BreadCrumb breadCrumb)
        {
            var currentBreadCrumbs = ctx.Items[CurrentBreadCrumbKey] as List<BreadCrumb> ?? new List<BreadCrumb>();
            if (currentBreadCrumbs.Any(crumb => crumb.Url.Equals(breadCrumb.Url, StringComparison.OrdinalIgnoreCase)))
            {
                return;
            }

            currentBreadCrumbs.Add(breadCrumb);
            ctx.Items[CurrentBreadCrumbKey] = currentBreadCrumbs;
        }

        /// <summary>
        /// Adds a custom bread crumb to the list
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="breadCrumb"></param>
        public static void AddBreadCrumb(this ControllerBase ctx, BreadCrumb breadCrumb)
        {
            ctx.ControllerContext.HttpContext.AddBreadCrumb(breadCrumb);
        }

        /// <summary>
        /// Sets the specified item's title
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="url"></param>
        /// <param name="title"></param>
        public static void SetBreadCrumbTitle(this HttpContextBase ctx, string url, string title)
        {
            var currentBreadCrumbs = ctx.Items[CurrentBreadCrumbKey] as List<BreadCrumb> ?? new List<BreadCrumb>();
            var breadCrumb = currentBreadCrumbs.FirstOrDefault(crumb => crumb.Url.Equals(url, StringComparison.OrdinalIgnoreCase));
            if (breadCrumb == null) return;

            breadCrumb.Title = title;
            ctx.Items[CurrentBreadCrumbKey] = currentBreadCrumbs;
        }

        /// <summary>
        /// Sets the specified item's title
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="url"></param>
        /// <param name="title"></param>
        public static void SetBreadCrumbTitle(this ControllerBase ctx, string url, string title)
        {
            ctx.ControllerContext.HttpContext.SetBreadCrumbTitle(url, title);
        }

        /// <summary>
        /// Sets the current item's title. It's useful for changing the title of the current action method's bread crumb dynamically.
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="title"></param>
        public static void SetCurrentBreadCrumbTitle(this HttpContextBase ctx, string title)
        {
            var url = ctx.Request.Url.ToString();
            var currentBreadCrumbs = ctx.Items[CurrentBreadCrumbKey] as List<BreadCrumb> ?? new List<BreadCrumb>();
            var breadCrumb = currentBreadCrumbs.FirstOrDefault(crumb => crumb.Url.Equals(url, StringComparison.OrdinalIgnoreCase));
            if (breadCrumb == null) return;

            breadCrumb.Title = title;
            ctx.Items[CurrentBreadCrumbKey] = currentBreadCrumbs;
        }

        /// <summary>
        /// Sets the current item's title. It's useful for changing the title of the current action method's bread crumb dynamically.
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="title"></param>
        public static void SetCurrentBreadCrumbTitle(this ControllerBase ctx, string title)
        {
            ctx.ControllerContext.HttpContext.SetCurrentBreadCrumbTitle(title);
        }
    }
}