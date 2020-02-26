using System.Web;
using System.Web.Mvc;

namespace QUIZ_SYSTEM
{
	public class FilterConfig
	{
		public static void RegisterGlobalFilters(GlobalFilterCollection filters)
		{
			filters.Add(new HandleErrorAttribute());
		}
	}
}
