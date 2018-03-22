using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace AnnerPage
{
    /// <summary>
    /// LoginHandler 的摘要说明
    /// </summary>
    public class LoginHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string name = context.Request.Params["name"];
            string pass = context.Request.Params["pass"];
            using (SqlConnection conn = new SqlConnection("server=.;database=AnnerPage;uid=sa;pwd=AnnerPoint123456"))
            {
                try
                {
                    conn.Open();
                    SqlCommand comm = new SqlCommand("Login", conn);
                    comm.Parameters.Add(new SqlParameter("@userName", name));
                    comm.Parameters.Add(new SqlParameter("@loginPass", pass));
                    comm.CommandType = System.Data.CommandType.StoredProcedure;
                    try
                    {
                        if (int.Parse(comm.ExecuteScalar().ToString()) > 0)
                        {
                            context.Response.Write("登陆成功！");
                        }
                    }
                    catch (Exception ex)
                    {
                        context.Response.Write("出现错误！：" + ex);
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