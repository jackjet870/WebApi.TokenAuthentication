using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SethWebster.WebApi.TokenAuthentication;
using System.Web.Http.Controllers;

namespace $rootnamespace$.TokenAuthenticator
//namespace Sethwebster.WebApi.TokenAuthenticator
{
    public class Authenticator : ITokenAuthenticator
    {
        public bool Authenticate(string token, HttpActionContext actionContext)
        {
            // Implement your authentication method here.
            throw new NotImplementedException();
        }
    }
}
