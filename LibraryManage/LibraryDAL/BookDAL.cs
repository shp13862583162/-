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
    /// 书籍数据层
    /// </summary>
    public class BookDAL
    {
        string dbname = "mytest";
        /// <summary>
        /// 根据数据类型ID查取类型名称
        /// </summary>
        /// <param name="BookTypeId"></param>
        /// <returns></returns>
        public DataTable FindTypeNameFromId(long BookTypeId)
        {
            List<SqlParameter> paras = new List<SqlParameter>();
            string str = " select BTName from BookType WITH(NOLOCK)  where IsValid = 0 and IsDeleted = 0  ";
            if (BookTypeId != 0)
            {
                paras.Add(new SqlParameter("@BTId", SqlDbType.BigInt) { Value = BookTypeId });
                str += "and BTId = @BTId";
            }
            DataTable result = CommonMethod.SelectData(str, paras.ToArray(), dbname);
            return result;
        }
        /// <summary>
        /// 获取所有 的书籍类型
        /// </summary>
        /// <returns></returns>
        public  DataTable SelectBookTypeRank()
        {
            DataTable table = new DataTable();
            string sql = @"select  BTName,BTId from mytest.dbo.BookType WITH(NOLOCK) 
                where  IsValid = 0 and IsDeleted = 0
                order by BTVisitCount desc  ";
            List<SqlParameter> paras = new List<SqlParameter>();

            DataTable result = CommonMethod.SelectData(sql, paras.ToArray(), dbname);
            return result;
        }
        /// <summary>
        /// 获取指定书籍类型的前10本书籍
        /// </summary>
        /// <returns></returns>
        public DataTable SelectBookRank(string booktypeid, string PageIndex, string PageSize)
        {
            DataTable table = new DataTable();
            int pageindex =0 ;
            int pagesize = 0;
            if (!string.IsNullOrWhiteSpace(PageIndex))
            {
                pageindex = Int32.Parse(PageIndex);
            }
            if (!string.IsNullOrWhiteSpace(PageSize))
            {
                pagesize = Int32.Parse(PageSize);
            }
            StringBuilder str = new StringBuilder();
            string wherestr = "";
            string sql = "";
            str.Append(@"select top {0}  BBookID,BName,BVisitCount,Writer,BIntroduction
                from(
                select Row_Number() over (order by  b.BBookID asc ) as number , b.BBookID,b.BName,b.BVisitCount,b.Writer,b.BIntroduction
                from mytest.dbo.Book b WITH(NOLOCK) 
                left join mytest.dbo.UserInfo u WITH(NOLOCK)  on b.WriterID = u.Account
                where 1=1 and u.UseStatue != 3 and b.BStatue = 2  and  b.IsValid = 0 and b.IsDeleted = 0 and  u.IsValid = 0 and u.IsDeleted = 0 {1} ) as temp where temp.number >{2}
                  ");
            List<SqlParameter> paras = new List<SqlParameter>();
            if (! string.IsNullOrWhiteSpace(booktypeid))
            {
                paras.Add(new SqlParameter("@BTypeID", SqlDbType.BigInt) { Value = Int64.Parse(booktypeid) });
                wherestr = "and b.BTypeID = @BTypeID ";
            }
            sql = string.Format(str.ToString(), pagesize, wherestr, (pageindex - 1) * pagesize);
            DataTable result = CommonMethod.SelectData(sql.ToString(), paras.ToArray(), dbname);
            return result;
        }
        /// <summary>
        /// 获取指定书籍类型的前10本书籍
        /// </summary>
        /// <returns></returns>
        public DataTable SelectTypeBook(string booktypeid)
        {
            DataTable table = new DataTable();
            StringBuilder sql = new StringBuilder();
            sql.Append(@"select b.BBookID,b.BName
                from mytest.dbo.Book b  WITH(NOLOCK) 
                left join mytest.dbo.UserInfo u WITH(NOLOCK)  on b.WriterID = u.Account
                where 1=1 and u.UseStatue != 3 and b.BStatue = 2  and  b.IsValid = 0 and b.IsDeleted = 0 and  u.IsValid = 0 and u.IsDeleted = 0 
                  ");
            List<SqlParameter> paras = new List<SqlParameter>();
            if (!string.IsNullOrWhiteSpace(booktypeid))
            {
                paras.Add(new SqlParameter("@BTypeID", SqlDbType.BigInt) { Value = Int64.Parse(booktypeid) });
                sql.AppendFormat("and b.BTypeID = @BTypeID ");
            }
            DataTable result = CommonMethod.SelectData(sql.ToString(), paras.ToArray(), dbname);
            return result;
        }
        /// <summary>
        /// 搜索前十的书籍
        /// </summary>
        /// <returns></returns>
        public DataTable SelectBook()
        {
            DataTable table = new DataTable();
            StringBuilder sqlwhere = new StringBuilder();
            string sql = @"select top 10 b.BBookID,b.BName 
                from mytest.dbo.Book  b WITH(NOLOCK) 
                left join mytest.dbo.UserInfo u WITH(NOLOCK)  on b.WriterID = u.Account
                where 1=1 and u.UseStatue != 3 and b.BStatue = 2   and  b.IsValid = 0 and b.IsDeleted = 0 and  u.IsValid = 0 and u.IsDeleted = 0 
                  ";
            List<SqlParameter> paras = new List<SqlParameter>();
            
            DataTable result = CommonMethod.SelectData(sql, paras.ToArray(), dbname);
            return result;
        }
        /// <summary>
        /// 搜索指定ID的书籍
        /// </summary>
        /// <param name="bookid"></param>
        /// <returns></returns>
        public DataTable SelectBookDetail(long bookid)
        {
            DataTable table = new DataTable();
            List<SqlParameter> paras = new List<SqlParameter>();
            StringBuilder sqlwhere = new StringBuilder();
            string sql = @"select top 10 *
                from mytest.dbo.Book WITH(NOLOCK) 
                where 1=1 and BBookID = @BBookID  and BStatue = 2  and  IsValid = 0 and IsDeleted = 0
                  ";
            if (bookid != 0) {
                paras.Add(new SqlParameter("@BBookID", SqlDbType.BigInt) { Value = bookid });
            }
            DataTable result = CommonMethod.SelectData(sql, paras.ToArray(), dbname);
            return result;
        }
        /// <summary>
        /// 查询书籍目录
        /// </summary>
        /// <param name="bookid"></param>
        /// <returns></returns>
        public DataTable SelectList(long bookid)
        {
            DataTable table = new DataTable();
            List<SqlParameter> paras = new List<SqlParameter>();
            StringBuilder sqlwhere = new StringBuilder();
            string sql = @"select CHID,CHName
                from mytest.dbo.Chapter WITH(NOLOCK) 
                where 1=1 and BookID = @BookID  and  IsValid = 0 and IsDeleted = 0
                  ";
            if (bookid != 0)
            {
                paras.Add(new SqlParameter("@BookID", SqlDbType.BigInt) { Value = bookid });
            }
            DataTable result = CommonMethod.SelectData(sql, paras.ToArray(), dbname);
            return result;
        }
        /// <summary>
        /// 获取章节内容
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public DataTable ChapterContent(ChapterModel model)
        {
            DataTable table = new DataTable();
            List<SqlParameter> paras = new List<SqlParameter>();
            StringBuilder sqlwhere = new StringBuilder();
            string sql = @"select  CHContent,CHName,CHID,BookID
                from mytest.dbo.Chapter WITH(NOLOCK) 
                where 1=1 and BookID = @BookID and CHID = @CHID  and  IsValid = 0 and IsDeleted = 0
                  ";
            if (model.CHID != 0)
            {
                paras.Add(new SqlParameter("@CHID", SqlDbType.BigInt) { Value = model.CHID });
            }
            if (model.BookID != 0)
            {
                paras.Add(new SqlParameter("@BookID", SqlDbType.BigInt) { Value = model.BookID });
            }
            DataTable result = CommonMethod.SelectData(sql, paras.ToArray(), dbname);
            return result;
        }
        /// <summary>
        /// 获取章节最小最大的ID
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public DataTable ChapterminMaxID(ChapterModel model)
        {
            DataTable table = new DataTable();
            List<SqlParameter> paras = new List<SqlParameter>();
            StringBuilder sqlwhere = new StringBuilder();
            string sql = @"select min(CHID),MAX(CHID)
                from mytest.dbo.Chapter WITH(NOLOCK) 
                where 1=1 and BookID = @BookID  and  IsValid = 0 and IsDeleted = 0
                  ";
            if (model.CHID != 0)
            {
                paras.Add(new SqlParameter("@CHID", SqlDbType.BigInt) { Value = model.CHID });
            }
            if (model.BookID != 0)
            {
                paras.Add(new SqlParameter("@BookID", SqlDbType.BigInt) { Value = model.BookID });
            }
            DataTable result = CommonMethod.SelectData(sql, paras.ToArray(), dbname);
            return result;
        }
        /// <summary>
        /// 添加收藏
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int AddCollection(BookCollectionModel model)
        {
            int result = 0;
            List<SqlParameter> paras = new List<SqlParameter>();
            string sql = " insert into mytest.dbo.Collection (BookID,Account,Optime,IsValid,IsDeleted) values(@BookID,@Account,@Optime,0,0)  ";
            if (model.Account != 0)
            {
                paras.Add(new SqlParameter("@Account", SqlDbType.BigInt) { Value = model.Account });
            }
            if (model.BookID != 0)
            {
                paras.Add(new SqlParameter("@BookID", SqlDbType.BigInt) { Value = model.BookID });
            }
            paras.Add(new SqlParameter("@Optime", SqlDbType.DateTime) { Value = DateTime.Now });
            return result = CommonMethod.AddData(sql, paras.ToArray(), dbname);
        }
        /// <summary>
        /// 判断是否已经收藏
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int FindCollect(BookCollectionModel model)
        {
            List<SqlParameter> paras = new List<SqlParameter>();
            string sql = " select count(1) from mytest.dbo.Collection where IsValid = 0 and IsDeleted = 0  ";
            if (model.Account != 0)
            {
                sql += " and Account = @Account";
                paras.Add(new SqlParameter("@Account", SqlDbType.BigInt) { Value = model.Account });
            }
            if (model.BookID != 0)
            {
                sql += " and BookID = @BookID";
                paras.Add(new SqlParameter("@BookID", SqlDbType.BigInt) { Value = model.BookID });
            }
            DataTable table = CommonMethod.SelectData(sql, paras.ToArray(), dbname);
            int result = Int32.Parse(table.Rows[0][0].ToString());
            return result;
        }
        /// <summary>
        /// 通过点击更新书籍的点击个数
        /// </summary>
        /// <param name="bookid"></param>
        /// <returns></returns>
        public int UpdateVisisNum(string bookid,int newnum)
        {
            int result = 0;
            List<SqlParameter> paras = new List<SqlParameter>();
            string sql = " Update mytest.dbo.Book set BVisitCount = @BVisitCount where IsDeleted = 0 and IsValid = 0  ";
         
            if (!string.IsNullOrWhiteSpace(bookid))
            {
                paras.Add(new SqlParameter("@BBookID", SqlDbType.BigInt) { Value = Int64.Parse(bookid) });
                sql += "and BBookID = @BBookID ";
            }
            if (newnum != 0)
            {
                paras.Add(new SqlParameter("@BVisitCount", SqlDbType.Int) { Value = newnum});
            }
            return result = CommonMethod.UpdateData(sql, paras.ToArray(), dbname);

        }
        /// <summary>
        /// 通过点击更新书籍类型的点击个数
        /// </summary>
        /// <param name="bookid"></param>
        /// <returns></returns>
        public int UpdateBookTypeVisisNum(long booktypeid, int newtypenum)
        {
            int result = 0;
            List<SqlParameter> paras = new List<SqlParameter>();
            string sql = " Update mytest.dbo.BookType set BTVisitCount = @BTVisitCount where IsDeleted = 0 and IsValid = 0  ";

            if (booktypeid!=0)
            {
                paras.Add(new SqlParameter("@BTId", SqlDbType.BigInt) { Value = booktypeid });
                sql += "and BTId = @BTId ";
            }
            if (newtypenum != 0)
            {
                paras.Add(new SqlParameter("@BTVisitCount", SqlDbType.Int) { Value = newtypenum });
            }
            return result = CommonMethod.UpdateData(sql, paras.ToArray(), dbname);

        }
        /// <summary>
        /// 获取指定书籍的阅读数量
        /// </summary>
        /// <param name="bookid"></param>
        /// <returns></returns>
        public DataTable FindBookVisNum(string bookid)
        {
            List<SqlParameter> paras = new List<SqlParameter>();
            string sql = " select BVisitCount,BTypeID from  mytest.dbo.Book  where IsDeleted = 0 and IsValid = 0  ";

            if (!string.IsNullOrWhiteSpace(bookid))
            {
                paras.Add(new SqlParameter("@BBookID", SqlDbType.BigInt) { Value = Int64.Parse(bookid) });
                sql += "and BBookID = @BBookID ";
            }
            DataTable result = CommonMethod.SelectData(sql, paras.ToArray(), dbname);
            return result;
        }
        /// <summary>
        /// 获取指定书籍类型的阅读数量
        /// </summary>
        /// <param name="bookid"></param>
        /// <returns></returns>
        public DataTable FindBooTypekVisNum(long booktypeid)
        {
            List<SqlParameter> paras = new List<SqlParameter>();
            string sql = " select BTVisitCount from  mytest.dbo.BookType  where IsDeleted = 0 and IsValid = 0  ";

            if (booktypeid!=0)
            {
                paras.Add(new SqlParameter("@BTId", SqlDbType.BigInt) { Value = booktypeid });
                sql += "and BTId = @BTId ";
            }
            DataTable result = CommonMethod.SelectData(sql, paras.ToArray(), dbname);
            return result;
        }
        /// <summary>
        /// 举报作者，添加投诉表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int AddReport(ReportModel model)
        {
            int result = 0;
            List<SqlParameter> paras = new List<SqlParameter>();
            string sql = @" INSERT INTO [dbo].[Report]
                                   (
                                   [ReportID]
                                   ,[Complainant]
                                   ,[ReportTime]
                                   ,[ReportReason]
                                   ,[IsValid]
                                   ,[IsDeleted])
                             VALUES 
                                    (@ReportID,@Complainant,@ReportTime,@ReportReason,0,0)
";
            if (model.ReportID != 0)
            {
                paras.Add(new SqlParameter("@ReportID", SqlDbType.BigInt) { Value = model.ReportID });
            }
            if (model.Complainant!=0)
            {
                paras.Add(new SqlParameter("@Complainant", SqlDbType.BigInt) { Value = model.Complainant });
            }
            if (!string.IsNullOrWhiteSpace(model.ReportReason))
            {
                paras.Add(new SqlParameter("@ReportReason", SqlDbType.NVarChar) { Value = model.ReportReason });
            }
            if (model.ReportTime != DateTime.Parse("1900-01-01"))
            {
                paras.Add(new SqlParameter("@ReportTime", SqlDbType.DateTime) { Value = model.ReportTime });
            }
            return result = CommonMethod.AddData(sql, paras.ToArray(), dbname);
        }
        /// <summary>
        ///添加点评表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int AddComment(CommentModel model)
        {
            int result = 0;
            List<SqlParameter> paras = new List<SqlParameter>();
            string sql = @" INSERT INTO [dbo].[Comment]
                                    ([ACCount]
                                    ,[BookID]
                                    ,[BContent]
                                    ,[CTime]
                                    ,[IsValid]
                                    ,[IsDeleted])
                              VALUES
                                    (@ACCount,@BookID,@BContent,@CTime,0,0)
";
            if (model.ACCount != 0)
            {
                paras.Add(new SqlParameter("@ACCount", SqlDbType.BigInt) { Value = model.ACCount });
            }
            if (model.BookID != 0)
            {
                paras.Add(new SqlParameter("@BookID", SqlDbType.BigInt) { Value = model.BookID });
            }
            if (!string.IsNullOrWhiteSpace(model.BContent))
            {
                paras.Add(new SqlParameter("@BContent", SqlDbType.NVarChar) { Value = model.BContent });
            }
            if (model.CTime != DateTime.Parse("1900-01-01"))
            {
                paras.Add(new SqlParameter("@CTime", SqlDbType.DateTime) { Value = model.CTime });
            }
            return result = CommonMethod.AddData(sql, paras.ToArray(), dbname);
        }
        public DataTable FindBookList(BookModel model)
        {
            List<SqlParameter> paras = new List<SqlParameter>();
            StringBuilder build = new StringBuilder();
            build.Append(@" SELECT [ID]
                              ,[BBookID]
                              ,[BName]
                              ,[WriterID]
                              ,[Writer]
                              ,[BTypeID]
                              ,[BIntroduction]
                              ,[BVisitCount]
                              ,[BStatue]
                              ,[ApplyTime]
                              ,[IsValid]
                              ,[IsDeleted]
                          FROM [dbo].[Book] 
                            where 1=1
                            ");
            if (!string.IsNullOrWhiteSpace(model.BName))
            {
                build.Append(" and BName = @BName ");
                paras.Add(new SqlParameter("@BName", SqlDbType.NVarChar) { Value = model.BName });
            }
            if (!string.IsNullOrWhiteSpace(model.Writer))
            {
                build.Append(" and Writer = @Writer ");
                paras.Add(new SqlParameter("@Writer", SqlDbType.NVarChar) { Value = model.Writer });
            }
            DataTable table = CommonMethod.SelectData(build.ToString(), paras.ToArray(), dbname);
            return table;
        }
        /// <summary>
        /// 获取书籍的10条点评
        /// </summary>
        /// <param name="bookid"></param>
        /// <returns></returns>
        public DataTable FindComment(long bookid,int pageindex,int pagesize,out int num)
        {
            List<SqlParameter> paras = new List<SqlParameter>();
            StringBuilder build = new StringBuilder();
            string wherestr = "";
            build.Append(@" 
                            select count(1) 
                                from [mytest].[dbo].[Comment] 
                                where 1=1 AND IsValid = 0 AND IsDeleted = 0 {1} 

                            select top {0} *  from (
                                SELECT  Row_Number() over (order by  ID asc) as number 
                                 ,[ID]
                                 ,[ACCount]
                                 ,[BookID]
                                 ,[BContent]
                                 ,[CTime]
                                 ,[IsValid]
                                 ,[IsDeleted]
                             FROM [mytest].[dbo].[Comment]
                            where 1=1 AND IsValid = 0 AND IsDeleted = 0 {1} ) as temp
                             where temp.number>{2}
                            ");
           
            if (bookid!=0)
            {
                wherestr += " and BookID = @BookID ";
                paras.Add(new SqlParameter("@BookID", SqlDbType.BigInt) { Value =bookid});
            }
            string sql = string.Format(build.ToString(), pagesize, wherestr.ToString(), (pageindex - 1) * pagesize);
            DataTableCollection tables = CommonMethod.GetDataSet(sql.ToString(), paras.ToArray(), dbname);
            num = Int32.Parse(tables[0].Rows[0][0].ToString());
            return tables[1];
        }
        /// <summary>
        /// 查询推荐表
        /// </summary>
        /// <returns></returns>
        public DataTable FindRecommend()
        {
            List<SqlParameter> paras = new List<SqlParameter>();
            string sql = @"  
                            SELECT top 8
                                  b.BBookID,b.BName,b.PictureUrl
                              FROM[dbo].[Recommend]
                                    r
                             left join[dbo].[Book]
                                    b on r.BookId = b.BBookID
                            where r.IsValid= 0 and r.IsDeleted = 0 and b.IsValid= 0 and b.IsDeleted = 0 
                        ";
            DataTable table = CommonMethod.SelectData(sql.ToString(), paras.ToArray(), dbname);
            return table;
        }
        /// <summary>
        /// 更新推荐表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int UpdateRecommend(Recommend model)
        {
            List<SqlParameter> paras = new List<SqlParameter>();
            string sql = @"  
                           UPDATE [dbo].[Recommend]
                                SET [BookId] = @BookId
                           WHERE  IsValid= 0 and IsDeleted = 0 and ID = @ID
                        ";
            if (model.ID != 0)
            {
                paras.Add(new SqlParameter("@ID", SqlDbType.BigInt) { Value = model.ID });
            }
            if (model.BookId != 0)
            {
                paras.Add(new SqlParameter("@BookId", SqlDbType.BigInt) { Value = model.BookId });
            }
            int result = CommonMethod.UpdateData(sql.ToString(), paras.ToArray(), dbname);
            return result;
        }
        /// <summary>
        /// 获取Recommenddata
        /// </summary>
        /// <returns></returns>
        public DataTable SelectRecommenddata()
        {
            List<SqlParameter> paras = new List<SqlParameter>();
            string sql = @"  
                           select top 8 * from [dbo].[Recommend]
                           WHERE  IsValid= 0 and IsDeleted = 0 
                        ";
            DataTable table = CommonMethod.SelectData(sql.ToString(), paras.ToArray(), dbname);
            return table;
        }
        /// <summary>
        /// 获取书籍的章节
        /// </summary>
        /// <param name="bookid"></param>
        /// <returns></returns>
        //public DataTable FindChapter(long bookid)
        //{
        //    string sql = @"SELECT [ID]
        //                  ,[CHID]
        //                  ,[CHName]
        //                  ,[CHContent]
        //                  ,[BookID]
        //                  ,[IsValid]
        //                  ,[IsDeleted]
        //              FROM[dbo].[Chapter]
        //             where BookID = @BookID and IsValid = 0 and IsDeleted = 0
        //            ";
        //    List<SqlParameter> paras = new List<SqlParameter>();
        //    if (bookid!=0)
        //    {
        //        paras.Add(new SqlParameter("@BookID", SqlDbType.BigInt) { Value = bookid });
        //    }
        //    DataTable result = CommonMethod.SelectData(sql, paras.ToArray(), dbname);
        //    return result;
        //}
    }
}
