using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOT_Project_Commarc.DataAccess.AdoNet
{
    /// <summary>
    /// SqlServer访问数据库
    /// </summary>

    public class SqlServerAccess
    {
        readonly string strConnection = "Data Source=192.168.1.126;Initial Catalog=ERPDB;User ID=sa; pwd=123456";

        //System.Configuration.ConfigurationSettings.AppSettings["SqlServerConection"];        

        /// <summary>
        /// 执行Sql语句，返回受影响行数  （主要是添加、修改、删除）
        /// </summary>
        /// <param name="strSql"></param>
        /// <returns></returns>
        public int ExecSqlGetRow(string strSql)
        {
            using (SqlConnection connection = new SqlConnection(strConnection))
            {
                if(connection.State == ConnectionState.Closed)
                    connection.Open();

                SqlCommand command = new SqlCommand(strSql, connection);
                int result = command.ExecuteNonQuery();

                return result;
            }
        }
        //执行多条SQL语句（事务）
        public  int ExecSqlGetRow(List<string>sqls)
        {
            using (SqlConnection connection=new SqlConnection(strConnection))
            {
                if (connection.State==ConnectionState.Closed)
                {
                    connection.Open();
                }
                SqlTransaction tran = connection.BeginTransaction();
                try
                {
                    int result = 0;
                    SqlCommand command = new SqlCommand();
                    command.Transaction = tran;
                    command.Connection = connection;
                    foreach (var item in sqls)
                    {
                        //指定sql
                        command.CommandText = item;
                        //执行SQL
                        result += command.ExecuteNonQuery();
                    }
                    tran.Commit();//提交事务
                    return result;
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    return -1;
                }
            }
        }

        /// <summary>
        /// 执行Sql语句，返回数据集(主要是查询)
        /// </summary>
        /// <param name="strSql"></param>
        /// <returns></returns>
        public DataSet ExecSqlGetDataTable (string strSql)
        {
            using (SqlConnection connection = new SqlConnection(strConnection))
            {
                //connection.Open();

                SqlCommand command = new SqlCommand(strSql, connection);
                SqlDataAdapter da = new SqlDataAdapter(command);

                DataSet ds = new DataSet();
                da.Fill(ds);

                return ds;
            }            
        }

        /// <summary>
        /// 执行存储过程,返回数据集
        /// </summary>
        /// <param name="procName"></param>
        /// <param name="parms"></param>
        /// <returns></returns>
        public DataSet ExecSqlGetDataTable(string procName,Dictionary<string,object> parms,string outParmsName,out string outStr)
        {
            outStr = "";
            using (SqlConnection connection =new SqlConnection (strConnection))
            {
                
                SqlCommand command = new SqlCommand(procName,connection);
                command.CommandType = CommandType.StoredProcedure;
                //添加参数
                foreach (var item in parms)
                {
                    command.Parameters.AddWithValue(item.Key,item.Value);
                }
                command.Parameters[outParmsName].Direction = ParameterDirection.Output;
                outStr = command.Parameters[outParmsName].Value.ToString();
                SqlDataAdapter da = new SqlDataAdapter(command);
                DataSet ds = new DataSet();
                da.Fill(ds);
                return ds;
            }
            
        }
        public DataSet ExecSqlGetDataTable(string procName, Dictionary<string, object> parms )
        {
            using (SqlConnection connection = new SqlConnection(strConnection))
            {
                 SqlCommand command = new SqlCommand(procName, connection);
                command.CommandType = CommandType.StoredProcedure;
                //添加参数
                foreach (var item in parms)
                {
                    command.Parameters.AddWithValue(item.Key, item.Value);
                }
              
                SqlDataAdapter da = new SqlDataAdapter(command);
                DataSet ds = new DataSet();
                da.Fill(ds);
                return ds;
            }

        }
    }
}
