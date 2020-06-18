using Common;
using DataAccess.AdoNet;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Business
{
    //继承业务类
    public class BaseBusiness : IBaseBusiness
    {
        public  SqlServerAccess sqlServer = new SqlServerAccess();
        //泛型添加
        public int Add<T>(T t)
        {
            string sql = ReflectionHelper.ModelToInsertSql(t);
            return sqlServer.ExecSqlGetRow(sql);
        }
        //泛型删除
        public int Delete(string sql)
        {
            return sqlServer.ExecSqlGetRow(sql);
        }
        //泛型显示
        public List<T> Select<T>(string sql)
        {
            DataSet ds = sqlServer.ExecSqlGetDataTable(sql);
            List<T> lst = new List<T>();
            if (ds != null)
                lst = ReflectionHelper.DatatableToList<T>(ds.Tables[0]);


            return lst;
        }
        //泛型修改
        public int Update(string sql)
        {
            return sqlServer.ExecSqlGetRow(sql);
        }
    }
}
