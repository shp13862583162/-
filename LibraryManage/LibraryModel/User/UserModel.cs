using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryModel.User
{
    /// <summary>
    /// 用户实体
    /// </summary>
    public class UserModel
    {
        /// <summary>
        /// 账号
        /// </summary>
        public long Account
        {
            get; set;
        }
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name
        {
            get; set;
        }
        /// <summary>
        /// 密码
        /// </summary>
        public string Password
        {
            get; set;
        }
        /// <summary>
        /// 性别0：男1：女
        /// </summary>
        public int Sex
        {
            get; set;
        }
        /// <summary>
        /// 用户类型1：读者2：作者3：管理员
        /// </summary>
        public int Type
        {
            get; set;
        }
        /// <summary>
        /// 用户类型1：读者2：作者3：管理员
        /// </summary>
        public string TypeName
        {
            get; set;
        }
        /// <summary>
        /// 晋升状态1：正在申请2：申请通过3：申请驳回
        /// </summary>
        public int PStatue
        {
            get; set;
        }
        /// <summary>
        /// 作者状态0：无（用户还是读者权限）1：正常2:  被举报3：被封杀
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
