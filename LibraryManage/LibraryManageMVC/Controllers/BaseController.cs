using LibraryModel;
using LibraryModel.User;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LibraryManageMVC.Controllers
{
    public class BaseController : Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (Request.CurrentExecutionFilePath != "/Home/GetSelfInfo")
            {
                var cookie = Request.Cookies["LoginInfo"];
                if (cookie != null)
                {
                    var co = cookie.Value;
                    var json = JsonConvert.DeserializeObject<LoginInfoModel>(co);

                    if (Session["LoginInfo"] == null)
                    {
                        filterContext.Result = RedirectToAction("Login", "Login");
                    }
                    else
                    {
                        var session = Session["LoginInfo"] as LoginInfoModel;
                        if (json.Account == session.Account && json.Password == session.Password)
                        {

                        }
                        else if (cookie == null)
                        {
                            filterContext.Result = RedirectToAction("Login", "Login");
                        }
                    }
                }
                else
                {
                    filterContext.Result = RedirectToAction("Login", "Login", new { nologin = 1 });
                }
            }
            base.OnActionExecuting(filterContext);
        }
        protected LoginInfoModel LoginUser
        {
            get
            {
                if (Session["LoginInfo"] != null)
                {
                    var user = Session["LoginInfo"] as LoginInfoModel;
                    return user;
                }
                return new LoginInfoModel ();
            }

        }
    }
}