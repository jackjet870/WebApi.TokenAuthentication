using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Controllers;

namespace SethWebster.WebApi.TokenAuthentication
{
    internal sealed class TokenAuthenticator
    {
        private readonly string _authorizationHeaderKey = "Authorization";

        public bool Authenticate(HttpActionContext actionContext, ITokenAuthenticator authenticator)
        {
            string token = string.Empty;
    
            try
            {
                token = actionContext.Request.Headers.GetValues(_authorizationHeaderKey).First();
            }
            catch (Exception)
            {
                actionContext.Response = new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest)
                {
                    Content = new StringContent("Missing Authorization Header (" + _authorizationHeaderKey + ")")
                };
                return false;
            }

            try
            {
                if (authenticator.Authenticate(token, actionContext))
                {
                    return true;
                }
                else
                {
                    throw new Exception();
                }
            }
            catch (Exception e)
            {
                actionContext.Response = new HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized)
                {
                    Content = new StringContent("Unauthorized User")
                };
                return false;
            }
        }
    }
}
