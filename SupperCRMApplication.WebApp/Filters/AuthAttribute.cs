using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SupperCRMApplication.Common;

namespace SupperCRMApplication.WebApp.Filters
{
    public class AuthAttribute:Attribute,IAuthorizationFilter
    {
        public string? Roles { get; set; }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (!context.HttpContext.Session.Keys.Contains(Constants.Session_Id))
            {//session yoksa yani herhangi bir oturum açılmamışsa logine gönderir.
                context.Result = new RedirectResult("/Account/Login");
                return; 
            }
            else
            {
                if (!string.IsNullOrEmpty(Roles))
                {//Kulanıcıya rol verilmişse rolü al sprit et
                    string role = context.HttpContext.Session.GetString(Constants.Session_Role);
                    string[] roles = Roles.Split(',');// admin,user,visitor

                    if (!roles.Contains(role))
                    {//roller içinte rol yoksa dahsboarda yönlendir.
                        context.Result = new RedirectResult("/");
                        return;
                    }
                }
            }
        }
    }
}
