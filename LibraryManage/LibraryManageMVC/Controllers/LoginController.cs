using LibraryBLL;
using LibraryModel;
using LibraryModel.User;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LibraryManageMVC.Controllers
{
    public class LoginController : Controller
    {
        UserInfoBLL bll = new UserInfoBLL();
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 注册
        /// </summary>
        /// <returns></returns>
        public ActionResult Register(int type = 0)
        {
            if (type == 1||type ==3)
            {
                ViewBag.usertype = type;
                return View();
            }
            else
            {
                return Content("用户类型错误!!");
            }
        }
        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="usertype"></param>
        /// <param name="username"></param>
        /// <param name="account"></param>
        /// <param name="pwd"></param>
        /// <param name="usersex"></param>
        /// <returns></returns>
        public JsonResult Add(string usertype,string username,string account,string pwd,string usersex )
        {
            RegisterModel model = new RegisterModel();
            if (!string.IsNullOrWhiteSpace(usertype))
            {
                model.usertype = Int32.Parse(usertype);
            }
            if (!string.IsNullOrWhiteSpace(username))
            {
                model.username = username;
            }
            if (!string.IsNullOrWhiteSpace(account))
            {
                model.account = Int64.Parse(account);
            }
            if (!string.IsNullOrWhiteSpace(pwd))
            {
                model.pwd = pwd;
            }
            if (!string.IsNullOrWhiteSpace(usersex))
            {
                model.sex = Int32.Parse(usersex);
            }
            if (bll.AddUser(model))
            {
                return Json(true, JsonRequestBehavior.DenyGet);
            }
            return Json(false, JsonRequestBehavior.DenyGet);
        }
       
        /// <summary>
        /// 登陆
        /// </summary>
        /// <returns></returns>
        public ActionResult Login()
        {
            int nologin = 0;
            if (Request["nologin"] != null)
            {
                nologin = Int32.Parse(Request["nologin"]);
            }
            ViewBag.nologin = nologin; 
            return View();
        }
        /// <summary>
        /// 自动添加用户信息
        /// </summary>
        /// <returns></returns>
        public string UserInfoAdd()
        {
            HttpCookie cookie = HttpContext.Request.Cookies["LoginInfo"];
            if (cookie != null)
            {
                return cookie.Value;
            }
            else { return null; }
        }

        /// <summary>
        /// 登录验证
        /// </summary>
        /// <param name="UserAccount">账户</param>
        /// <param name="UserPassword">密码</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult CheckUser(string UserAccount,string UserPassword)
        {
            var success = 0;
            var istrue = true;

            LoginInfoModel model = new LoginInfoModel() {Account= Int64.Parse(UserAccount),Password = UserPassword };
            istrue = bll.CheckUser(model);
            if (istrue)
            {
                model = bll.SelectUser(model)[0] as LoginInfoModel;
                if (model.Type == 2)
                {
                    model.UserTypeName = "作者";
                }
                else if (model.Type == 1)
                {
                    model.UserTypeName = "读者";
                }
                else if (model.Type == 3)
                {
                    model.UserTypeName = "管理员";
                }
                HttpCookie cookie = new HttpCookie("LoginInfo", JsonConvert.SerializeObject(model));
                cookie.Expires = DateTime.Now.AddDays(2);
                Response.AppendCookie(cookie);
                //session的过期时间默认为30 分钟，这个时间是按照最后一次请求session来计算
                Session["LoginInfo"] = model;
            }

            return Json(success = 1, JsonRequestBehavior.DenyGet);
        }
      
    }
}