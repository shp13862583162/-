using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryModel.User
{
    /// <summary>
    /// 举报封杀  实体
    /// </summary>
    public class ReportModel:PageModel
    {
        public ReportModel()
        {
            ReportTime = DateTime.Parse("1900-01-01");
            StartReportTime = DateTime.Parse("1900-01-01");
            EndReportTime = DateTime.Parse("1900-01-01");
        }
        /// <summary>
        /// 被举报者ID
        /// </summary>
        public long ReportID
        {
            get; set;
        }
        /// <summary>
        /// 被举报者姓名
        /// </summary>
        public string ReportName
        {
            get; set;
        }
        /// <summary>
        /// 举报者ID
        /// </summary>
        public long Complainant
        {
            get; set;
        }
        /// <summary>
        /// 举报原因
        /// </summary>
        public string ReportReason
        {
            get; set;
        }
        /// <summary>
        /// 被举报次数
        /// </summary>
        public int ReportNum
        {
            get; set;
        }
        /// <summary>
        /// 举报时间
        /// </summary>
        public DateTime ReportTime
        {
            get; set;
        }
        /// <summary>
        /// 开始 举报时间
        /// </summary>
        public DateTime StartReportTime
        {
            get; set;
        }
        /// <summary>
        /// 结束 举报时间
        /// </summary>
        public DateTime EndReportTime
        {
            get; set;
        }
        /// <summary>
        /// 1：正常 2:  被举报 3：被封杀
        /// </summary>
        public int UseStatue
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
