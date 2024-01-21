﻿using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Tools
{
    public class DbHelper //不能是泛型类
    {

        //如果是固定多库可以传 new SqlSugarScope(List<ConnectionConfig>,db=>{}) 文档：多租户
        //如果是不固定多库 可以看文档Saas分库

        //用单例模式
      //  public static SqlSugarScope Db = new SqlSugarScope(new ConnectionConfig()
      //  {
      //      ConnectionString = "server=222.180.242.134,77;uid=sa;pwd=RMAdmin123!@#;database=ManagementServer3;TrustServerCertificate=true",//连接符字串
      //      DbType = DbType.SqlServer,//数据库类型
      //      IsAutoCloseConnection = true //不设成true要手动close
      //  },
      //db =>
      //{
      //    //(A)全局生效配置点
      //    //调试SQL事件，可以删掉
      //    db.Aop.OnLogExecuting = (sql, pars) =>
      //    {
      //        Console.WriteLine(sql);//输出sql,查看执行sql
      //                               //5.0.8.2 获取无参数化 SQL 
      //                               //UtilMethods.GetSqlString(DbType.SqlServer,sql,pars)
      //    };
      //});
    }
}
