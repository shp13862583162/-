using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryModel
{
    public class PageModel
    {
        public PageModel(){
            pageindex = 0;
            pagesize = 20;
        }
        /// <summary>
        /// 索引
        /// </summary>
        public int pageindex
        {
            get; set;
        }
        /// <summary>
        /// 页面大小
        /// </summary>
        public int pagesize
        {
            get; set;
        }
    }
}
