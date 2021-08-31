using System.Web.Mvc;

namespace RState.Areas.Reals
{
    public class RealsAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Reals";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Reals_default",
                "Reals/{controller}/{action}/{id}",
                new { controller = "RState", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}