using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryModel.Book
{
    /// <summary>
    /// 收藏表
    /// </summary>
    public class BookCollectionModel
    {
        /// <summary>
        /// 主键
        /// </summary>
        public long ID
        {
            get; set;
        }
        /// <summary>
        /// 书籍ID
        /// </summary>
        public long BookID
        {
            get; set;
        }
        /// <summary>
        /// 用户账号
        /// </summary>
        public  long Account
        {
            get;set;
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
