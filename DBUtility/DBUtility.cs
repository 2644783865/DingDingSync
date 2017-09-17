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
        private SqlConnection sqlConnection;
        private DataSet dataSet = new DataSet();

        public DBUtility()
        {
            sqlConnection = new SqlConnection(Configuration.ConnectionString);
            //this.dataSet.Tables.Add("ding_kq_source");   //钉钉打卡原始数据表
            //this.dataSet.Tables.Add("ding_kq_sourceqk"); //钉钉签卡记录表
            //this.dataSet.Tables.Add("ding_kq_paiban");   //钉钉排班记录表
            //this.dataSet.Tables.Add("ding_kq_banzhi");   //钉钉考勤组
            //this.dataSet.Tables.Add("ding_kq_process");  //钉钉审批数据表
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
                catch
                {
                    throw;
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
            catch
            {
                throw;
            }
            return dt;
        }

        //将DataTable数据插入数据库
        public void Insert(DataTable table)
        {
            Dictionary<string, string> columnParam = new Dictionary<string, string>();
            string sql = string.Empty;

            foreach (DataColumn column in table.Columns)
            {
                columnParam.Add("@" + column.ColumnName, column.ColumnName);
            }

            sql = string.Format("insert into {0} ({1}) values({2})", table.TableName, string.Join(",", columnParam.Values.ToArray<string>()), string.Join(",", columnParam.Keys.ToArray<string>()));

            try
            {
                using (sqlConnection)
                {
                    sqlConnection.Open();
                    SqlCommand cmd = new SqlCommand(sql, sqlConnection);

                    foreach (var kv in columnParam)
                    {
                        SqlParameter parameter = new SqlParameter(kv.Key, SqlDbType.VarChar, 100, kv.Value);
                        cmd.Parameters.Add(parameter);
                    }

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.InsertCommand = cmd;
                    adapter.Update(table.GetChanges());
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        public void Delete(DataTable table)
        {
        }

        public void ExecuteProc(string procName, Dictionary<string, string> param)
        {
        }
    }
}