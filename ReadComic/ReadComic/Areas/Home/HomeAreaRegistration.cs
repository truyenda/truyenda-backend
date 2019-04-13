using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;

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
                "updateAccount",
                "accounts/my",
                new { controller = "Information", action = "UpdateAccount", id = UrlParameter.Optional },
                constraints: new { httpMethod = new HttpMethodConstraint("PUT") }
            );

            context.Routes.MapHttpRoute(
                "getAccount",
                "accounts/my",
                new { controller = "Information", action = "GetAccount", id = UrlParameter.Optional },
                constraints: new { httpMethod = new HttpMethodConstraint("GET") }
            );

            context.Routes.MapHttpRoute(
                "CreateAccount",
                "accounts",
                new { controller = "Register", action = "CreateAccount", id = UrlParameter.Optional },
                constraints: new { httpMethod = new HttpMethodConstraint("POST") }
            );

            context.Routes.MapHttpRoute(
                "CheckAccount",
                "accounts/check",
                new { controller = "Register", action = "CheckExistAccount", id = UrlParameter.Optional },
                constraints: new { httpMethod = new HttpMethodConstraint("POST") }
            );

            context.Routes.MapHttpRoute(
                "GetTruyen",
                "storys",
                new { controller = "Home", action = "GetTruyen", id = UrlParameter.Optional },
                constraints: new { httpMethod = new HttpMethodConstraint("GET") }
            );

            context.Routes.MapHttpRoute(
                "GetALLComic",
                "story/all",
                new { controller = "Home", action = "GetAllComicList", id = UrlParameter.Optional },
                constraints: new { httpMethod = new HttpMethodConstraint("GET") }
            );

            context.Routes.MapHttpRoute(
                "GetComicWithCategorys",
                "story/categorys",
                new { controller = "Home", action = "GetComicListWithCategorys", id = UrlParameter.Optional },
                constraints: new { httpMethod = new HttpMethodConstraint("GET") }
            );

            context.MapRoute(
                "Home_default",
                "Home/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}