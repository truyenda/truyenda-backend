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
                "commics/{id}",
                new { controller = "Home", action = "GetTruyen", id = UrlParameter.Optional },
                constraints: new { httpMethod = new HttpMethodConstraint("GET") }
            );

            context.Routes.MapHttpRoute(
                "GetALLComic",
                "stories/all",
                new { controller = "Home", action = "GetAllComicList", id = UrlParameter.Optional },
                constraints: new { httpMethod = new HttpMethodConstraint("GET") }
            );

            context.Routes.MapHttpRoute(
                "GetComicWithCategorys",
                "stories/categorys/{id}",
                new { controller = "Home", action = "GetComicListWithCategorys", id = UrlParameter.Optional },
                constraints: new { httpMethod = new HttpMethodConstraint("GET") }
            );

            context.Routes.MapHttpRoute(
                "GetComicWithRecommend",
                "stories/recommend",
                new { controller = "Home", action = "GetRandomComicList", id = UrlParameter.Optional },
                constraints: new { httpMethod = new HttpMethodConstraint("GET") }
            );

            context.Routes.MapHttpRoute(
               "SearchComic",
               "stories/search/{query}/page/{index}",
               new { controller = "Home", action = "SearchComic", id = UrlParameter.Optional },
               constraints: new { httpMethod = new HttpMethodConstraint("GET") }
           );

            context.Routes.MapHttpRoute(
               "ReadComic",
               "chapters/{Id_Chuong}/read",
               new { controller = "Home", action = "ReadComic", Id_Chuong = UrlParameter.Optional },
               constraints: new { httpMethod = new HttpMethodConstraint("GET") }
           );

            context.Routes.MapHttpRoute(
               "GetComicOfDay",
               "stories/day",
               new { controller = "Home", action = "GetComicOfDay", id = UrlParameter.Optional },
               constraints: new { httpMethod = new HttpMethodConstraint("GET") }
           );

            context.Routes.MapHttpRoute(
               "GetComicOfWeek",
               "stories/week",
               new { controller = "Home", action = "GetComicOfWeek", id = UrlParameter.Optional },
               constraints: new { httpMethod = new HttpMethodConstraint("GET") }
           );

            context.Routes.MapHttpRoute(
               "GetComicOfMonth",
               "stories/month",
               new { controller = "Home", action = "GetComicOfMonth", id = UrlParameter.Optional },
               constraints: new { httpMethod = new HttpMethodConstraint("GET") }
           );

            context.Routes.MapHttpRoute(
               "TheoDoiTruyen",
               "bookmarks",
               new { controller = "TheoDoiTruyen", action = "TheoDoiTruyen", id = UrlParameter.Optional },
               constraints: new { httpMethod = new HttpMethodConstraint("POST") }
           );

            context.Routes.MapHttpRoute(
               "GetListTheoDoiTruyen",
               "bookmarks/my",
               new { controller = "TheoDoiTruyen", action = "GetTheoDoiTruyen", id = UrlParameter.Optional },
               constraints: new { httpMethod = new HttpMethodConstraint("GET") }
           );

            context.Routes.MapHttpRoute(
               "DeleteBookMark",
               "bookmarks/{id}",
               new { controller = "TheoDoiTruyen", action = "XoaTheoDoiTruyen", id = UrlParameter.Optional },
               constraints: new { httpMethod = new HttpMethodConstraint("DELETE") }
           );

            context.Routes.MapHttpRoute(
               "UpdateBookMark",
               "bookmarks/{id}",
               new { controller = "TheoDoiTruyen", action = "UpdateTheoDoi", id = UrlParameter.Optional },
               constraints: new { httpMethod = new HttpMethodConstraint("PUT") }
           );

            context.Routes.MapHttpRoute(
                "Forgot",
                "forgot",
                new { controller = "Login", action = "Forgot", id = UrlParameter.Optional },
                constraints: new { httpMethod = new HttpMethodConstraint("POST") }
            );

            context.Routes.MapHttpRoute(
                "ChangePass",
                "forgot",
                new { controller = "Login", action = "ChangePass", id = UrlParameter.Optional },
                constraints: new { httpMethod = new HttpMethodConstraint("PUT") }
            );

            context.MapRoute(
                "Home_default",
                "Home/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}