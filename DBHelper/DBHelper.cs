using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Sql;
using System.Data.SqlClient;

namespace DBUtility
{
    /// <summary>
    /// 单例模式
    /// </summary>
    public class DBHelper
    {
        private static string _connectStr = string.Empty;
        private static DBHelper _dbInstance;

        private SqlDataAdapter adapter;

        private DBHelper()
        {
        }

        public static DBHelper CreateInstance(string connectionString)
        {
            if (_dbInstance == null)
            {
                _dbInstance = new DBHelper();
                _connectStr = connectionString;
            }
            return _dbInstance;
        }

        public void Select()
        {
        }

        public void Insert()
        {
        }

        public void Update()
        {
        }

        public void Delete()
        {
        }

        public void ExecProc()
        {
        }
    }
}