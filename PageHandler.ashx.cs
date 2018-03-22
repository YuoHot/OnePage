using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace AnnerPage
{
    /// <summary>
    /// PageHandler 的摘要说明
    /// </summary>
    public class PageHandler : IHttpHandler
    {
        //获取分页
        public void ProcessRequest(HttpContext context)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["conn"].ConnectionString))
            {
                conn.Open();
                SqlCommand comm = new SqlCommand("GetPageCount",conn);
                int PageCount = ((int)comm.ExecuteScalar())/2;
                string name = "";
                for (int i = 1; i <= PageCount; i++)
                {
                    name += "<li name='uioclick' onclick='GetList('笔记',"+i+")'><a>" + i.ToString()+"</a></li>";
                }
                context.Response.Write(name);
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