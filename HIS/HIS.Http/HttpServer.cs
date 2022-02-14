namespace HIS.Http
{
    using System;
    using System.Net;
    using System.Net.Sockets;
    using System.Text;
    using System.Threading.Tasks;

    public class HttpServer : IHttpServer
    {
        private readonly TcpListener tcpListener;
        //Todo: actions
        public HttpServer(int port)
        {
            this.tcpListener = new TcpListener(IPAddress.Loopback, port);
        }

        public async Task ResetAsync()
        {
            this.Stop();
            await this.StartAsync();

        }

        public async Task StartAsync()
        {
            this.tcpListener.Start();
            while (true)
            {
                TcpClient tcpClient = await tcpListener.AcceptTcpClientAsync();

                //TODO: test
                // To allow "while" to work and listen for a new client
 #pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
                Task.Run(() => ProcessClientAsync(tcpClient));
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
            }
        }

        public void Stop()
        {
            this.tcpListener.Stop();
        }

        private async Task ProcessClientAsync(TcpClient tcpClient)
        {
            const string NewLine = "\r\n";
            using NetworkStream networkStream = tcpClient.GetStream();
            byte[] requestBytes = new byte[1000000]; // TODO: Use buffer
            int bytesRead = await networkStream.ReadAsync(requestBytes, 0, requestBytes.Length);
            string request = Encoding.UTF8.GetString(requestBytes, 0, bytesRead);
            byte[] fileContent = Encoding.UTF8.GetBytes("<h1>Hello, World!</h1>");
            string headers = "HTTP/1.0 200 OK" + NewLine +
                "Server: Hdikov/1.0" + NewLine + 
                "Content-Type: text/html" + NewLine +
                "COntent-Lenght: " + fileContent.Length + NewLine
                + NewLine;
            byte[] headersBytes = Encoding.UTF8.GetBytes(headers);
            //byte[] stringContent = Encoding.UTF8.GetBytes(content);
            //var response = new HttpResponse(HttpResponseCode.Ok, stringContent);
            //response.Headers.Add(new Header("Server", "SoftUniServer/1.0"));
            //response.Headers.Add(new Header("Content-Type", "text/html"));

            //byte[] responseBytes = Encoding.UTF8.GetBytes(response.ToString());
            await networkStream.WriteAsync(headersBytes, 0, headersBytes.Length);
            await networkStream.WriteAsync(fileContent, 0, fileContent.Length);

            Console.WriteLine(request);
            Console.WriteLine(new string('=', 60));
        }
    }
}

