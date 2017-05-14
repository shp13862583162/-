using LibraryDAL;
using LibraryModel.Book;
using LibraryModel.User;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryBLL
{
    /// <summary>
    /// 书籍逻辑层
    /// </summary>
    public class BookBLL
    {
        BookDAL dal = new BookDAL();
        /// <summary>
        /// 根据书籍类型ID获取名称
        /// </summary>
        /// <param name="BookTypeId"></param>
        /// <returns></returns>
        public string FindBookTypeNameFromID(long BookTypeId)
        {
            DataTable table = dal.FindTypeNameFromId(BookTypeId);
            string BookTypeName = table.Rows[0][0].ToString();
            return BookTypeName;
        }
        /// <summary>
        /// 获取书籍类型排行前10
        /// </summary>
        /// <returns></returns>
        public List<BookType> SelectBookTypeRank()
        {
            BookType model = new BookType();
            List<BookType> list = new List<BookType>();
            list = Common.TableToList<BookType>(dal.SelectBookTypeRank());
            return list;
        }
        /// <summary>
        /// 获取指定的书籍类型 前十的书籍
        /// </summary>
        /// <param name="typeid"></param>
        /// <returns></returns>
        public List<BookModel> SelectBookRang(string typeid)
        {
            List<BookModel> list = new List<BookModel>();
            list = Common.TableToList<BookModel>(dal.SelectBookRank(typeid,"1","10"));
            return list;
        }
        /// <summary>
        /// 获取指定的书籍类型的书籍
        /// </summary>
        /// <param name="typeid"></param>
        /// <returns></returns>
        public List<BookModel> SelectTypeBook(string typeid,string PageIndex,string PageSize)
        {
            List<BookModel> list = new List<BookModel>();
            list = Common.TableToList<BookModel>(dal.SelectBookRank(typeid, PageIndex, PageSize));
            return list;
        }
        
        /// <summary>
        /// 获取总排名前十的书籍
        /// </summary>
        /// <returns></returns>
        public List<BookModel> SelectBook()
        {
            List<BookModel> list = new List<BookModel>();
            list = Common.TableToList<BookModel>(dal.SelectBook());
            return list;
        }
        /// <summary>
        /// 获取指定书籍详情
        /// </summary>
        /// <param name="bookid"></param>
        /// <returns></returns>
        public BookModel SelectDetail(long bookid) {
            BookModel model = new BookModel();
            model = Common.TableToList<BookModel>(dal.SelectBookDetail(bookid))[0];
            return model;
        }
        /// <summary>
        /// 获取书籍的目录
        /// </summary>
        /// <param name="bookid"></param>
        /// <returns></returns>
        public List<ChapterModel> SelectList(long bookid)
        {
            List<ChapterModel> list = new List<ChapterModel>();
            list = Common.TableToList<ChapterModel>(dal.SelectList(bookid));
            return list;
        }
        /// <summary>
        /// 获取章节内容
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ChapterModel ChapterContent(ChapterModel model)
        {
            ChapterModel result = new ChapterModel();
            result = Common.TableToList<ChapterModel>(dal.ChapterContent(model))[0];
            return result;
        }
        /// <summary>
        /// 获取章节最小最大的ID
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void ChapterminMaxID(ChapterModel model,out int min,out int max )
        {
            var table = dal.ChapterminMaxID(model);
            min = Int32.Parse(table.Rows[0][0].ToString());
            max = Int32.Parse(table.Rows[0][1].ToString());
        }
        /// <summary>
        /// 添加收藏
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool AddCollection(BookCollectionModel model)
        {
            int panduan = dal.FindCollect(model);
            if (panduan > 0)
            {
                return true;
            }else
            {
                int result = dal.AddCollection(model);
                if (result == 1)
                {
                    return true;
                }
                return false;
            }
        }
        /// <summary>
        /// 更新浏览数量
        /// </summary>
        /// <param name="bookid"></param>
        /// <returns></returns>
        public bool UpdateVisisNum(string bookid)
        {
            int newnum = 0;
            DataTable table = dal.FindBookVisNum(bookid);
            long booktypeid = Int64.Parse(table.Rows[0][1].ToString());
            newnum = Int32.Parse(table.Rows[0][0].ToString()) + 1;
            DataTable typetable = dal.FindBooTypekVisNum(booktypeid);
            int typenum = Int32.Parse(typetable.Rows[0][0].ToString()) + 1;
            int result1 = dal.UpdateVisisNum(bookid, newnum);
            int result2 = dal.UpdateBookTypeVisisNum(booktypeid, typenum);
            if (result1 > 0 && result2 > 0)
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// 添加投诉表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool AddReport(ReportModel model)
        {
            int result = dal.AddReport(model);
            if (result > 0)
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// 添加点评表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool AddComment(CommentModel model)
        {
            int result = dal.AddComment(model);
            if (result > 0)
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// 获取搜索的书籍列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public List<BookModel> FindBookList(BookModel model)
        {
            List<BookModel> list = new List<BookModel>();
            DataTable table =  dal.FindBookList(model);
            list = Common.TableToList<BookModel>(table);
            return list;
        }
        /// <summary>
        /// 获取书籍的点评
        /// </summary>
        /// <param name="bookid"></param>
        /// <returns></returns>
        public List<CommentModel> FindComment(long bookid,int pageindex,int pagesize,out int num)
        {
            DataTable table = dal.FindComment(bookid, pageindex, pagesize,out num);
            List<CommentModel> list = new List<CommentModel>();
            list = Common.TableToList<CommentModel>(table);
            return list;
        }
        /// <summary>
        /// 获取推荐表
        /// </summary>
        /// <returns></returns>
        public List<BookModel> FindRecommend()
        {
            DataTable table =  dal.FindRecommend();
            List<BookModel> list = new List<BookModel>();
            list = Common.TableToList<BookModel>(table);
            return list;
        }
        /// <summary>
        /// 更新推荐表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool UpdateRecommend(Recommend model)
        {
            int result = dal.UpdateRecommend(model);
            if (result > 0)
            {
                return true;
            }
            return false;

        }
        /// <summary>
        /// 获取Recommenddata
        /// </summary>
        /// <returns></returns>
        public List<Recommend> SelectRecommenddata()
        {
            DataTable table = dal.SelectRecommenddata();
            List<Recommend> list = new List<Recommend>();
            list = Common.TableToList<Recommend>(table);
            return list;
        }
            /// <summary>
            /// 获取书籍的章节
            /// </summary>
            /// <param name="bookid"></param>
            /// <returns></returns>
            //public List<ChapterModel> FindChapter(long bookid)
            //{
            //    List<ChapterModel> list = new List<ChapterModel>();
            //    DataTable table = dal.FindChapter(bookid);
            //    list =  Common.TableToList<ChapterModel>(table);
            //    return list;
            //}
        }
}
