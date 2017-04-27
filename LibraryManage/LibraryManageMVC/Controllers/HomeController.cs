using LibraryBLL;
using LibraryModel;
using LibraryModel.Book;
using LibraryModel.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LibraryManageMVC.Controllers
{
    public class HomeController : BaseController
    {
        UserInfoBLL bll = new UserInfoBLL();
        public ActionResult Detail()
        {
            return View();
        }
        public ActionResult Index()
        {
            BookBLL bbll = new BookBLL();
            int level = 1;
            ViewBag.level = level;
            List<BookModel> list = new List<BookModel>();
            list = bbll.FindRecommend();
            for(int i = 0; i < list.Count; i++)
            {
                if (string.IsNullOrWhiteSpace(list[i].PictureUrl))
                {
                    list[i].PictureUrl = "~/upload/Default/0.jpg";
                }
            }
            PageDataModel model = new PageDataModel();
            model.list = list;
            return View(model);
        }
       /// <summary>
       /// 获取缓存的数据 
       /// </summary>
       /// <returns></returns>
        public JsonResult GetSelfInfo()
        {
            LoginInfoModel model = base.LoginUser as LoginInfoModel;
            if (model == null || model.Account==0||model.Name=="")
            {
                model = bll.SelectUser(model)[0];
                if ( model.Type==2)
                {
                    model.UserTypeName = "作者";
                }
                else if (model.Type==1)
                {
                    model.UserTypeName = "读者";
                }
                else if(model.Type==3)
                {
                    model.UserTypeName = "管理员";
                }
            }
            return Json(model,JsonRequestBehavior.DenyGet);
        }
        /// <summary>
        /// 个人页面
        /// </summary>
        /// <param name="useraccount"></param>
        /// <returns></returns>
        public ActionResult Self(string useraccount)
        {
            UserModel model = new UserModel();
            if (!string.IsNullOrWhiteSpace(useraccount))
            {
                model.Account = Int64.Parse(useraccount);
            }
            List<UserModel> list = new List<UserModel>();
            if(base.LoginUser.Type  == 1)
            {
                list = bll.GetSelfInfoDetail(model);
            }
            else
            {
                list = bll.GetSelfInfo(model);
            }
            UserModel viewdata = new UserModel(); 
            if (list != null && list.Count > 0)
            {
                viewdata = list[0] as UserModel;
            }
            else
            {
                viewdata = bll.GetSelfInfoDetail1(model)[0];
                viewdata.PStatue = 0;
            }
            if (viewdata.Type == 2)
            {
                viewdata.TypeName = "作者";
            }
            else if (viewdata.Type == 1)
            {
                viewdata.TypeName = "读者";
            }
            else if (viewdata.Type == 3)
            {
                viewdata.TypeName = "管理员";
            }
            return View(viewdata);
        }
        /// <summary>
        /// 获取书信息
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public JsonResult GetIntroduction(string account)
        {
            BookModel model = new BookModel();
            if (!string.IsNullOrWhiteSpace(account))
            {
                model.Account = Int64.Parse(account);
            }
            List<BookModel> list = new List<BookModel>();
            list =  bll.GetIntroduction(model);
            
            return Json(list.ToArray(), JsonRequestBehavior.DenyGet);
        }
        /// <summary>
        /// 删除收藏
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public JsonResult ShancIntroduction(string bookid,string account)
        {
            BookModel model = new BookModel();
            if (!string.IsNullOrWhiteSpace(bookid))
            {
                model.BBookID = Int64.Parse(bookid);
            }
            if (!string.IsNullOrWhiteSpace(account))
            {
                model.Account = Int64.Parse(account);
            }
            bool issuccess = bll.ShancIntroduction(model);

            return Json(issuccess, JsonRequestBehavior.DenyGet);
        }
        public JsonResult SelectBookRang(string typenameid1, string typenameid2, string typenameid3) { 
            BookBLL bbll = new BookBLL();       
            List<BookModel> firstlist = bbll.SelectBookRang(typenameid1);
            List<BookModel> secondlist = bbll.SelectBookRang(typenameid2);
            List<BookModel> thirdlist = bbll.SelectBookRang(typenameid3);
            return Json(new { Firstlist = firstlist, Secondlist = secondlist, Thirdlist= thirdlist }, JsonRequestBehavior.DenyGet);
        }
        
    }
}