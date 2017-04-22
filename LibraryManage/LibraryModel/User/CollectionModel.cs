using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryModel.User
{
    public class CollectionModel
    {
        /// <summary>
        /// 主键id
        /// </summary>
        public long ID
        {
            get; set;
        }
        /// <summary>
        /// 用户编号id
        /// </summary>
        public string Account
        {
            get; set;
        }
        /// <summary>
        /// 书籍编号id
        /// </summary>
        public long BookID
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
