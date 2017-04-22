using LibraryBLL;
using LibraryModel.Book;
using LibraryModel.User;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace LibraryManageMVC.Controllers
{
    public class BookInfoController : BaseController
    {
        BookBLL bll = new BookBLL();
        // GET: SelfInfo
       
        /// <summary>
        /// 书籍详情
        /// </summary>
        /// <returns></returns>
        public ActionResult BookDetail(string bookid)
        {
            BookModel model = new BookModel();
            if (!string.IsNullOrWhiteSpace(bookid))
            {
                model = bll.SelectDetail(Int64.Parse(bookid));
            }

            return View(model);
        }
        /// <summary>
        /// 获取目录
        /// </summary>
        /// <param name="bookid"></param>
        /// <returns></returns>
        public JsonResult SelectList(string bookid)
        {
            ChapterModel model = new ChapterModel();
            if (!string.IsNullOrWhiteSpace(bookid))
            {
                model.BookID = Int64.Parse(bookid);
            }
            List<ChapterModel> list = bll.SelectList(model.BookID);

            return Json(new {List=list }, JsonRequestBehavior.DenyGet);
        }
        /// <summary>
        /// 获取章节内容
        /// </summary>
        /// <param name="CHID"></param>
        /// <param name="Bookid"></param>
        /// <returns></returns>
        public ViewResult ChapterContent(string CHID, string Bookid) 
        {
            ChapterModel model = new ChapterModel();
            if (!string.IsNullOrWhiteSpace(CHID))
            {
                model.CHID = Int64.Parse(CHID);
            }
            if (!string.IsNullOrWhiteSpace(Bookid))
            {
                model.BookID = Int64.Parse(Bookid);
            }
            model = bll.ChapterContent(model);
            int min = 0;int max = 0;
            bll.ChapterminMaxID(model,out min,out max);
            ViewBag.Min = min;
            ViewBag.Max = max;
            return View(model);
        }
        /// <summary>
        /// 添加收藏
        /// </summary>
        /// <param name="Bookid"></param>
        /// <returns></returns>
        public JsonResult AddCollection( string Bookid)
        {
            BookCollectionModel model = new BookCollectionModel();
            if (!string.IsNullOrWhiteSpace(Bookid))
            {
                model.BookID = Int64.Parse(Bookid);
            }
            LoginInfoModel loginmodel = base.LoginUser as LoginInfoModel;
            if(loginmodel!=null && loginmodel.Account != 0)
            {
                model.Account = loginmodel.Account;
            }
            int issuccess = 0;
            if (bll.AddCollection(model))
            {
                issuccess = 1;
            }
            return Json(new { Issuccess=issuccess },JsonRequestBehavior.DenyGet);
        }
        /// <summary>
        /// 指定类型的书籍
        /// </summary>
        /// <returns></returns>
        public ActionResult TypeBooks(string booktypeid)
        {
            if (!string.IsNullOrWhiteSpace(booktypeid))
            {
                ViewBag.booktypeid = booktypeid;
                long BookTypeID = Int64.Parse(booktypeid);
                string BookTypeName = bll.FindBookTypeNameFromID(BookTypeID);
                ViewBag.BookTypeName = BookTypeName;
                return View();
            }
            else
            {
                return Content("书籍类型ID不正确");
            }
        }
        public JsonResult SelectTypeBook(string booktypeid ,string PageIndex,string PageSize)
        {
            List<BookModel> list = bll.SelectTypeBook(booktypeid, PageIndex, PageSize);
            return Json(new { List = list }, JsonRequestBehavior.DenyGet);
        }
        /// <summary>
        /// 点击书籍更新浏览个数
        /// </summary>
        /// <param name="bookid"></param>
        /// <returns></returns>
        public JsonResult UpdateVisisNum(string bookid)
        {
            bool isSuccess = false;
            if (bll.UpdateVisisNum(bookid))
            {
                isSuccess = true;
            }
            return Json(new { IsSuccess = isSuccess }, JsonRequestBehavior.DenyGet);
        }
        /// <summary>
        /// 投诉作者
        /// </summary>
        /// <param name="Bookid"></param>
        /// <param name="ReportContent"></param>
        /// <returns></returns>
        public JsonResult AddReport(string WriterID, string ReportContent)
        {
            ReportModel model = new ReportModel();
            bool issuccess = false;
            LoginInfoModel loginmodel = base.LoginUser as LoginInfoModel;
            if (loginmodel != null && loginmodel.Account != 0)
            {
                model.Complainant = loginmodel.Account;
            }
            model.ReportID = Int64.Parse(WriterID);
            model.ReportReason = ReportContent;
            model.ReportTime = DateTime.Now;
            issuccess = bll.AddReport(model);
            return Json(new { IsSuccess = issuccess }, JsonRequestBehavior.DenyGet);
        }
        /// <summary>
        /// 添加点评
        /// </summary>
        /// <param name="Bookid"></param>
        /// <param name="CommentContent"></param>
        /// <returns></returns>
        public JsonResult AddComment(string Bookid, string CommentContent)
        {
            CommentModel model = new CommentModel();
            bool issuccess = false;
            LoginInfoModel loginmodel = base.LoginUser as LoginInfoModel;
            if (loginmodel != null && loginmodel.Account != 0)
            {
                model.ACCount = loginmodel.Account;
            }
            model.BookID = Int64.Parse(Bookid);
            model.BContent = CommentContent;
            model.CTime = DateTime.Now;
            issuccess = bll.AddComment(model);
            return Json(new { IsSuccess = issuccess }, JsonRequestBehavior.DenyGet);
        }
        /// <summary>
        /// 自己搜搜界面
        /// </summary>
        /// <returns></returns>
        public ViewResult FindBook()
        {
            return View();
        }
        /// <summary>
        /// 获取搜索的书籍
        /// </summary>
        /// <param name="bookname"></param>
        /// <param name="writer"></param>
        /// <returns></returns>
        public JsonResult FindBookList(string bookname,string writer)
        {
            List<BookModel> list = new List<BookModel>();
            BookModel model = new BookModel();
            if (!string.IsNullOrWhiteSpace(bookname))
            {
                model.BName = bookname;
            }
            if (!string.IsNullOrWhiteSpace(writer))
            {
                model.Writer = writer;
            }
            list = bll.FindBookList(model);
            return Json(new { List = list }, JsonRequestBehavior.DenyGet);
        }
        /// <summary>
        /// 获取
        /// </summary>
        /// <param name="bookid"></param>
        /// <returns></returns>
        public JsonResult FindComment(string bookid, string pageindex, string pagesize)
        {
            if (!string.IsNullOrWhiteSpace(bookid)&& !string.IsNullOrWhiteSpace(pageindex) && !string.IsNullOrWhiteSpace(pagesize) )
            {
                long Bookid = Int64.Parse(bookid);
                int Pageindex = Int32.Parse(pageindex);
                int Pagesize = Int32.Parse(pagesize);
                List<CommentModel> list = bll.FindComment(Bookid, Pageindex, Pagesize);
                return Json(new {Message="获取成功！", List = list }, JsonRequestBehavior.DenyGet);
            }
            return Json(new { Message = "获取失败！" }, JsonRequestBehavior.DenyGet);
        }
        /// <summary>
        /// 获取书籍的章节
        /// </summary>
        /// <param name="Bookid"></param>
        /// <returns></returns>
        //public JsonResult FindChapter(string Bookid)
        //{
        //    long bookid = 0;
        //    if (!string.IsNullOrWhiteSpace(Bookid))
        //    {
        //        bookid = Int64.Parse(Bookid);
        //    }
        //    List<ChapterModel> list = new List<ChapterModel>();
        //    list =  bll.FindChapter(bookid);
        //    return Json(new { List = list }, JsonRequestBehavior.DenyGet);
        //}
    }
}