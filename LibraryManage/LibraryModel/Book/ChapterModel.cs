using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryModel.Book
{
    public class ChapterModel
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
        /// 章节id
        /// </summary>
        public long CHID
        {
            get; set;
        }
        /// <summary>
        /// 章节名称
        /// </summary>
        public string CHName
        {
            get; set;
        }
        /// <summary>
        /// 章节内容
        /// </summary>
        public string CHContent
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
