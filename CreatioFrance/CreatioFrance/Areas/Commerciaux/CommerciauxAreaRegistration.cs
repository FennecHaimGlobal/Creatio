using System.Web.Mvc;

namespace CreatioFrance.Areas.Commerciaux
{
    public class CommerciauxAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Commerciaux";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Commerciaux_default",
                "Commerciaux/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}