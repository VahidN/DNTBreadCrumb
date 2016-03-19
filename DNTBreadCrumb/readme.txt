Usage:
-----------------
- After installing the DNTBreadCrumb package,
  add the `Views\Shared\_BreadCrumb.cshtml` definition to your `_Layout.cshtml` file:

  @{Html.RenderPartial("_BreadCrumb");}

- Then add the `BreadCrumb` attribute to your controller or action methods:

    [BreadCrumb(Title = "News Root", UseDefaultRouteUrl = true, RemoveAllDefaultRouteValues = true,
        Order = 0, GlyphIcon = "glyphicon glyphicon-link")]
    public class NewsController : Controller
    {
        [BreadCrumb(Title = "Main index", Order = 1)]
        public ActionResult Index()
        {
            return View();
        }

Please follow the `MVCBreadCrumbTest` sample for more scenarios:
https://github.com/VahidN/DNTBreadCrumb/MVCBreadCrumbTest