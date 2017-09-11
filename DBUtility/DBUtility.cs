using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.IO;

using Conf;

namespace DBTools
{
    public class DBUtility
    {
        private SqlConnection sqlConnection;//= new SqlConnection(Configuration.ConnectionString);
        private DataSet dataSet = new DataSet();

        public DBUtility()
        {
            sqlConnection = new SqlConnection(Configuration.ConnectionString);
            this.dataSet.Tables.Add("kq_source_ding");   //钉钉打卡原始数据表
            this.dataSet.Tables.Add("kq_sourceqk_ding"); //钉钉签卡记录表
            this.dataSet.Tables.Add("kq_paiban_ding");   //钉钉排班记录表
            this.dataSet.Tables.Add("kq_process_ding");  //钉钉审批数据表
        }

        //打开数据库连接
        public void OpenConnection()
        {
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }
        }

        //关闭数据库连接
        public void CloseConnection()
        {
            if (sqlConnection.State != ConnectionState.Closed)
                sqlConnection.Close();
        }

        public List<string> Select()
        {
            List<string> userids = new List<string>();
            using (sqlConnection)
            {
                SqlCommand cmd = new SqlCommand
                {
                    Connection = sqlConnection,
                    CommandText = @"select code from zlemployee where isnull(lzdate,getdate())<dateadd(day,-7,getdate()) order by code"
                };
                try
                {
                    sqlConnection.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            userids.Add((string)reader[0]);
                        }
                    }
                }
                catch (Exception e)
                {
                    using (FileStream fs = new FileStream(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "error.log"), FileMode.Append))
                    {
                        fs.Write(Encoding.UTF8.GetBytes(e.Message + "\r\n"), 0, e.Message.Length);
                    }
                }
            }
            return userids;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="table">表名</param>
        /// <param name="columns">["id","code","name"]</param>
        /// <param name="where"></param>
        /// <returns></returns>
        public DataTable Select(string table, string[] columns, string where = "(1=1)")
        {
            DataTable dt = new DataTable();
            foreach (string columnName in columns)
            {
                dt.Columns.Add(columnName);
            }

            string sql = "";
            sql = "select " + (columns.Length <= 1 ? columns[0] : string.Join(",", columns)) + " from " + table + " where " + where;
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = sqlConnection;
                cmd.CommandText = sql;

                using (sqlConnection)
                {
                    sqlConnection.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            DataRow row = dt.NewRow();
                            foreach (string columnName in columns)
                            {
                                row[columnName] = reader[columnName];
                            }
                            dt.Rows.Add(row);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                using (FileStream fs = new FileStream(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "error.log"), FileMode.Append))
                {
                    fs.Write(Encoding.UTF8.GetBytes(e.Message + "\r\n"), 0, e.Message.Length);
                }
            }
            return dt;
        }

        public void Insert()
        {
        }

        public void Delete(DataTable table)
        {
        }

        public void ExecuteProc(string procName, Dictionary<string, string> param)
        {
        }
    }
}