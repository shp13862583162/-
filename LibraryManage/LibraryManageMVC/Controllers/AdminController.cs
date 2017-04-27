using LibraryBLL;
using LibraryModel.Book;
using LibraryModel.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LibraryManageMVC.Controllers
{
    public class AdminController : Controller
    {
        AdminInfoBLL bll = new AdminInfoBLL();
        // GET: Admin

        /// <summary>
        /// 管理员专区页面
        /// </summary>
        /// <param name="useraccount"></param>
        /// <returns></returns>
        public ActionResult AdminDetail(string useraccount)
        {
            return View();
        }
        public ActionResult PromotionList()
        {
            return View();
        }
        /// <summary>
        /// 查询晋升数据
        /// </summary>
        /// <param name="account"></param>
        /// <param name="startdatetime"></param>
        /// <param name="enddatetime"></param>
        /// <param name="ApplyStatue"></param>
        /// <returns></returns>
        public JsonResult FindPromotion(string account,string startdatetime,string enddatetime,string ApplyStatue,string pageindex,string pagesize)
        {
            PromotionModel model = new PromotionModel();
            if (!string.IsNullOrWhiteSpace(account))
            {
                model.PAccount = Int64.Parse(account);
            }
            if (!string.IsNullOrWhiteSpace(startdatetime))
            {
                model.StartTime = DateTime.Parse(startdatetime);
            }   
            if (!string.IsNullOrWhiteSpace(enddatetime))
            {
                model.EndTime = DateTime.Parse(enddatetime);
            }
            if (!string.IsNullOrWhiteSpace(ApplyStatue))
            {
                model.PStatue = Int32.Parse(ApplyStatue);
            }
            if (!string.IsNullOrWhiteSpace(pageindex))
            {
                model.pageindex = Int32.Parse(pageindex);
            }  
            if (!string.IsNullOrWhiteSpace(pagesize))
            {
                model.pagesize = Int32.Parse(pagesize);
            }
            List<PromotionModel> list = new List<PromotionModel>();
            int number = 0;
            list =  bll.SelectPromotionList(model,out  number);
            return Json(new { List = list,Total = number },JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 书籍申请列表
        /// </summary>
        /// <returns></returns>
        public ViewResult ApplyList()
        {
            return View();
        }
        /// <summary>
        /// 查询 书籍申请 数据
        /// </summary>
        /// <param name="account"></param>
        /// <param name="startdatetime"></param>
        /// <param name="enddatetime"></param>
        /// <param name="ApplyStatue"></param>
        /// <returns></returns>
        public JsonResult FindApply(string bookname, string startdatetime, string enddatetime, string ApplyStatue, string pageindex, string pagesize)
        {
            BookModel model = new BookModel();
            if (!string.IsNullOrWhiteSpace(bookname))
            {
                model.BName = bookname;
            }
            if (!string.IsNullOrWhiteSpace(startdatetime))
            {
                model.StartApplyTime = DateTime.Parse(startdatetime);
            }
            if (!string.IsNullOrWhiteSpace(enddatetime))
            {
                model.EndApplyTime = DateTime.Parse(enddatetime);
            }
            if (!string.IsNullOrWhiteSpace(ApplyStatue))
            {
                model.BStatue = Int32.Parse(ApplyStatue);
            }
            if (!string.IsNullOrWhiteSpace(pageindex))
            {
                model.pageindex = Int32.Parse(pageindex);
            }
            if (!string.IsNullOrWhiteSpace(pagesize))
            {
                model.pagesize = Int32.Parse(pagesize);
            }
            List<BookModel> list = new List<BookModel>();
            int number = 0;
            list = bll.SelectApplyList(model, out number); 
            return Json(new { List= list,Total = number } , JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 作者封杀列表
        /// </summary>
        /// <returns></returns>
        public ViewResult BanList()
        {
            return View();
        }
        /// <summary>
        /// 主编推荐编辑界面
        /// </summary>
        /// <returns></returns>
        public ViewResult RecommendList()
        {
            return View();
        }
        /// <summary>
        /// 查询 作者封杀 数据
        /// </summary>
        /// <param name="account"></param>
        /// <param name="startdatetime"></param>
        /// <param name="enddatetime"></param>
        /// <param name="ApplyStatue"></param>
        /// <returns></returns>
        public JsonResult FindBan(string Writername, string startdatetime, string enddatetime, string ReportStatue, string pageindex, string pagesize)
        {
            ReportModel model = new ReportModel();
            if (!string.IsNullOrWhiteSpace(Writername))
            {
                model.ReportName = Writername;
            }
            if (!string.IsNullOrWhiteSpace(startdatetime))
            {
                model.StartReportTime= DateTime.Parse(startdatetime);
            }
            if (!string.IsNullOrWhiteSpace(enddatetime))
            {
                model.EndReportTime = DateTime.Parse(enddatetime);
            }
            if (!string.IsNullOrWhiteSpace(ReportStatue))
            {
                model.UseStatue = Int32.Parse(ReportStatue);
            }
            if (!string.IsNullOrWhiteSpace(pageindex))
            {
                model.pageindex = Int32.Parse(pageindex);
            }
            if (!string.IsNullOrWhiteSpace(pagesize))
            {
                model.pagesize = Int32.Parse(pagesize);
            }
            List<ReportModel> list = new List<ReportModel>();
            int number = 0;
            list = bll.SelectBanList(model, out number);
            return Json( new  {List= list,Toatl = number } , JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 更新   申请通过   
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public JsonResult Adopt(string account)
        {
            bool IsSuccess = false;
            if (!string.IsNullOrWhiteSpace(account))
            {
                if (bll.UpdateAdoptData(account))
                {
                    IsSuccess = true;
                    return Json(new { IsSuccess = IsSuccess }, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(new { IsSuccess = IsSuccess }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 申请驳回  更新
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public JsonResult Reject(string account)
        {
            bool IsSuccess = false;
            if (!string.IsNullOrWhiteSpace(account))
            {
                if (bll.UpdateAdoptData(account))
                {
                    IsSuccess = true;
                    return Json(new { IsSuccess = IsSuccess }, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(new { IsSuccess = IsSuccess }, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 书籍申请成功 更新book表
        /// </summary>
        /// <param name="BookID"></param>
        /// <returns></returns>
        public JsonResult BookAdopt(string BookID)
        {
            bool IsSuccess = false;
            BookModel model = new BookModel();
            if (!string.IsNullOrWhiteSpace(BookID))
            {
                model.BBookID = Int64.Parse(BookID);
            }
            model.BStatue = 2;
            IsSuccess = bll.UpdateBookApply(model);
            return Json(IsSuccess, JsonRequestBehavior.DenyGet);
        }
        /// <summary>
        /// 书籍申请驳回 更新book表
        /// </summary>
        /// <param name="BookID"></param>
        /// <returns></returns>
        public JsonResult BookReject(string BookID)
        {
            bool IsSuccess = false;
            BookModel model = new BookModel();
            if (!string.IsNullOrWhiteSpace(BookID))
            {
                model.BBookID = Int64.Parse(BookID);
            }
            model.BStatue = 3;
            IsSuccess = bll.UpdateBookApply(model);
            return Json(IsSuccess, JsonRequestBehavior.DenyGet);
        }
        /// <summary>
        /// 封杀作者成功
        /// </summary>
        /// <param name="ReportID"></param>
        /// <returns></returns>
        public JsonResult ReportAdopt(string ReportID)
        {
            bool IsSuccess = false;
            ReportModel model = new ReportModel();
            if (!string.IsNullOrWhiteSpace(ReportID))
            {
                model.ReportID = Int64.Parse(ReportID);
            }
            model.UseStatue = 3;
            IsSuccess = bll.BanUpdate(model);
            return Json(IsSuccess, JsonRequestBehavior.DenyGet);
        }
        /// <summary>
        /// 封杀作者 忽略
        /// </summary>
        /// <param name="ReportID"></param>
        /// <returns></returns>
        public JsonResult ReportIgnore(string ReportID)
        {
            bool IsSuccess = false;
            ReportModel model = new ReportModel();
            if (!string.IsNullOrWhiteSpace(ReportID))
            {
                model.ReportID = Int64.Parse(ReportID);
            }
            model.UseStatue = 1;
            IsSuccess = bll.BanUpdate(model);
            return Json(IsSuccess, JsonRequestBehavior.DenyGet); 
        }
        /// <summary>
        /// 更新主编推荐表
        /// </summary>
        /// <param name="bookid"></param>
        /// <param name="myid"></param>
        /// <returns></returns>
        public JsonResult UpdateRecommend(string bookid, string myid)
        {
            BookBLL bbll = new BookBLL();
            Recommend model = new Recommend();
            bool IsSuccess = false;
            if (!string.IsNullOrWhiteSpace(bookid))
            {
                model.BookId = Int64.Parse(bookid);
            }
            if (!string.IsNullOrWhiteSpace(myid))
            {
                model.ID = Int64.Parse(myid);
            }
            IsSuccess = bbll.UpdateRecommend(model);

            return Json(IsSuccess, JsonRequestBehavior.DenyGet);

        }
        /// <summary>
        /// 获取推荐表
        /// </summary>
        /// <returns></returns>
        public JsonResult SelectRecommenddata()
        {
            BookBLL bbll = new BookBLL();
            List<Recommend> list = new List<Recommend>();
            list = bbll.SelectRecommenddata();
            return Json(list, JsonRequestBehavior.DenyGet);
        }

    }
}