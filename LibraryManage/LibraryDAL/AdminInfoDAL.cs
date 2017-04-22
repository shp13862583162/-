using LibraryDAL.Common;
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
    /// 管理员查库
    /// </summary>
    public class AdminInfoDAL
    {
        string dbname = "mytest";
        /// <summary>
        /// 数据    晋升表
        /// </summary>
        /// <returns></returns>
        public DataTable SelectPromotionList(PromotionModel model,out int number)
        {
            DataTable table = new DataTable();

            //string numsql = "select count(1) from Promotion p left join UserInfo u on p.PAccount = u.Account  where 1=1 and u.IsValid= 0 and u.IsDeleted = 0 and p.IsValid= 0 and p.IsDeleted = 0   ";
            StringBuilder sqlwhere = new StringBuilder();
            List<SqlParameter> paras = new List<SqlParameter>();
            if (model.PAccount!= 0)
            {
                paras.Add(new SqlParameter("@PAccount", SqlDbType.BigInt) { Value = model.PAccount });
                sqlwhere.AppendFormat("and p.PAccount = @PAccount ");
            }
            if (model.StartTime!= DateTime.Parse("1900-01-01"))
            {
                paras.Add(new SqlParameter("@StartTime", SqlDbType.DateTime) { Value = model.StartTime });
                sqlwhere.AppendFormat("and p.PApplyTime > @StartTime ");
            }
            if (model.EndTime!= DateTime.Parse("1900-01-01"))
            {
                paras.Add(new SqlParameter("@EndTime", SqlDbType.DateTime) { Value = model.EndTime });
                sqlwhere.AppendFormat("and p.PApplyTime < @EndTime ");
            }
            if (model.PStatue!= 0)
            {
                paras.Add(new SqlParameter("@PStatue", SqlDbType.Int) { Value = model.PStatue });
                sqlwhere.AppendFormat("and PStatue = @PStatue ");
            }
            string sql =string.Format(@"
                                select count(1) from Promotion p WITH(NOLOCK)  left join UserInfo u WITH(NOLOCK)  on p.PAccount = u.Account  where 1=1 and u.IsValid= 0 and u.IsDeleted = 0 and p.IsValid= 0 and p.IsDeleted = 0 {1}

                                select top {0} * from (select Row_Number() over (order by  p.PAccount asc) as number ,p.PAccount,p.PStatue,p.PApplyTime,u.Name from Promotion p WITH(NOLOCK) 
                                    left join UserInfo u WITH(NOLOCK)  on p.PAccount = u.Account 
                                    where 1=1 and u.IsValid= 0 and u.IsDeleted = 0 and p.IsValid= 0 and p.IsDeleted = 0   {1} ) t where t.number>{2}  ", model.pagesize ,sqlwhere.ToString(),(model.pageindex-1)*model.pagesize);
            DataTableCollection result = CommonMethod.GetDataSet(sql, paras.ToArray(), dbname);

            number = Int32.Parse(result[0].Rows[0][0].ToString());
            return result[1];
        }
        /// <summary>
        /// 数据    书籍申请
        /// </summary>
        /// <returns></returns>
        public DataTable SelectApplyList(BookModel model, out int number)
        {
            DataTable table = new DataTable();

            //string numsql = "select count(1) from Book where 1=1 and IsValid= 0 and IsDeleted = 0   ";
            StringBuilder sqlwhere = new StringBuilder();
            List<SqlParameter> paras = new List<SqlParameter>();
            if (!string.IsNullOrWhiteSpace(model.BName))
            {
                paras.Add(new SqlParameter("@BName", SqlDbType.BigInt) { Value = model.BName });
                sqlwhere.AppendFormat("and BName = @BName ");
            }
            if (model.StartApplyTime != DateTime.Parse("1900-01-01"))
            {
                paras.Add(new SqlParameter("@StartApplyTime", SqlDbType.DateTime) { Value = model.StartApplyTime });
                sqlwhere.AppendFormat("and ApplyTime > @StartApplyTime ");
            }
            if (model.EndApplyTime != DateTime.Parse("1900-01-01"))
            {
                paras.Add(new SqlParameter("@EndApplyTime", SqlDbType.DateTime) { Value = model.EndApplyTime });
                sqlwhere.AppendFormat("and ApplyTime < @EndApplyTime ");
            }
            if (model.BStatue != 0)
            {
                paras.Add(new SqlParameter("@BStatue", SqlDbType.Int) { Value = model.BStatue });
                sqlwhere.AppendFormat("and BStatue = @BStatue ");
            }
            //numsql = numsql + sqlwhere.ToString() ;
            string sql = string.Format(@"
                    select count(1) from Book WITH(NOLOCK)  where 1=1 and IsValid= 0 and IsDeleted = 0  {1}
                    select top {0} * from (select Row_Number() over (order by  BBookID  asc) as number,BBookID ,WriterID,Writer,BName,BStatue,ApplyTime from Book WITH(NOLOCK)  where 1=1  and IsValid= 0 and IsDeleted = 0 {1} ) t where t.number>{2}  ", model.pagesize, sqlwhere.ToString(), (model.pageindex - 1) * model.pagesize);
            DataTableCollection result = CommonMethod.GetDataSet(sql, paras.ToArray(), dbname);
            DataTable  numtable = result[0];
            number = Int32.Parse(numtable.Rows[0][0].ToString());
            //number = Int32.Parse(CommonMethod.SelectData(numsql, paras.ToArray(), dbname).Rows[0][0].ToString());
            return result[1];
        }
        /// <summary>
        /// 数据    封杀申请
        /// </summary>
        /// <returns></returns>
        public DataTable SelectBanList(ReportModel model, out int number)
        {
            DataTable table = new DataTable();

            
            StringBuilder sqlwhere = new StringBuilder();
            List<SqlParameter> paras = new List<SqlParameter>();
            if (!string.IsNullOrWhiteSpace(model.ReportName))
            {
                paras.Add(new SqlParameter("@ReportName", SqlDbType.BigInt) { Value = model.ReportName });
                sqlwhere.AppendFormat("and rp.ReportName = @ReportName ");
            }
            if (model.StartReportTime != DateTime.Parse("1900-01-01"))
            {
                paras.Add(new SqlParameter("@StartReportTime", SqlDbType.DateTime) { Value = model.StartReportTime });
                sqlwhere.AppendFormat("and rp.ReportTime > @StartReportTime ");
            }
            if (model.EndReportTime != DateTime.Parse("1900-01-01"))
            {
                paras.Add(new SqlParameter("@EndReportTime", SqlDbType.DateTime) { Value = model.EndReportTime });
                sqlwhere.AppendFormat("and rp.ReportTime < @EndReportTime ");
            }
            if (model.UseStatue != 0)
            {
                paras.Add(new SqlParameter("@UseStatue", SqlDbType.Int) { Value = model.UseStatue });
                sqlwhere.AppendFormat("and u.UseStatue = @UseStatue ");
            }
            string sql = string.Format(@"
                            select count(1) from
                                (
                                select ReportNum = sum(1), ReportID = rp.ReportID, ReportName = u.Name
                                from Report rp WITH(NOLOCK) 
                                left
                                join UserInfo u WITH(NOLOCK)  on rp.ReportID = u.Account
                                where u.IsValid = 0 and u.IsDeleted = 0 and rp.IsValid = 0 and rp.IsDeleted = 0 {1}
                                group by  rp.ReportID , u.Name) as temp  

                             select top {0} * from (
                                select Row_Number() over(order by  temp.ReportID asc) as number, temp.* from
                                (
                                select ReportNum = sum(1), ReportID = rp.ReportID, ReportName = u.Name
                                from Report rp WITH(NOLOCK) 
                                left
                                join UserInfo u WITH(NOLOCK)  on rp.ReportID = u.Account
                                where u.IsValid = 0 and u.IsDeleted = 0 and rp.IsValid = 0 and rp.IsDeleted = 0 {1}
                                            group by  rp.ReportID , u.Name
                                ) as temp
                                ) as a
                                where a.number >{2}
                                  ", model.pagesize, sqlwhere.ToString(), (model.pageindex - 1) * model.pagesize);
            DataTableCollection result = CommonMethod.GetDataSet(sql, paras.ToArray(), dbname);

            number = Int32.Parse(result[0].Rows[0][0].ToString());
            return result[1];
        }
        /// <summary>
        /// 申请成功 更新申请表
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public int UpdateAdopt(string account)
        {
            int result = 0;
            string sql = @"update  mytest.dbo.Promotion 
                    set
                    PStatue = 2
                    where PAccount = @PAccount and IsValid = 0 and IsDeleted = 0";
            if (!string.IsNullOrWhiteSpace(account))
            {
                List<SqlParameter> paras = new List<SqlParameter>();
                paras.Add(new SqlParameter("@PAccount", SqlDbType.BigInt) { Value = Int64.Parse(account) });
                result = CommonMethod.UpdateData(sql, paras.ToArray(), dbname);
            }
            return result;
        }
        /// <summary>
        /// 申请成功 更新用户表
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public int UpdateAdoptUser(string account)
        {
            int result = 0;
            string sql = @"update  mytest.dbo.UserInfo 
                    set
                    Type = 2
                    where Account = @Account and IsValid = 0 and IsDeleted = 0";
            if (!string.IsNullOrWhiteSpace(account))
            {
                List<SqlParameter> paras = new List<SqlParameter>();
                paras.Add(new SqlParameter("@Account", SqlDbType.BigInt) { Value = Int64.Parse(account) });
                result = CommonMethod.UpdateData(sql, paras.ToArray(), dbname);
            }
            return result;
        }
        /// <summary>
        /// 申请驳回  更新申请表
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public int UpdateReject(string account)
        {
            int result = 0;
            string sql = @"update  mytest.dbo.Promotion 
                    set
                    PStatue = 3
                    where PAccount = @PAccount and IsValid = 0 and IsDeleted = 0";
            if (!string.IsNullOrWhiteSpace(account))
            {
                List<SqlParameter> paras = new List<SqlParameter>();
                paras.Add(new SqlParameter("@PAccount", SqlDbType.BigInt) { Value = Int64.Parse(account) });
                result = CommonMethod.UpdateData(sql, paras.ToArray(), dbname);
            }
            return result;
        }
        /// <summary>
        /// 更新  book表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int UpdateBookApply(BookModel model)
        {
            int result = 0;
            List<SqlParameter> paras = new List<SqlParameter>();
            string sql = @"update  mytest.dbo.Book 
                    set
                    BStatue = @BStatue
                    where BBookID = @BBookID and IsValid = 0 and IsDeleted = 0";
            if (model.BBookID!=0)
            {
                paras.Add(new SqlParameter("@BBookID", SqlDbType.BigInt) { Value = model.BBookID });
            }
            if (model.BStatue!=0)
            {
                paras.Add(new SqlParameter("@BStatue", SqlDbType.Int) { Value = model.BStatue });
            }
            result = CommonMethod.UpdateData(sql, paras.ToArray(), dbname);
            return result;
        }
        /// <summary>
        /// 封杀 更新用户表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int ReportUpdateUserInfo(ReportModel model)
        {
            int result = 0;
            List<SqlParameter> paras = new List<SqlParameter>();
            string sql = @"update  mytest.dbo.UserInfo
                        set
                        UseStatue = @UseStatue
                        where Account = @Account and IsValid = 0 and IsDeleted = 0";
            if (model.ReportID != 0)
            {
                paras.Add(new SqlParameter("@Account", SqlDbType.BigInt) { Value = model.ReportID });
            }
            if (model.UseStatue != 0)
            {
                paras.Add(new SqlParameter("@UseStatue", SqlDbType.Int) { Value = model.UseStatue });
            }
            result = CommonMethod.UpdateData(sql, paras.ToArray(), dbname);
            return result;
        }
        /// <summary>
        /// 封杀 更新封杀表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int ReportUpdateReport(ReportModel model)
        {
            int result = 0;
            List<SqlParameter> paras = new List<SqlParameter>();
            string sql = @"update  mytest.dbo.Report
                        set
                        IsValid = 1
                        where ReportID = @ReportID and IsValid = 0 and IsDeleted = 0";
            if (model.ReportID != 0)
            {
                paras.Add(new SqlParameter("@ReportID", SqlDbType.BigInt) { Value = model.ReportID });
            }
            result = CommonMethod.UpdateData(sql, paras.ToArray(), dbname);
            return result;
        }
    }
}
