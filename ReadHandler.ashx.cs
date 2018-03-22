using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace AnnerPage
{
    /// <summary>
    /// ReadHandler 的摘要说明
    /// </summary>
    public class ReadHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string id = context.Request.Params["id"];
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["conn"].ConnectionString))
            {
                try
                {
                    conn.Open();
                    SqlCommand comm = new SqlCommand("ReadInfo", conn);
                    comm.Parameters.Add(new SqlParameter("@id", int.Parse(id)));
                    comm.CommandType = System.Data.CommandType.StoredProcedure;
                    try
                    {
                        string HTMLText = "";
                        SqlDataReader read = comm.ExecuteReader();
                        while (read.Read())
                        {
                            HTMLText += "<h3>"+read["title"]+"</h3><br/><h5>"+Convert.ToDateTime(read["postTime"]).ToString("yyyy-MM-dd")+"</h5><br/><h4>"+read["context"]+"</h4>";
                        }
                        context.Response.Write(HTMLText);
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