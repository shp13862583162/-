using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryModel.User
{
    /// <summary>
    /// 晋升 实体
    /// </summary>
    public class PromotionModel:PageModel
    {
        public PromotionModel()
        {
            PAccount = 0;
            PApplyTime = DateTime.Parse("1900-01-01");
            StartTime = DateTime.Parse("1900-01-01");
            EndTime = DateTime.Parse("1900-01-01");
            PStatue = 0;
        }
        /// <summary>
        /// 申请账户
        /// </summary>
        public long PAccount
        {
            get; set;
        }
        /// <summary>
        /// 申请时间
        /// </summary>
        public DateTime PApplyTime
        {
            get; set;
        }
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime StartTime
        {
            get; set;
        }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndTime
        {
            get; set;
        }
        /// <summary>
        /// 申请状态  1：正在申请2：申请通过 3：申请驳回 
        /// </summary>
        public int PStatue
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
