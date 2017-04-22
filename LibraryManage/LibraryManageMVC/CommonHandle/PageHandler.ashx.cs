using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web;

namespace LibraryManageMVC.CommonHandle
{
    /// <summary>
    /// PageHandler 的摘要说明
    /// </summary>
    public class PageHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            context.Response.Write("Hello World");
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        #region 分页显示table



        public static DataTable TableToPage(DataTable table, string attr1) 
        {
            return TableToPage(table, attr1, "");
        }
        public static DataTable TableToPage(DataTable table,
      string attr1, string attr2) 
        {
            return TableToPage(table, attr1, attr2, "");
        }
        public static DataTable TableToPage(DataTable table,
       string attr1, string attr2, string attr3) 
        {
            return TableToPage(table, attr1, attr2, attr3, "");
        }
        public static DataTable TableToPage(DataTable table,
         string attr1, string attr2, string attr3, string attr4) 
        {
            return TableToPage(table, attr1, attr2, attr3, attr4, "");
        }
        public static DataTable TableToPage(DataTable table,
         string attr1, string attr2, string attr3, string attr4, string attr5) 
        {
            return TableToPage(table, attr1, attr2, attr3, attr4, attr5, "");
        }
        public static DataTable TableToPage(DataTable table,
          string attr1, string attr2, string attr3, string attr4, string attr5, string attr6) 
        {
            return TableToPage(table, attr1, attr2, attr3, attr4, attr5, attr6, "");
        }
        public static DataTable TableToPage(DataTable table,
           string attr1, string attr2, string attr3, string attr4, string attr5, string attr6, string attr7) 
        {
            return TableToPage(table, attr1, attr2, attr3, attr4, attr5, attr6, attr7, "");
        }
        public static DataTable TableToPage(DataTable table,
           string attr1, string attr2, string attr3, string attr4, string attr5, string attr6, string attr7, string attr8) 
        {
            return TableToPage(table, attr1, attr2, attr3, attr4, attr5, attr6, attr7, attr8, "");
        }
        public static DataTable TableToPage(DataTable table,
            string attr1, string attr2, string attr3, string attr4, string attr5, string attr6, string attr7, string attr8, string attr9)
        {
            return TableToPage(table, attr1, attr2, attr3, attr4, attr5, attr6, attr7, attr8, attr9, "");
        }
        public static DataTable TableToPage(DataTable table,
            string attr1, string attr2, string attr3, string attr4, string attr5, string attr6, string attr7, string attr8, string attr9, string attr10) 
        {
            if (table != null && table.Rows.Count > 0)
            {
                string tablestr = "";

                for (int i = 0; i < table.Rows.Count; i++)
                {
                    tablestr += "<tr>";
                    if (!string.IsNullOrWhiteSpace(attr1))
                    {
                        string str1 = table.Rows[i][attr1].ToString();
                        tablestr += "<td>" + str1 + "</td>";
                    }
                    if (!string.IsNullOrWhiteSpace(attr2))
                    {
                        string str2 = table.Rows[i][attr2].ToString();
                        tablestr += "<td>" + str2 + "</td>";
                    }
                    if (!string.IsNullOrWhiteSpace(attr3))
                    {
                        string str3 = table.Rows[i][attr3].ToString();
                        tablestr += "<td>" + str3 + "</td>";
                    }
                    if (!string.IsNullOrWhiteSpace(attr4))
                    {
                        string str4 = table.Rows[i][attr4].ToString();
                        tablestr += "<td>" + str4 + "</td>";
                    }
                    if (!string.IsNullOrWhiteSpace(attr5))
                    {
                        string str5= table.Rows[i][attr5].ToString();
                        tablestr += "<td>" + str5 + "</td>";
                    }
                    if (!string.IsNullOrWhiteSpace(attr6))
                    {
                        string str6 = table.Rows[i][attr6].ToString();
                        tablestr += "<td>" + str6 + "</td>";
                    }
                    if (!string.IsNullOrWhiteSpace(attr7))
                    {
                        string str7 = table.Rows[i][attr7].ToString();
                        tablestr += "<td>" + str7 + "</td>";
                    }
                    if (!string.IsNullOrWhiteSpace(attr8))
                    {
                        string str8 = table.Rows[i][attr8].ToString();
                        tablestr += "<td>" + str8 + "</td>";
                    }
                    if (!string.IsNullOrWhiteSpace(attr9))
                    {
                        string str9 = table.Rows[i][attr9].ToString();
                        tablestr += "<td>" + str9 + "</td>";
                    }
                    if (!string.IsNullOrWhiteSpace(attr10))
                    {
                        string str10 = table.Rows[i][attr10].ToString();
                        tablestr += "<td>" + str10 + "</td>";
                    }
                    tablestr += "</tr>";
                }

            }
            return table;
        }
        #endregion
    }
}