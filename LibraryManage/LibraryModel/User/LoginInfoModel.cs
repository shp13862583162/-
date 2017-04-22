using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryModel.User
{
    /// <summary>
    /// 用户登录信息实体
    /// </summary>
    public class LoginInfoModel
    {
        /// <summary>
        /// 用户密码
        /// </summary>
        public string Password
        {
            get; set;
        }
        /// <summary>
        /// 用户账户
        /// </summary>
        public long Account
        {
            get; set;
        }
        /// <summary>
        /// 用户名称
        /// </summary>
        public string Name
        {
            get; set;
        }
        /// <summary>
        /// 权限：1：读者，2：作者，3：管理员
        /// </summary>
        public int Type
        {
            get;set;
        }
        /// <summary>
        /// 权限名称1：读者，2：作者，3：管理员
        /// </summary>
        public string UserTypeName
        {
            get; set;
        }
        /// <summary>
        /// 用户性别
        /// </summary>
        public int Sex
        {
            get; set;
        }
    }
}
