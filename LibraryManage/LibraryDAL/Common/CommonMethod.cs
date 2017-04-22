using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryDAL.Common
{
    public class CommonMethod
    {
        /// <summary>
        /// select查数据库封装方法
        /// </summary>
        /// <typeparam name="T">实体</typeparam>
        /// <param name="sql">sql语句</param>
        /// <param name="t"></param>
        /// <param name="para"></param>
        /// <returns></returns>
        public static DataTable SelectData(string sql ,SqlParameter[] para,string dataname) {
            SqlConnection con = new SqlConnection();
            DataTable table = new DataTable();
            string connection = string.Format("data source=.; Database={0};user id=SA; password=123qwe!@#", dataname);
            con.ConnectionString = connection;
            con.Open();
            SqlCommand cmd = new SqlCommand(sql, con);

            cmd.Parameters.AddRange(para);
            SqlDataReader reader = cmd.ExecuteReader();
            for (int i = 0; i < reader.VisibleFieldCount; i++)
            {
                table.Columns.Add(reader.GetName(i), reader.GetFieldType(i));
            }
            while (reader.Read())
            {
                DataRow dr = table.NewRow();
                for (int i = 0; i < reader.VisibleFieldCount; i++)
                {
                    dr[i] = reader.GetValue(i);
                }
                table.Rows.Add(dr);
            }
            con.Close();
            return table;
            #region  使用DataAdapter和DataSet对象读取数据
            //SqlDataAdapter ad = new SqlDataAdapter(sql, con);
            //DataSet set = new DataSet();
            //ad.SelectCommand.Parameters.AddRange(para);
            //ad.Fill(table);
            //con.Close();
            //return table;
            #endregion
        }
        /// <summary>
        /// select查数据库封装方法
        /// </summary>
        /// <typeparam name="T">实体</typeparam>
        /// <param name="sql">sql语句</param>
        /// <param name="t"></param>
        /// <param name="para"></param>
        /// <returns></returns>
        public static DataTableCollection GetDataSet(string sql, SqlParameter[] para, string dataname)
        {
            SqlConnection con = new SqlConnection();
            DataTable table = new DataTable();
            string connection = string.Format("data source=.; Database={0};user id=SA; password=123qwe!@#", dataname);
            con.ConnectionString = connection;
            con.Open();
            #region  使用DataAdapter和DataSet对象读取数据
            SqlDataAdapter ad = new SqlDataAdapter(sql, con);
            ad.SelectCommand.Parameters.AddRange(para);
            DataSet set = new DataSet();
            ad.Fill(set);
            DataTableCollection result = set.Tables;
            con.Close();
            return result;
            #endregion
        }
        /// <summary>
        /// 删除数据库数据的封装方法
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="para"></param>
        /// <param name="dataname"></param>
        /// <returns></returns>
        public static int DeleteData(string sql,SqlParameter[] para, string dataname)
        {
            SqlConnection con = new SqlConnection();
            string connection = string.Format("data source=.; Database={0};user id=SA; password=123qwe!@#", dataname);
            con.ConnectionString = connection;
            con.Open();
            SqlCommand cmd = new SqlCommand(sql, con);

            cmd.Parameters.AddRange(para);

            int result = cmd.ExecuteNonQuery();
            con.Close();
            return result;
        }
        /// <summary>
        /// 添加数据库数据的封装方法
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="list"></param>
        /// <param name="para"></param>
        /// <param name="dataname"></param>
        /// <returns></returns>
        public static int AddData(string sql, SqlParameter[] para, string dataname)
        {
            SqlConnection con = new SqlConnection();
            string connection = string.Format("data source=.; Database={0};user id=SA; password=123qwe!@#", dataname);
            con.ConnectionString = connection;
            con.Open();
            SqlCommand cmd = new SqlCommand(sql, con);

            cmd.Parameters.AddRange(para);

            int result = cmd.ExecuteNonQuery();
            con.Close();
            return result;
        }
        /// <summary>
        /// 修改数据库数据的封装方法
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="list"></param>
        /// <param name="para"></param>
        /// <param name="dataname"></param>
        /// <returns></returns>
        public static int UpdateData(string sql, SqlParameter[] para, string dataname)
        {
            SqlConnection con = new SqlConnection();
            string connection = string.Format("data source=.; Database={0};user id=SA; password=123qwe!@#", dataname);
            con.ConnectionString = connection;
            con.Open();
            SqlCommand cmd = new SqlCommand(sql, con);

            cmd.Parameters.AddRange(para);

            int result = cmd.ExecuteNonQuery();
            con.Close();
            return result;
        }

    }
}
