using System.Net;

namespace PlayListProject.Application.CustomsExceptions
{
    public class PlayListException: Exception
    {
        public HttpStatusCode StatusCode { get; set; }
        public string Message { get; set; }

        public PlayListException(HttpStatusCode statusCode, string message)
        {
            StatusCode = statusCode;
            Message = message;
        }
    }
}
