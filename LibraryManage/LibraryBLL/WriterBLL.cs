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
    /// 作者 逻辑层
    /// </summary>
    public class WriterBLL
    {
        WriterDAL dal = new WriterDAL();
        // <summary>
        /// 验证 申请读者权限数据 是否存在
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int  SelectPromotionNum(PromotionModel model)
        {
            int result = Common.TableToList<PromotionModel>(dal.SelectPromotionNum(model)).Count;
            return result;
        }
        /// <summary>
        /// 添加晋升表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool AddPromotion(PromotionModel model)
        {
            int result  = dal.AddPromotion(model);
            if(result > 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        /// <summary>
        /// 获取上架书籍
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public List<BookModel> GetShelvesBooks(BookModel model,out int total)
        {
            List<BookModel> list = new List<BookModel>();
            DataTableCollection tables =  dal.GetShelvesBooks(model);
            list = Common.TableToList<BookModel>(tables[0]);
            total = Int32.Parse(tables[1].Rows[0][0].ToString());
            return list;
        }
        /// <summary>
        /// 添加书籍申请
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool AddBook(BookModel model)
        {
            long maxbookid =  dal.FindMaxBookID();
            long bookid = maxbookid + 1;
            model.BBookID = bookid;
            int result = dal.AddBook(model);
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
        /// 获取所有书籍类型
        /// </summary>
        /// <returns></returns>
        public List<BookType> FindBookType()
        {
            List<BookType> list = new List<BookType>();
            DataTable table = dal.FindBookType();
            list = Common.TableToList<BookType>(table);
            return list;
        }
        /// <summary>
        /// 添加章节的具体操作
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool AddChapter(ChapterModel model)
        {
            long maxchapterid = dal.FindBookMaxChapterID(model);
            long chapterid = maxchapterid + 1;
            model.CHID = chapterid;
            int result = dal.AddChapter(model);
            if (result > 0)
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
