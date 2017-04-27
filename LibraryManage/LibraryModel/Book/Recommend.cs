using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryModel.Book
{
    /// <summary>
    /// 推荐表实体
    /// </summary>
    public class Recommend
    {
        /// <summary>
        /// 主键
        /// </summary>
        public long ID
        {
            get;set;
        }
        /// <summary>
        /// 书籍id
        /// </summary>
        public long BookId
        {
            get;set;
        }
        /// <summary>
        /// 有效性  0：有效 1：无效
        /// </summary>
        public int IsValid
        {
            get;set;
        }
        /// <summary>
        /// 是否删除   0：未删除 1：已删除
        /// </summary>
        public int IsDeleted
        {
            get; set;
        }
    }
}
