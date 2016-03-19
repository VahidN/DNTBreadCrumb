DNTBreadCrumb
=======
`DNTBreadCrumb` Creates custom bread crumb definitions, based on Twitter Bootstrap 3.x features.



Install via NuGet
-----------------
To install DNTBreadCrumb, run the following command in the Package Manager Console:

```
PM> Install-Package DNTBreadCrumb
```

You can also view the [package page](http://www.nuget.org/packages/DNTBreadCrumb/) on NuGet.



Usage:
-----------------
- After installing the DNTBreadCrumb package, add the `Views\Shared\_BreadCrumb.cshtml` definition to your `_Layout.cshtml` file:
```
@{Html.RenderPartial("_BreadCrumb");}
```

- Then add the `BreadCrumb` attribute to your controller or action methods:
```csharp
[BreadCrumb(Title = "Home", UseDefaultRouteUrl = true, Order = 0)]
public class HomeController : Controller
{
   [BreadCrumb(Title = "Main index", Order = 1)]
   public ActionResult Index()
   {
      return View();
   }
```
Please follow the `MVCBreadCrumbTest` sample for more scenarios.