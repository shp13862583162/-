using LibraryDAL.Common;
using LibraryModel;
using LibraryModel.Book;
using LibraryModel.User;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryDAL
{
    /// <summary>
    /// 用户   数据层
    /// </summary>
    public class UserInfoDAL
    {
        string dbname = "mytest";
        /// <summary>
        /// 用户验证登录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public DataTable CheckUser(LoginInfoModel model)
        {
            DataTable table = new DataTable();
           
            string sql = "select count(1) from mytest.dbo.UserInfo WITH(NOLOCK)  where 1=1  ";
            StringBuilder sqlwhere = new StringBuilder();
            List<SqlParameter> paras = new List<SqlParameter>();
            if (model.Account != 0)
            {
                paras.Add(new SqlParameter("@UserAccount", SqlDbType.BigInt) { Value = model.Account });
                sqlwhere.AppendFormat("and Account = @UserAccount ");
            }
            if (!string.IsNullOrWhiteSpace(model.Password))
            {
                paras.Add(new SqlParameter("@UserPassword", SqlDbType.Char, 50) { Value = model.Password });
                sqlwhere.AppendFormat("and Password = @UserPassword ");
            }
            sql = sql + sqlwhere.ToString()+ "and UseStatue !=3 and IsValid= 0 and IsDeleted = 0";
            DataTable result = CommonMethod.SelectData(sql,paras.ToArray(),dbname);
            return result;
        }
        /// <summary>
        /// 获取登录这的详细信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public DataTable SelectUser(LoginInfoModel model)
        {
            DataTable table = new DataTable();

            string sql = "select Name,Account,Password,Sex,Type from mytest.dbo.UserInfo WITH(NOLOCK)  where 1=1  ";
            StringBuilder sqlwhere = new StringBuilder();
            List<SqlParameter> paras = new List<SqlParameter>();
            if (model.Account != 0)
            {
                paras.Add(new SqlParameter("@UserAccount", SqlDbType.BigInt) { Value = model.Account });
                sqlwhere.AppendFormat("and Account = @UserAccount ");
            }
            sql = sql + sqlwhere.ToString() + "and UseStatue!=3 and IsValid= 0 and IsDeleted = 0";
            DataTable result = CommonMethod.SelectData(sql, paras.ToArray(), dbname);
            return result;
        }
        /// <summary>
        /// 添加管理员
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int AddAdmin(RegisterModel model)
        {
            int result = 0;
            List<SqlParameter> paras = new List<SqlParameter>();
            if (!string.IsNullOrWhiteSpace(model.username))
            {
                paras.Add(new SqlParameter("@ADName", SqlDbType.Char,20) { Value = model.username });
            }
            if (model.account!=0)
            {
                paras.Add(new SqlParameter("@ADID", SqlDbType.BigInt) { Value = model.account });
            }
            string sql =string.Format("insert into mytest.dbo.Admin (ADID,ADName,IsValid,IsDeleted) values (@ADID,@ADName,0,0) ");
            return result = CommonMethod.AddData(sql, paras.ToArray(), dbname);
        }
        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int AddUser(RegisterModel model)
        {
            int result = 0;
            List<SqlParameter> paras = new List<SqlParameter>();
            if (model.account != 0)
            {
                paras.Add(new SqlParameter("@Account", SqlDbType.BigInt) { Value = model.account });
            }
            if (!string.IsNullOrWhiteSpace(model.username))
            {
                paras.Add(new SqlParameter("@Name", SqlDbType.Char, 20) { Value = model.username });
            }
            if (!string.IsNullOrWhiteSpace(model.pwd))
            {
                paras.Add(new SqlParameter("@Password", SqlDbType.Char, 50) { Value = model.pwd });
            }
            if (model.sex != -1)
            {
                paras.Add(new SqlParameter("@Sex", SqlDbType.Int) { Value = model.sex });
            }
            if (model.usertype != 0)
            {
                paras.Add(new SqlParameter("@Type", SqlDbType.Int) { Value = model.usertype });
            }
            string sql = string.Format("insert into mytest.dbo.UserInfo (Account,Name,Password,Sex,Type,UseStatue,IsValid,IsDeleted) values (@Account,@Name,@Password,@Sex,@Type,1,0,0) ");
            return result = CommonMethod.AddData(sql, paras.ToArray(), dbname);
        }
        /// <summary>
        /// 获取数据库里个人的数据  非读者
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public DataTable GetSelfInfo(UserModel model)
        {
            DataTable table = new DataTable();

            string sql = @"select Name,Account,Sex,Type from mytest.dbo.UserInfo WITH(NOLOCK)  
                    where 1=1  ";
            StringBuilder sqlwhere = new StringBuilder();
            List<SqlParameter> paras = new List<SqlParameter>();
            if (model.Account != 0)
            {
                paras.Add(new SqlParameter("@UserAccount", SqlDbType.BigInt) { Value = model.Account });
                sqlwhere.AppendFormat("and Account = @UserAccount ");
            }
            sql = sql + sqlwhere.ToString() + "and IsValid= 0 and IsDeleted = 0 ";
            DataTable result = CommonMethod.SelectData(sql, paras.ToArray(), dbname);
            return result;
        }
        /// <summary>
        /// 获取数据库里个人的数据 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public DataTable GetSelfInfoDetail(UserModel model)
        {
            DataTable table = new DataTable();

            string sql = @"select u.Name,u.Account,u.Sex,u.Type,p.PStatue from mytest.dbo.UserInfo u WITH(NOLOCK) 
                    left join mytest.dbo.Promotion p WITH(NOLOCK)  on u.Account = p.PAccount 
                    where 1=1  ";
            StringBuilder sqlwhere = new StringBuilder();
            List<SqlParameter> paras = new List<SqlParameter>();
            if (model.Account!=0)
            {
                paras.Add(new SqlParameter("@UserAccount", SqlDbType.BigInt) { Value = model.Account });
                sqlwhere.AppendFormat("and u.Account = @UserAccount ");
            }
            sql = sql + sqlwhere.ToString()+ "and u.IsValid= 0 and u.IsDeleted = 0 and p.IsValid= 0 and p.IsDeleted = 0";
            DataTable result = CommonMethod.SelectData(sql, paras.ToArray(), dbname);
            return result;
        }
        /// <summary>
        /// 获取数据库里个人的数据 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public DataTable GetSelfInfoDetail1(UserModel model)
        {
            DataTable table = new DataTable();

            string sql = @"select u.Name,u.Account,u.Sex,u.Type from mytest.dbo.UserInfo u WITH(NOLOCK) 
                    where 1=1  ";
            StringBuilder sqlwhere = new StringBuilder();
            List<SqlParameter> paras = new List<SqlParameter>();
            if (model.Account != 0)
            {
                paras.Add(new SqlParameter("@UserAccount", SqlDbType.BigInt) { Value = model.Account });
                sqlwhere.AppendFormat("and u.Account = @UserAccount ");
            }
            sql = sql + sqlwhere.ToString() + "and u.IsValid= 0 and u.IsDeleted = 0 ";
            DataTable result = CommonMethod.SelectData(sql, paras.ToArray(), dbname);
            return result;
        }
        /// <summary>
        /// 获取个人收藏
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public DataTable GetIntroduction(BookModel model)
        {
            DataTable table = new DataTable();

            string sql = @"select BK.BBookID,BK.BName,BK.BTypeID,BK.BIntroduction,BK.Writer
                from mytest.dbo.Collection  as CT WITH(NOLOCK) 
                left join mytest.dbo.Book BK WITH(NOLOCK)  on CT.BookID = BK.BBookID
                where CT.Account = @Account and CT.IsValid = 0 and CT.IsDeleted = 0 and BK.IsValid = 0 and BK.IsDeleted = 0
                 ";
            StringBuilder sqlwhere = new StringBuilder();
            List<SqlParameter> paras = new List<SqlParameter>();
            if (model.Account != 0)
            {
                paras.Add(new SqlParameter("@Account", SqlDbType.BigInt) { Value = model.Account });
            }
            sql = sql + sqlwhere.ToString();
            DataTable result = CommonMethod.SelectData(sql, paras.ToArray(), dbname);
            return result;
        }
        /// <summary>
        /// 删除收藏
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int ShancIntroduction(BookModel model)
        {
            List<SqlParameter> paras = new List<SqlParameter>();
            string sql = @"UPDATE [dbo].[Collection]
                               SET [IsValid] = 1
                                  ,[Optime] = @Optime
                             WHERE [Account] = @Account
                                  and [BookID] = @BookID AND [IsDeleted] = 0  ";
            if (model.Account != 0)
            {
                paras.Add(new SqlParameter("@Account", SqlDbType.BigInt) { Value = model.Account });
            }
            if (model.BBookID != 0)
            {
                paras.Add(new SqlParameter("@BookID", SqlDbType.BigInt) { Value = model.BBookID });
            }
            paras.Add(new SqlParameter("@Optime", SqlDbType.DateTime) { Value = DateTime.Now});
            int result = CommonMethod.UpdateData(sql, paras.ToArray(), dbname);
            return result;
        }
    }
}

