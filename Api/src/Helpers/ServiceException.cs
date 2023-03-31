using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Api.src.Helpers
{
    public class ServiceException : Exception
    {
        public HttpStatusCode StatusCode { get; set; }
       public override string Message { get; }

        public ServiceException (HttpStatusCode statusCode, string message )
        {
            StatusCode = statusCode;
            Message = message;
        }

        public static ServiceException NotFound(string message = "Id is not found")
        {
            return new ServiceException(HttpStatusCode.NotFound, message);
        }

        public static ServiceException Unauthorized(string message = "Unauthorized")
        {
            return new ServiceException(HttpStatusCode.Unauthorized, message);
        }

        public static ServiceException BadRequest(string message = "Bad request")
        {
            return new ServiceException(HttpStatusCode.BadRequest, message);
        }
    }
}