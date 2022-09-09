

namespace PlayListProject.Application.CustomsExceptions
{
    public class BadRequestException: PlayListException
    {
        public BadRequestException(string message = "Bad Request"):base(System.Net.HttpStatusCode.BadRequest, message)
        {

        }
    }
}
