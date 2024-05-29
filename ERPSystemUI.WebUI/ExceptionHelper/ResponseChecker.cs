using ERPSystemUI.Model.CustomException;
using System.Net;

namespace ERPSystemUI.WebUI.ExceptionHelper
{
    public class ResponseChecker
    {
        public void Checker(int StatusCode)
        {
            if (StatusCode == (int)HttpStatusCode.NotFound)
            {
                throw new NotFoundException();
            }
            else if (StatusCode == (int)HttpStatusCode.Unauthorized)
            {
                throw new UnauthorizedException();
            }
            else if (StatusCode == (int)HttpStatusCode.InternalServerError)
            {
                throw new InternalServerErrorException();
            }
            else if (StatusCode == (int)HttpStatusCode.BadGateway)
            {
                throw new BadGatewayException();
            }
            else if (StatusCode == (int)HttpStatusCode.GatewayTimeout)
            {
                throw new GatewayTimeoutException();
            }
            else if (StatusCode == (int)HttpStatusCode.Forbidden)
            {
                throw new ForbiddenException();
            }
        }


    }
}
