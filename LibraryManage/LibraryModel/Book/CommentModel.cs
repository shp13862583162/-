using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryModel.Book
{
    /// <summary>
    /// 点评表
    /// </summary>
    public class CommentModel
    {
        /// <summary>
        /// 主键id
        /// </summary>
        public long ID
        {
            get; set;
        }
        /// <summary>
        /// 书籍id
        /// </summary>
        public long BookID
        {
            get; set;
        }
        /// <summary>
        /// 用户账号
        /// </summary>
        public long ACCount
        {
            get; set;
        }
        /// <summary>
        /// 点评内容
        /// </summary>
        public string BContent
        {
            get; set;
        }
        /// <summary>
        /// 点评时间
        /// </summary>
        public DateTime CTime
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
