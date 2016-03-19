using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace DNTBreadCrumb
{
    /// <summary>
    /// UrlHelper Extensions
    /// </summary>
    public static class UrlHelperExtensions
    {
        /// <summary>
        /// Creates a URL without its route values
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="action"></param>
        /// <param name="removeRouteValues"></param>
        /// <returns></returns>
        public static string ActionWithoutRouteValues(this UrlHelper helper, string action, string[] removeRouteValues = null)
        {
            var routeValues = helper.RequestContext.RouteData.Values;
            var routeValueKeys = routeValues.Keys.Where(o => o != "controller" && o != "action").ToList();

            // Temporarily remove route values
            var oldRouteValues = new Dictionary<string, object>();

            foreach (var key in routeValueKeys)
            {
                if (removeRouteValues != null && !removeRouteValues.Contains(key))
                {
                    continue;
                }

                oldRouteValues[key] = routeValues[key];
                routeValues.Remove(key);
            }

            // Generate URL
            var url = helper.Action(action);

            // Reinsert route values
            foreach (var keyValuePair in oldRouteValues)
            {
                routeValues.Add(keyValuePair.Key, keyValuePair.Value);
            }

            return url;
        }
    }
}