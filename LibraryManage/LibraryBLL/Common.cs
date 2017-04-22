using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LibraryBLL
{
    public class Common
    {
        /// <summary>
        /// 把datatable格式转换为list格式
        /// </summary>  
        /// <typeparam name="T"></typeparam>
        /// <param name="table"></param>
        /// <returns></returns>
        public static List<T> TableToList<T>(DataTable table) where T :new()
        {
            List<T> list = new List<T>();
            Type type = typeof(T);
            if (table != null && table.Rows.Count > 0)
            {
                
                PropertyInfo[] properties = type.GetProperties();
                foreach (DataRow row in table.Rows)
                {
                    T t = new T();
                    foreach (PropertyInfo item in properties)
                    {
                        string name = item.Name;
                        if (table.Columns.Contains(name))
                        {
                            if (!item.CanWrite)
                            {
                                continue;
                            }
                            object value = row[name];
                            if (value != DBNull.Value)
                            {
                                item.SetValue(t, value, null);
                            }
                        }
                    }
                    list.Add(t);
                }
            }
            return list;
        }
        /// <summary>
        /// 把list转换为table格式
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public static DataTable ListToTable<T>(List<T> list) where T :new()
        {
            DataTable table = new DataTable();
            if(list != null&& list.Count > 0)
            {
                T t = new T();
                Type type = typeof(T);
                PropertyInfo[] properties = t.GetType().GetProperties();
                foreach(PropertyInfo property in properties)
                {
                    table.Columns.Add(property.Name);
                }
                for (int i = 0; i < list.Count; i++)
                {
                    ArrayList tempList = new ArrayList();
                    foreach (PropertyInfo property in properties)
                    {
                        object objc = property.GetMethod.Invoke(list[i],null);
                        object obj  = property.GetValue(list[i], null);
                        tempList.Add(obj);
                    }
                    object[] array = tempList.ToArray();
                    table.LoadDataRow(array, true);
                }
            }
            return table;
        }
       
    }
}
