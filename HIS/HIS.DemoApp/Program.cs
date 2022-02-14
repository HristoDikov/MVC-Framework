namespace HIS.DemoApp
{
    using HIS.Http;
    using System.Threading.Tasks;

    class Program
    {
        static async Task Main(string[] args)
        {
            var httpServer = new HttpServer(81);
            await httpServer.StartAsync();
        }
    }
}
