using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace SethWebster.WebApi.TokenAuthentication
{
    public class TokenAuthenticationFilter : IAuthorizationFilter
    {
        ITokenAuthenticator _authenticator;

        public TokenAuthenticationFilter(ITokenAuthenticator authenticator)
        {
            _authenticator = authenticator;
        }

        public Task<HttpResponseMessage> ExecuteAuthorizationFilterAsync(HttpActionContext actionContext, CancellationToken cancellationToken, Func<Task<HttpResponseMessage>> continuation)
        {
            try
            {
                var result = new TokenAuthenticator().Authenticate(actionContext, _authenticator);
            }
            catch (Exception e)
            {
                TaskCompletionSource<HttpResponseMessage> tcs = new TaskCompletionSource<HttpResponseMessage>();
                tcs.SetException(e);
                return tcs.Task;
            }

            if (actionContext.Response != null)
            {
                TaskCompletionSource<HttpResponseMessage> tcs = new TaskCompletionSource<HttpResponseMessage>();
                tcs.SetResult(actionContext.Response);
                return tcs.Task;
            }
            else
            {
                return continuation().ContinueWith<HttpResponseMessage>((tsk) =>
                {
                    HttpResponseMessage response = tsk.Result;
                    return response;

                }, TaskContinuationOptions.OnlyOnRanToCompletion);
            }
        }

        public bool AllowMultiple
        {
            get { throw new NotImplementedException(); }
        }
    }
}
