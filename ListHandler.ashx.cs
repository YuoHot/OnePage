using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace AnnerPage
{
    /// <summary>
    /// ListHandler 的摘要说明
    /// </summary>
    public class ListHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string groupName = context.Request.Params["groupName"];
            string pageIndex = context.Request.Params["pageIndex"];
            using (SqlConnection conn = new SqlConnection("server=.;database=AnnerPage;uid=sa;pwd=AnnerPoint123456"))
            {
                try
                {
                    conn.Open();
                    SqlCommand comm = new SqlCommand("GetList", conn);
                    comm.Parameters.Add(new SqlParameter("@groupName", groupName));
                    comm.Parameters.Add(new SqlParameter("@pageIndex",pageIndex));
                    comm.CommandType = System.Data.CommandType.StoredProcedure;
                    try
                    {
                        string HTMLText = "";
                        SqlDataReader read = comm.ExecuteReader();
                        while (read.Read())
                        {
                            HTMLText += "<a name="+read["id"]+" onclick='ReadInfo(this.name)'><div class=\"ail\"><img src=\"BackgroundImage / php.png\"></img><div class=\"lio\" brtag=\"Edit\">"+read["title"].ToString()+"</div><div class=\"lio2\">"+read["postTime"]+"</div></div></a>";
                        }
                        context.Response.Write(HTMLText);
                    }
                    catch(Exception ex)
                    {
                        context.Response.Write(ex.Message);
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