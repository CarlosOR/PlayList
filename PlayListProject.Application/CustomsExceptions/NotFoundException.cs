namespace PlayListProject.Application.CustomsExceptions
{
    public class NotFoundException: PlayListException
    {
        public NotFoundException(string message = "NotFound"): base(System.Net.HttpStatusCode.NotFound, message)
        {

        }
    }
}
