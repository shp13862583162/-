using LibraryBLL;
using LibraryModel.Book;
using LibraryModel.User;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LibraryManageMVC.Controllers
{
    public class WriterController : BaseController
    {
        WriterBLL bll = new WriterBLL();
        // GET: Wroter
        /// <summary>
        /// 作者专区
        /// </summary>
        /// <returns></returns>
        public ViewResult WriterWeb(string account)
        {
            ViewBag.account = account;
            return View();
        }
        /// <summary>
        /// 添加晋升表
        /// </summary>
        /// <param name="Account"></param>
        /// <returns></returns>
        public JsonResult AddPromotion(string Account)
        {
            PromotionModel model = new PromotionModel();
            if (!string.IsNullOrWhiteSpace(Account))
            {
                model.PAccount = Int64.Parse(Account);
            }
            model.PApplyTime = DateTime.Now;
            //验证申请权限的用户是否存在
            int num = bll.SelectPromotionNum(model);
            if (num == 0)
            {
                if (bll.AddPromotion(model))
                {
                    return Json(new { IsTrue = true, JsonRequestBehavior.DenyGet });
                }
                else
                {
                    return Json(new { IsTrue = false, JsonRequestBehavior.DenyGet });
                }
            }
            else
            {
                return Json(new { IsTrue = false,Message = "用户已经申请", JsonRequestBehavior.DenyGet });
            }
        }
        /// <summary>
        /// 上架书籍页面
        /// </summary>
        /// <returns></returns>
        public ViewResult ShelvesBooks()
        {
            return View();
        }
        /// <summary>
        /// 申请书籍页面
        /// </summary>
        /// <returns></returns>
        public ViewResult ApplyBooks()
        {
            return View();
        }
        /// <summary>
        /// 获取上架书籍
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public JsonResult GetShelvesBooks(string account,string PageIndex,string PageSize)
        {
            BookModel model = new BookModel();
            if (!string.IsNullOrWhiteSpace(account))
            {
                model.WriterID = Int64.Parse(account);
            }
            if (!string.IsNullOrWhiteSpace(PageIndex))
            {
                model.pageindex = Int32.Parse(PageIndex);
            }
            if (!string.IsNullOrWhiteSpace(PageSize))
            {
                model.pagesize = Int32.Parse(PageSize);
            }
            model.BStatue = 2;
            int toatl;
            List<BookModel> list = bll.GetShelvesBooks(model,out toatl);
            return Json(new {List =list,total = toatl, JsonRequestBehavior.DenyGet });
        }

        public JsonResult FindApply(string account,string bookname,string startdatetime ,string enddatetime,string PageIndex, string PageSize, string ApplyStatue = "4")
        {
            BookModel model = new BookModel();
            if (!string.IsNullOrWhiteSpace(bookname))
            {
                model.BName = bookname;
            }
            if (!string.IsNullOrWhiteSpace(startdatetime))
            {
                string mystarttime = startdatetime + " 00:00:00";
                model.StartApplyTime = DateTime.Parse(mystarttime);
            }
            if (!string.IsNullOrWhiteSpace(enddatetime))
            {
                string myendtime = enddatetime + " 23:59:59";
                model.EndApplyTime = DateTime.Parse(myendtime);
            }
            if (!string.IsNullOrWhiteSpace(ApplyStatue))
            {
                model.BStatue = Int32.Parse(ApplyStatue);
            }
            if (!string.IsNullOrWhiteSpace(account))
            {
                model.WriterID = Int64.Parse(account);
            }
            if (!string.IsNullOrWhiteSpace(PageIndex))
            {
                model.pageindex = Int32.Parse(PageIndex);
            }
            if (!string.IsNullOrWhiteSpace(PageSize))
            {
                model.pagesize = Int32.Parse(PageSize);
            }
            int toatl;
            List<BookModel> list = bll.GetShelvesBooks(model, out toatl);
            return Json(new { List = list, Total = toatl, JsonRequestBehavior.DenyGet });
        }

        /// <summary>
        /// 作者申请书籍界面
        /// </summary>
        /// <returns></returns>
        public ViewResult ApplyBook()
        {
            return View();
        }
        /// <summary>
        /// 申请书籍
        /// </summary>
        /// <param name="BookName"></param>
        /// <param name="Introduction"></param>
        /// <returns></returns>
        public ActionResult ApplyABook(HttpPostedFileBase upImg,string BookName,string Introduction,string BookType)
        {
            string fileName = System.IO.Path.GetFileName(upImg.FileName);
            string filePhysicalPath = Server.MapPath("~/upload/"+ LoginUser.Account.ToString() + "/" + fileName);
            string pic = "", error = "";
            try
            {
                string myfilename = Server.MapPath("~/upload/" + LoginUser.Account.ToString());    //组合路径  
                Directory.CreateDirectory(myfilename);
                upImg.SaveAs(filePhysicalPath);
                pic = "~/upload/"+ LoginUser.Account.ToString() + "/" + fileName;
            }
            catch (Exception ex)
            {
                error = ex.Message;
            }
           
            bool issuccess = false;
            BookModel model = new BookModel();
            if (!string.IsNullOrWhiteSpace(BookName))
            {
                model.BName = BookName;
            }
            if (!string.IsNullOrWhiteSpace(Introduction))
            {
                model.BIntroduction = Introduction;
            }
            if (!string.IsNullOrWhiteSpace(BookType))
            {
                model.BTypeID = Int32.Parse(BookType);
            }
            model.PictureUrl = pic;
            model.Writer = LoginUser.Name;
            model.WriterID = LoginUser.Account;
            model.ApplyTime = DateTime.Now;
            issuccess = bll.AddBook(model);

            if (issuccess)
            {
                return Content("<script>alert('开始申请')</script>");
            }else
            {
                return Content("<script>alert('申请失败')</script>");
            }
            //return Json(new
            //{
            //    IsSuccess = issuccess,
            //    pic = pic,
            //    error = error,
            //    JsonRequestBehavior.DenyGet
            //});
        }
        /// <summary>
        /// 获取所有的有效的书籍类型
        /// </summary>
        /// <returns></returns>
        public JsonResult FindBookType()
        {
            List<BookType> list = new List<BookType>();
            list = bll.FindBookType();
            return Json(new { List = list, JsonRequestBehavior.DenyGet });
        }
        /// <summary>
        /// 作者添加章节界面
        /// </summary>
        /// <returns></returns>
        public ActionResult WriterAddChapter(string bookid)
        {
            BookModel model = new BookModel();
            BookBLL bl = new BookBLL();
            if (!string.IsNullOrWhiteSpace(bookid))
            {
                model = bl.SelectDetail(Int64.Parse(bookid));
            }
            return View(model);
        }
        /// <summary>
        /// 添加章节，具体界面
        /// </summary>
        /// <returns></returns>
        public ActionResult AddChapter(string bookid)
        {
            ViewBag.bookid = bookid;
            return View();
        }
        public JsonResult AddMyChapter(string bookid, string chapterName,string wzcontent)
        {
            bool issuccess = false;
            ChapterModel model = new ChapterModel();
            if (!string.IsNullOrWhiteSpace(bookid))
            {
                model.BookID = Int64.Parse(bookid);
            }
            if (!string.IsNullOrWhiteSpace(chapterName))
            {
                model.CHName = chapterName;
            }
            if (!string.IsNullOrWhiteSpace(wzcontent))
            {
                model.CHContent = wzcontent;
            }
            issuccess = bll.AddChapter(model);

            return Json(new { Issuccess = issuccess, JsonRequestBehavior.DenyGet });
        }
    }
}