using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace TaskManager
{
    class Database
    {
        static string connectionString = ConfigurationManager.ConnectionStrings["TaskDB"].ConnectionString;//Переменная для подключения к БД, хранится в файле App.config

        private static SqlConnection sqlConnection = null;
        
        public static SqlConnection SQLConnection
        {
            get
            {
                return sqlConnection;
            }
            
        }
        
        public static SqlConnection ConnectDB()
        {
            sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();//открываем соединение с SQL ОБЯЗАТЕЛЬНО!
            return sqlConnection;
        }

    }
}
