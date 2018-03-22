using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace AnnerPage
{
    /// <summary>
    /// PostHandler 的摘要说明
    /// </summary>
    public class PostHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string title = context.Request.Params["title"];
            string conContent = context.Request.Params["conContent"];
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["conn"].ConnectionString))
            {
                try
                {
                    conn.Open();
                    SqlCommand comm = new SqlCommand("postNote", conn);
                    comm.Parameters.Add(new SqlParameter("@title", title));
                    comm.Parameters.Add(new SqlParameter("@content", conContent));
                    comm.Parameters.Add(new SqlParameter("@time",DateTime.Now));
                    comm.Parameters.Add(new SqlParameter("@groupType","笔记"));
                    comm.CommandType = System.Data.CommandType.StoredProcedure;
                    try
                    {
                        if (int.Parse(comm.ExecuteScalar().ToString()) > 0)
                        {
                            context.Response.Write("发布成功！");
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