using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryModel.User
{
    /// <summary>
    /// 注册实体
    /// </summary>
    public class RegisterModel
    {
        public RegisterModel()
        {
            sex =-1;
            usertype = 1;
        }
        /// <summary>
        /// 添加用户类型
        /// </summary>
        public int usertype
        {
            get;set;
        }
        /// <summary>
        /// 添加用户名称
        /// </summary>
        public string username
        {
            get; set;
        }
        /// <summary>
        /// 添加用户账号
        /// </summary>
        public long account
        {
            get; set;
        }
        /// <summary>
        /// 添加用户密码
        /// </summary>
        public string pwd
        {
            get; set;
        }
        /// <summary>
        /// 性别
        /// </summary>
        public int sex
        {
            get; set;
        }
    }
}
