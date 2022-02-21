namespace HIS.Http
{
    using System;

    public class HttpServerException : Exception
    {
        public HttpServerException(string message)
            :base(message)
        {
        }
    }
}
