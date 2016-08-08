using System.Web.Mvc;

namespace CreatioFrance.Areas.Avocats
{
    public class AvocatsAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Avocats";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Avocats_default",
                "Avocats/{controller}/{action}/{id}",
                defaults: new { action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "CreatioFrance.Areas.Avocats.Controllers" }
            );
        }
    }
}