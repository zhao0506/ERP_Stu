using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace IOT_Project_Commarc.Common
{
    public class ReflectionHelper
    {
        /// <summary>
        /// 将Model类转为插入SQL语句
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public static string ModelToInsertSql<T>(T t)
        {
            string sql = string.Empty;
            string s1 = "";
            string s2 = "";
            string s3 = "";

            Type obj = t.GetType();
            PropertyInfo[] ps = obj.GetProperties();

            s1 = $"insert into {obj.Name.Replace("Model", "")}";

            foreach (PropertyInfo item in ps)
            {
                string n = item.Name;
                object v = item.GetValue(t, null);

                //不传值的属性不拼接到sql
                if (v != null)
                {
                    s2 += $"{n},";

                    //判断该属性是否为字符类型，特殊处理
                    if (typeof(string).Equals(item.PropertyType))
                        s3 += $"N'{v.ToString().Replace("'", "''")}',";
                    else
                        s3 += $"'{v}',";
                }
            }

            sql = $"{s1} ({s2.TrimEnd(',')}) values({s3.TrimEnd(',')})";

            return sql;
        }


        /// <summary>
        /// 将Model类转为更新SQL语句
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public static string ModelToUpdateSql<T>(T t)
        {
            return "";
        }


        /// <summary>
        /// 将DataTable转为List集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static List<T> DatatableToList<T>(DataTable dt)
        {
            //    string s = JsonConvert.SerializeObject(dt);
            //    List<T> _lst = JsonConvert.DeserializeObject<List<T>>(s);
            #region /****  反射用法  ****/

            // 定义集合    
            List<T> lst = new List<T>();

            // 获得此模型的类型   
            Type type = typeof(T);

            string tempName = "";

            foreach (DataRow dr in dt.Rows)
            {
                T t = Activator.CreateInstance<T>();

                // 获得此模型的公共属性      
                PropertyInfo[] propertys = type.GetType().GetProperties();
                foreach (PropertyInfo pi in propertys)
                {
                    tempName = pi.Name;  // 检查DataTable是否包含此列    

                    if (dt.Columns.Contains(tempName))
                    {
                        // 判断此属性是否有Setter      
                        if (!pi.CanWrite) continue;

                        object value = dr[tempName];
                        if (value != DBNull.Value)
                            pi.SetValue(t, value, null);
                    }
                }
                lst.Add(t);
            }

            #endregion
            return lst;
        }


        /// <summary>
        /// 将List集合转为DataTable
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ts"></param>
        /// <returns></returns>
        public static DataTable ListToDataTable<T>(List<T> ts)
        {
            //获取该类模型的属性
            var props = typeof(T).GetProperties();

            //实例一个DataTable
            var dt = new DataTable();

            //构建DataTable的列
            dt.Columns.AddRange(props.Select(p => new DataColumn(p.Name, p.PropertyType)).ToArray());

            if (ts.Count() > 0)
            {
                for (int i = 0; i < ts.Count(); i++)
                {
                    //将集合中的每个对象的属性值添加到Arraylist
                    System.Collections.ArrayList tempList = new System.Collections.ArrayList();
                    foreach (PropertyInfo pi in props)
                    {
                        object obj = pi.GetValue(ts.ElementAt(i), null);
                        tempList.Add(obj);
                    }
                    object[] array = tempList.ToArray();

                    //将数据添加到Datatable的数据行中
                    dt.LoadDataRow(array, true);
                }
            }
            return dt;
        }
    }
}
