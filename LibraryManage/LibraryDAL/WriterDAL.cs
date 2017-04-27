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
    /// 作者  数据层
    /// </summary>
    public class WriterDAL
    {
        string dbname = "mytest";
        /// <summary>
        /// 验证 申请读者权限数据 是否存在
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public DataTable SelectPromotionNum(PromotionModel model)
        {
            List<SqlParameter> paras = new List<SqlParameter>();
            string sql = @"select PAccount from mytest.dbo.Promotion WITH(NOLOCK) 
                where PAccount=@PAccount and IsValid = 0 and IsDeleted = 0 and PStatue != 3
                ";
            if (model.PAccount != 0)
            {
                paras.Add(new SqlParameter("@PAccount", SqlDbType.BigInt) { Value = model.PAccount });
            }
           
            DataTable result = CommonMethod.SelectData(sql.ToString(), paras.ToArray(), dbname);
            return result;
        }
        /// <summary>
        /// 添加申请读者权限数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int AddPromotion(PromotionModel model)
        {
            List<SqlParameter> paras = new List<SqlParameter>();
            string sql = @"insert into mytest.dbo.Promotion
                (PAccount,PStatue,PApplyTime)
                values
                (@PAccount,1,@PApplyTime)
                ";
            if (model.PAccount != 0)
            {
                paras.Add(new SqlParameter("@PAccount", SqlDbType.BigInt) { Value = model.PAccount });
            }
            if(model.PApplyTime!= DateTime.Parse("1900-01-01"))
            {
                paras.Add(new SqlParameter("@PApplyTime", SqlDbType.DateTime) { Value = model.PApplyTime });
            }
            int result = CommonMethod.AddData(sql.ToString(), paras.ToArray(), dbname);
            return result;
        }
        /// <summary>
        /// 获取上架书籍 作者
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public DataTableCollection GetShelvesBooks(BookModel model)
        {
            List<SqlParameter> paras = new List<SqlParameter>();
            string sql = "";
            string sqlwhere = "";
            if (model.WriterID != 0)
            {
                paras.Add(new SqlParameter("@WriterID", SqlDbType.BigInt) { Value = model.WriterID });
                sqlwhere += " and  b.WriterID = @WriterID ";
            }
            if (!string.IsNullOrWhiteSpace(model.BName))
            {
                paras.Add(new SqlParameter("@BName", SqlDbType.NVarChar) { Value = model.BName });
                sqlwhere += " and  b.BName = @BName ";
            }
            if(model.StartApplyTime != DateTime.Parse("1900-01-01"))
            {
                paras.Add(new SqlParameter("@StartApplyTime", SqlDbType.DateTime) { Value = model.StartApplyTime });
                sqlwhere += " and  b.ApplyTime > @StartApplyTime ";
            }
            if (model.EndApplyTime != DateTime.Parse("1900-01-01"))
            {
                paras.Add(new SqlParameter("@EndApplyTime", SqlDbType.DateTime) { Value = model.EndApplyTime });
                sqlwhere += " and  b.ApplyTime < @EndApplyTime ";
            }
            if (model.BStatue == 1)
            {
                sqlwhere += " and b.BStatue = 1 ";
            }
            else if (model.BStatue == 2)
            {
                sqlwhere += " and b.BStatue = 2 ";
            }else if (model.BStatue == 3)
            {
                sqlwhere += " and b.BStatue = 3 ";
            }
            else if(model.BStatue == 4)
            {
                sqlwhere += " and b.BStatue != 2 ";
            }
            sql = string.Format(@"
                    select top {0} BBookID,BName,BTypeID,WriterID,Writer,BStatue,ApplyTime
                        from
                        (select  Row_Number() over (order by  b.BBookID asc ) as number ,b.ApplyTime, b.BBookID,b.BName,b.BTypeID,b.WriterID,b.Writer,b.BStatue
                        from mytest.dbo.Book b WITH(NOLOCK) 
                        left join mytest.dbo.UserInfo  u WITH(NOLOCK)   on b.WriterID= u.Account
                        where  u.UseStatue!=3 and b.IsValid = 0 and b.IsDeleted = 0 and u.IsValid = 0 and u.IsDeleted =0 {1})as temp
                        where
                        temp.number >{2}
                    select count(1) from  mytest.dbo.Book b
                        left join mytest.dbo.UserInfo  u  on b.WriterID= u.Account
                        where u.UseStatue!=3 and b.IsValid = 0 and b.IsDeleted = 0 and u.IsValid = 0 and u.IsDeleted =0  {1}
                ", model.pagesize,sqlwhere, (model.pageindex - 1) * model.pagesize);
            DataTableCollection result = CommonMethod.GetDataSet(sql.ToString(), paras.ToArray(), dbname);
            return result;
        }
        /// <summary>
        ///添加书籍申请
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int AddBook(BookModel model)
        {
            List<SqlParameter> paras = new List<SqlParameter>();
            StringBuilder build = new StringBuilder();
            build.Append(@"INSERT INTO [dbo].[Book]
           ([BBookID]
           ,[BName]
            ,[PictureUrl]
           ,[WriterID]
           ,[Writer]
           ,[BTypeID]
           ,[BIntroduction]
           ,[BVisitCount]
           ,[BStatue]
           ,[ApplyTime]
           ,[IsValid]
           ,[IsDeleted])
     VALUES
            (@BBookID,@BName,@PictureUrl,@WriterID,@Writer,@BTypeID,@BIntroduction,0,1,@ApplyTime,0,0)
");
            if (model.BBookID != 0)
            {
                paras.Add(new SqlParameter("@BBookID", SqlDbType.BigInt) { Value = model.BBookID });
            }
            if (!string.IsNullOrWhiteSpace(model.BName))
            {
                paras.Add(new SqlParameter("@BName", SqlDbType.NVarChar) { Value = model.BName });
            }
            if (!string.IsNullOrWhiteSpace(model.PictureUrl))
            {
                paras.Add(new SqlParameter("@PictureUrl", SqlDbType.NVarChar) { Value = model.PictureUrl });
            }
            if (model.WriterID != 0)
            {
                paras.Add(new SqlParameter("@WriterID", SqlDbType.BigInt) { Value = model.WriterID });
            }
            if (!string.IsNullOrWhiteSpace(model.Writer))
            {
                paras.Add(new SqlParameter("@Writer", SqlDbType.NVarChar) { Value = model.Writer });
            }
            if (!string.IsNullOrWhiteSpace(model.BIntroduction))
            {
                paras.Add(new SqlParameter("@BIntroduction", SqlDbType.NVarChar) { Value = model.BIntroduction });
            }
            if (model.ApplyTime!=DateTime.Parse("1900-01-01"))
            {
                paras.Add(new SqlParameter("@ApplyTime", SqlDbType.DateTime) { Value = model.ApplyTime });
            }
            if(model.BTypeID != 0)
            {
                paras.Add(new SqlParameter("@BTypeID", SqlDbType.Int) { Value = model.BTypeID });
            }
            int result = CommonMethod.AddData(build.ToString(), paras.ToArray(), dbname);
            return result;
        }
        /// <summary>
        /// 获取最大的书籍id
        /// </summary>
        /// <returns></returns>
        public long FindMaxBookID()
        {
            List<SqlParameter> paras = new List<SqlParameter>();
            string str = "select max(BBookID) from [dbo].[Book] WITH(NOLOCK) ";
            DataTable table = CommonMethod.SelectData(str, paras.ToArray(), dbname);
            long result;
            if (table == null||table.Rows.Count<=0 || table.Rows[0][0].ToString() == "")
            {
                result = 0;
            }
            else
            {
                result = Int64.Parse(table.Rows[0][0].ToString());
            }
            return result;
        }
        /// <summary>
        /// 获取书籍类型
        /// </summary>
        /// <returns></returns>
        public DataTable FindBookType()
        {
            string sql = "select BTId ,BTName from BookType WITH(NOLOCK) where IsValid = 0 and IsDeleted = 0  ";
            List<SqlParameter> paras = new List<SqlParameter>();
            DataTable table = CommonMethod.SelectData(sql, paras.ToArray(), dbname);
            return table;
        }
        /// <summary>
        /// 获取最大的书籍id
        /// </summary>
        /// <returns></returns>
        public long FindBookMaxChapterID(ChapterModel model)
        {
            List<SqlParameter> paras = new List<SqlParameter>();
            string str = "select max(CHID) from [dbo].[Chapter] WITH(NOLOCK) where BookID = @BookID ";
            if (model.BookID != 0)
            {
                paras.Add(new SqlParameter("@BookID", SqlDbType.BigInt) { Value = model.BookID });
            }
            DataTable table = CommonMethod.SelectData(str, paras.ToArray(), dbname);
            long result;
            if (table == null || table.Rows.Count <= 0||table.Rows[0][0].ToString()=="")
            {
                result = 0;
            }else
            {
                result = Int64.Parse(table.Rows[0][0].ToString());
            }
            
            return result;
        }
        /// <summary>
        /// 添加章节的具体操作
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int AddChapter(ChapterModel model)
        {
            List<SqlParameter> paras = new List<SqlParameter>();
            string sql = @"
INSERT INTO [dbo].[Chapter]
           ([CHID]
           ,[CHName]
           ,[CHContent]
           ,[BookID]
           ,[IsValid]
           ,[IsDeleted])
     VALUES
           (@CHID
           ,@CHName
           ,@CHContent
           ,@BookID
           ,0
           ,0)";
            if (model.CHID != 0)
            {
                paras.Add(new SqlParameter("@CHID", SqlDbType.BigInt) { Value = model.CHID });
            }
            if (!string.IsNullOrWhiteSpace(model.CHName))
            {
                paras.Add(new SqlParameter("@CHName", SqlDbType.NVarChar) { Value = model.CHName });
            }
            if (!string.IsNullOrWhiteSpace(model.CHContent))
            {
                paras.Add(new SqlParameter("@CHContent", SqlDbType.NVarChar) { Value = model.CHContent });
            }
            if (model.BookID != 0)
            {
                paras.Add(new SqlParameter("@BookID", SqlDbType.BigInt) { Value = model.BookID });
            }
            int  result = CommonMethod.AddData(sql, paras.ToArray(), dbname);
            return result;
        }
    }
}
