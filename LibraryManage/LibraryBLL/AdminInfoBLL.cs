using LibraryDAL;
using LibraryModel.Book;
using LibraryModel.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryBLL
{
    /// <summary>
    /// 管理员   逻辑
    /// </summary>
    public class AdminInfoBLL
    {
        AdminInfoDAL dal = new AdminInfoDAL();
        /// <summary>
        /// 查询晋升表数据 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public List<PromotionModel> SelectPromotionList(PromotionModel model,out int number)
        {
            List<PromotionModel> list = new List<PromotionModel>();
            list = Common.TableToList<PromotionModel>(dal.SelectPromotionList(model,out number));
            return list;
        }
        /// <summary>
        /// 查询晋升表数据 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public List<BookModel> SelectApplyList(BookModel model, out int number)
        {
            List<BookModel> list = new List<BookModel>();
            list = Common.TableToList<BookModel>(dal.SelectApplyList(model, out number));
            return list;
        }
        /// <summary>
        /// 查询晋升表数据 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public List<ReportModel> SelectBanList(ReportModel model, out int number)
        {
            List<ReportModel> list = new List<ReportModel>();
            list = Common.TableToList<ReportModel>(dal.SelectBanList(model, out number));
            return list;
        }
        /// <summary>
        /// 申请成功  更新申请表和用户表
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public bool UpdateAdoptData(string account)
        {
            int result1 = dal.UpdateAdopt(account);
            int result2 = dal.UpdateAdoptUser(account);
            if (result1 > 0 && result2 > 0)
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// 申请成功  更新申请表和用户表
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public bool UpdateRejectData(string account)
        {
            int result = dal.UpdateReject(account);
            if (result > 0 )
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// 更新 Book表 （申请）
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool UpdateBookApply(BookModel model)
        {
            int result = dal.UpdateBookApply(model);
            if (result > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 封杀 逻辑
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool BanUpdate(ReportModel model)
        {
            int result1 = dal.ReportUpdateReport(model);
            int result2 = dal.ReportUpdateUserInfo(model);
            if (result1 > 0 && result2 > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
