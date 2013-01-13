using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Controllers;

namespace SethWebster.WebApi.TokenAuthentication
{
    public interface ITokenAuthenticator
    {
        bool Authenticate(string token, HttpActionContext context);
    }
}
