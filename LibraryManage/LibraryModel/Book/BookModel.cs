using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryModel.Book
{
    public class BookModel:PageModel
    {
        public BookModel()
        {
            ApplyTime = DateTime.Parse("1900-01-01");
            StartApplyTime = DateTime.Parse("1900-01-01");
            EndApplyTime = DateTime.Parse("1900-01-01");
        }
        /// <summary>
        /// 主键id
        /// </summary>
        public long ID
        {
            get; set;
        }
        /// <summary>
        /// 用户账号
        /// </summary>
        public long Account
        {
            get; set;
        }
        /// <summary>
        /// 书籍id
        /// </summary>
        public long BBookID
        {
            get; set;
        }
        /// <summary>
        /// 书籍名称
        /// </summary>
        public string BName
        {
            get; set;
        }
        /// <summary>
        /// 作者
        /// </summary>
        public string Writer
        {
            get; set;
        } 
        /// <summary>
          /// 作者ID
          /// </summary>
        public long WriterID
        {
            get; set;
        }
        /// <summary>
        /// 简介
        /// </summary>
        public string BIntroduction
        {
            get; set;
        }
        /// <summary>
        /// 书籍类型id
        /// </summary>
        public int BTypeID
        {
            get; set;
        }
        /// <summary>
        /// 书籍类型
        /// </summary>
        public string booktype
        {
            get; set;
        }
        /// <summary>
        /// 书籍数量
        /// </summary>
        public int BNumber
        {
            get; set;
        }
        /// <summary>
        /// 书籍浏览数量
        /// </summary>
        public int BVisitCount
        {
            get; set;
        }
        /// <summary>
        /// 点评数量
        /// </summary>
        public int critique
        {
            get; set;
        }
        /// <summary>
        /// 书籍的状态0：无1：申请中2：申请成功3：申请驳回4:非2
        /// </summary>
        public int BStatue
        {
            get; set;
        }
        /// <summary>
        /// 申请时间
        /// </summary>
        public DateTime ApplyTime
        {
            get; set;
        }
        /// <summary>
        /// 开始申请时间
        /// </summary>
        public DateTime StartApplyTime
        {
            get; set;
        }
        /// <summary>
        /// 结束申请时间
        /// </summary>
        public DateTime EndApplyTime
        {
            get; set;
        }
        /// <summary>
        /// 有效性1有效，0无效
        /// </summary>
        public int IsValid
        {
            get; set;
        }
        /// <summary>
        /// 是否删除，1未删除，0已删除
        /// </summary>
        public int IsDeleted
        {
            get; set;
        }
    }
}
