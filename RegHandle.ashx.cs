using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace AnnerPage
{
    /// <summary>
    /// RegHandle 的摘要说明
    /// </summary>
    public class RegHandle : IHttpHandler
    {
        //登陆的处理
        public void ProcessRequest(HttpContext context)
        {
            string name = context.Request.Params["name"];
            string pass = context.Request.Params["pass"];
            using (SqlConnection conn = new SqlConnection("server=.;database=AnnerPage;uid=sa;pwd=AnnerPoint123456"))
            {
                try
                {
                    conn.Open();
                    SqlCommand comm = new SqlCommand("Reg",conn);
                    comm.Parameters.Add(new SqlParameter("@userName",name));
                    comm.Parameters.Add(new SqlParameter("@loginPass",pass));
                    comm.CommandType = System.Data.CommandType.StoredProcedure;
                    try
                    {
                        if (comm.ExecuteNonQuery() > 0)
                        {
                            context.Response.Write("注册成功！");
                        }
                    }
                    catch (Exception ex)
                    {
                        context.Response.Write("出现错误！："+ex);
                    }
                }
                catch (Exception ex)
                {
                    context.Response.Write("打开数据库错误！");
                }

            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}