using LibraryBLL;
using LibraryModel;
using LibraryModel.Book;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Web;

namespace System.Web.Mvc
{
    public static class HtmlHelperExtends
    {
        /// <summary>
        /// 生成table，前端
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="htmlHelper"></param>
        /// <param name="list"></param>
        /// <param name="number"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static MvcHtmlString ToTable<T>(this HtmlHelper htmlHelper, List<T> list,int number,int index=1) where T :new()
        {
            if (list == null)
            {
                return MvcHtmlString.Create("<script>alert('查无数据！！')</script>");
            }
            StringBuilder str = new StringBuilder();
            str.Append("<table><thead><tr>");
            T t = new T();
            Type TType = t.GetType();
            PropertyInfo[] propertys = t.GetType().GetProperties();
            int count = propertys.Length;
            foreach(PropertyInfo property in propertys)
            {
                str.AppendFormat("<th>{0}</th>",property.Name);
            }
            str.Append("</tr></thead><tbody>");
            foreach(var item in list)
            {
                str.Append("<tr>");
                foreach (PropertyInfo property in propertys)
                {
                    var name = property.Name;
                    //property.GetValue(item, null);
                    str.AppendFormat("<td>{0}</td>", property.GetMethod.Invoke(item, null));
                }
                str.Append("</tr>");
            }
            str.Append("</tbody>");
            str.Append("<tfoot><tr><td colspan="+count+"></td></tr></tfoot>");
            str.Append("</table>");
            return MvcHtmlString.Create(str.ToString());
        }


        public static MvcHtmlString AutoCompleteFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> selector)
        {
            var selectionname = ExpressionHelper.GetExpressionText(selector);
            var uhelper = new UrlHelper(htmlHelper.ViewContext.RequestContext);
            var model = htmlHelper.ViewContext.ViewData.Model;
            var tmodel = model == null ? default(TModel) : (TModel)model;
            var selection = tmodel == null ? default(TProperty) : selector.Compile()(tmodel);


            string str = string.Format(@"<script>alert({0});</script>",selectionname);
            return MvcHtmlString.Create(str);
        }
        /// <summary>
        /// 建立一个书籍类型前十的table列表
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="number"></param>
        /// <returns></returns>
        public static MvcHtmlString Typetable(this HtmlHelper htmlHelper)
        {
            BookBLL bll = new BookBLL();
            List<BookType> list = new List<BookType>();
            list= bll.SelectBookTypeRank();
            int number = list.Count();
            StringBuilder build = new StringBuilder();
            int width = 10;
            string style = "style='width:" + width + "%'";
            build.Append("<tr>");
            for(int i = 1; i < number+1; i++)
            {
                if (i % 10 == 1 && i > 10)
                {
                    build.Append("</tr><tr>");
                }
                build.Append("<td id='typeid"+i+"' "+ style + ">" + i+",<a class='typerangname' myid='" + list[i - 1].BTId + "' id='typenameid"+i+ "'>" + list[i-1].BTName+"</a></td>");
            }
            build.Append("</tr>");
            return MvcHtmlString.Create(build.ToString());
        }
        /// <summary>
        /// 获取排行前十的书籍
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <returns></returns>
        public static MvcHtmlString SelectBookTable(this HtmlHelper htmlHelper)
        {
            BookBLL bll = new BookBLL();
            List<BookModel> list = new List<BookModel>();
            list= bll.SelectBook();
            StringBuilder build = new StringBuilder();
          
            
            for (int i = 1; i < 11; i++)
            {
                build.Append("<tr>");
                BookModel model = new BookModel() { BName = "asda" + i, BBookID = i };
                list.Add(model);
                build.Append("<td id='booktdid" + i + "' >" + i + ",<a class='bookclassa' myid='" + list[i - 1].BBookID + "' id='typenameid" + i + "'>" + list[i - 1].BName + "</a></td>");
                build.Append("</tr>");

            }
            return MvcHtmlString.Create(build.ToString());
        }
       
    }
}