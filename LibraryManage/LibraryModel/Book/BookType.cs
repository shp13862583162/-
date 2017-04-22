using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryModel.Book
{
    public class BookType
    {
        /// <summary>
        /// 主键id
        /// </summary>
        public long ID
        {
            get; set;
        }
        /// <summary>
        /// 书籍类型id
        /// </summary>
        public long BTId
        {
            get; set;
        }
        /// <summary>
        /// 书籍类型名称
        /// </summary>
        public string BTName
        {
            get; set;
        }
        /// <summary>
        /// 该类型书籍浏览数量
        /// </summary>
        public int BTVisitCount
        {
            get; set;
        }
        /// <summary>
        /// 有效性1有效，0无效
        /// </summary>
        public int isvalid
        {
            get; set;
        }
        /// <summary>
        /// 是否删除，1未删除，0已删除
        /// </summary>
        public int isdelete
        {
            get; set;
        }
    }
}
