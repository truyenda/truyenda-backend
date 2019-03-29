using System.Web.Http;
using System.Web.Mvc;

namespace ReadComic.Areas.Home
{
    public class HomeAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Home";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.Routes.MapHttpRoute(
               "APICheckExit",
               "api/home/check-exit",
               new { controller = "Register", action = "CheckExistAccount", id = UrlParameter.Optional }
           );

            context.Routes.MapHttpRoute(
               "APIRegister",
               "api/home/register",
               new { controller = "Register", action = "CreateAccount", id = UrlParameter.Optional }
           );

            context.Routes.MapHttpRoute(
                "APICheckLogin",
                "login",
                new { controller = "Login", action = "CheckLogin", id = UrlParameter.Optional }
            );

            context.Routes.MapHttpRoute(
                "APILogout",
                "logout",
                new { controller = "Login", action = "Logout", id = UrlParameter.Optional }
            );

            context.Routes.MapHttpRoute(
                "GetNewComic",
                "NewComicList",
                new { controller = "Home", action = "GetNewComicList", id = UrlParameter.Optional }
            );

            context.Routes.MapHttpRoute(
                "GetAccount",
                "accounts/my",
                new { controller = "Login", action = "GetAccount", id = UrlParameter.Optional }
            );

            context.MapRoute(
                "Home_default",
                "Home/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}