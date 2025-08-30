using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using System.Web.Http.Controllers;

public class AuthenticateAttribute : AuthorizeAttribute
{
    protected override bool IsAuthorized(HttpActionContext actionContext)
    {
        var headers = actionContext.Request.Headers;

        if (headers.Authorization != null && headers.Authorization.Scheme == "Basic")
        {
            string encodedUsernamePassword = headers.Authorization.Parameter;

            // Decode from Base64 to "username:password"
            string usernamePassword = Encoding.UTF8.GetString(Convert.FromBase64String(encodedUsernamePassword));
            string[] parts = usernamePassword.Split(':');

            if (parts.Length == 2)
            {
                string username = parts[0];
                string password = parts[1];

                // Hardcoded check — replace with database lookup in real use
                if (username == "admin" && password == "1234")
                    return true;
            }
        }

        return false;
    }

    protected override void HandleUnauthorizedRequest(HttpActionContext actionContext)
    {
        actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized, "Unauthorized");
        actionContext.Response.Headers.Add("WWW-Authenticate", "Basic");
    }
}
