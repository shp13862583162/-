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
    /// 用户    逻辑层
    /// </summary>
    public class UserInfoBLL
    {
        UserInfoDAL dal = new UserInfoDAL();
        /// <summary>
        /// 验证用户登录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool CheckUser(LoginInfoModel model)
        {
            var table = dal.CheckUser(model);
            if (table != null) {
                if (int.Parse(table.Rows[0][0].ToString())>0) {
                    return true;
                }
            }
            return false;
        }
        public List<LoginInfoModel> SelectUser(LoginInfoModel model)
        {
            var table = dal.SelectUser(model);
            if (table != null)
            {
                return Common.TableToList<LoginInfoModel>(table);
            }
            return new List<LoginInfoModel>();
        }
        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool AddUser(RegisterModel model)
        {
            int result1 = 0;
            int result2 = 0;
            result1 = dal.AddUser(model);
            if (result1>0)
            {
                if (model.usertype == 3)
                {
                    result2 = dal.AddAdmin(model);
                    if (result2 > 0)
                    {
                        return true;
                    }
                }
                return true;
            }
            return false;
        }
        /// <summary>
        /// 获取数据库 个人数据 非读者
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public List<UserModel> GetSelfInfo(UserModel model)
        {
            List<UserModel> list = Common.TableToList<UserModel>(dal.GetSelfInfo(model));
            return list;
        }
        /// <summary>
        /// 获取数据库 个人数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public List<UserModel> GetSelfInfoDetail(UserModel model)
        {
            List<UserModel>  list = Common.TableToList<UserModel>(dal.GetSelfInfoDetail(model));
            return list;
        }/// <summary>
         /// 获取数据库 个人数据
         /// </summary>
         /// <param name="model"></param>
         /// <returns></returns>
        public List<UserModel> GetSelfInfoDetail1(UserModel model)
        {
            List<UserModel> list = Common.TableToList<UserModel>(dal.GetSelfInfoDetail1(model));
            return list;
        }
        /// <summary>
        /// 获取个人收藏
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public List<BookModel> GetIntroduction(BookModel model)
        {
            List<BookModel> list = Common.TableToList<BookModel>(dal.GetIntroduction(model));
            return list;
        }
        /// <summary>
        /// 删除收藏
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool ShancIntroduction(BookModel model)
        {
            int result = dal.ShancIntroduction(model);
            if (result > 0)
            {
                return true;
            }
            return false;
        }
    }
}
