using LibraryModel.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibraryManageMVC.MyMvcApplication
{
    public class MyMvcApplication<T> : System.Web.Mvc.WebViewPage<T>
    {
        public LoginInfoModel UserEntity
        {
            get
            {
                if (Session["LoginInfo"] != null)
                {
                    LoginInfoModel model = Session["LoginInfo"] as LoginInfoModel;
                    return model;
                }
                else
                {
                    return new LoginInfoModel();
                }
            }
        }

        public override void Execute()
        {
            // throw new NotImplementedException();
        }

    }
}