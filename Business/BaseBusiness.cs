using System;
using System.Collections.Generic;
using System.Text;

namespace Business
{
    //继承业务类
    public class BaseBusiness : IBaseBusiness
    {
        //泛型添加
        public int Add<T>(T t)
        {
            throw new NotImplementedException();
        }
        //泛型删除
        public int Delete(string sql)
        {
            throw new NotImplementedException();
        }
        //泛型显示
        public List<T> Select<T>(string sql)
        {
            throw new NotImplementedException();
        }
        //泛型修改
        public int Update(string sql)
        {
            throw new NotImplementedException();
        }
    }
}
