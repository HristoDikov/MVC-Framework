namespace HIS.DemoApp
{
    using HIS.Http;
    using HIS.Http.Response;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    class Program
    {
        static async Task Main(string[] args)
        {
            var routeTable = new List<Route>();
            routeTable.Add(new Route(HttpMethodType.Get, "/", Index));
            routeTable.Add(new Route(HttpMethodType.Get, "/users/login", DoLogin));
            routeTable.Add(new Route(HttpMethodType.Post, "/users/login", DoLogin));
            routeTable.Add(new Route(HttpMethodType.Get, "/contact", Contact));
            routeTable.Add(new Route(HttpMethodType.Get, "/favicon.ico", FavIcon));

            var httpServer = new HttpServer(81, routeTable);
            await httpServer.StartAsync();
        }

        private static HttpResponse FavIcon(HttpRequest arg)
        {
            throw new NotImplementedException();
        }

        private static HttpResponse Contact(HttpRequest arg)
        {
            return new HtmlResponse("<h1>contact</h1>");
        }

        public static HttpResponse Index(HttpRequest request) 
        {
            return new HtmlResponse("<h1>home page</h1>");
        }

        public static HttpResponse Login(HttpRequest request)
        {
            return new HtmlResponse("<h1>login page</h1>");
        }

        public static HttpResponse DoLogin(HttpRequest request)
        {
            return new HtmlResponse("<h1>login page</h1>");
        }
    }
}
