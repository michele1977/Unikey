using System.Web;
using System.Web.Mvc;

namespace UnikeyFactoryTest.Presentation
{
    public class FilterConfig2
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
