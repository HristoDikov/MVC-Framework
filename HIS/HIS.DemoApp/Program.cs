namespace HIS.DemoApp
{
    using HIS.Http;
    using HIS.Http.Response;
    using System.Collections.Generic;
    using System.IO;
    using System.Threading.Tasks;

    class Program
    {
        static async Task Main(string[] args)
        {
            var routeTable = new List<Route>
            {
                new Route(HttpMethodType.Get, "/", Index),
                new Route(HttpMethodType.Get, "/users/login", Login),
                new Route(HttpMethodType.Post, "/users/login", DoLogin),
                new Route(HttpMethodType.Get, "/contact", Contact),
                new Route(HttpMethodType.Get, "/favicon.ico", FavIcon)
            };

            var httpServer = new HttpServer(81, routeTable);
            await httpServer.StartAsync();
        }

        private static HttpResponse FavIcon(HttpRequest arg)
        {
            var bContent = File.ReadAllBytes("wwwroot/favicon.ico");
            return new FileResponse(bContent, "image/x-icon");
        }

        private static HttpResponse Contact(HttpRequest arg)
        {
            return new HtmlResponse("<h1>contact</h1>");
        }

        public static HttpResponse Index(HttpRequest request) 
        {
            var username = request.SessionData.ContainsKey("Username") ? request.SessionData["Username"] : "Pal";
            return new HtmlResponse($"<h1>Home page. Hello, {username}!</h1>");
        }

        public static HttpResponse Login(HttpRequest request)
        {
            request.SessionData["Username"] = "Hristo";
            return new HtmlResponse("<h1>login page</h1>");
        }

        public static HttpResponse DoLogin(HttpRequest request)
        {
            return new HtmlResponse("<h1>login page</h1>");
        }
    }
}
