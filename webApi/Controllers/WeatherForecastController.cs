using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace webApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {

        private readonly IConfiguration configuration;
        public WeatherForecastController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        [HttpGet]
        public JsonResult Get()
        {
            SqlConnection cn = new SqlConnection(configuration.GetConnectionString("con"));
            cn.Open();
            SqlCommand com = new SqlCommand("select * from users",cn);
            SqlDataReader dr = com.ExecuteReader();
            DataTable table = new DataTable("tb");
            table.Load(dr);
            cn.Close();
            return new JsonResult(table);
        }

        [HttpPost]
        public JsonResult Post(User user)
        {
            SqlConnection cn = new SqlConnection(@"Data source = .\sqlexpress;initial catalog = react_crud;integrated security = true;");
            cn.Open();
            SqlCommand com = new SqlCommand("insert into users values ('"+user.username+"','"+user.password+"','"+user.Fullname+"','"+ user.email + "','"+user.phoneNumber+"')", cn);
            try
            {
                com.ExecuteNonQuery();
                return new JsonResult("Added Successefuly");
            }
            catch (Exception) { return new JsonResult("Failed !!"); }
        }

        [HttpPut]
        public JsonResult Put(User user)
        {
            SqlConnection cn = new SqlConnection(@"Data source = .\sqlexpress;initial catalog = react_crud;integrated security = true;");
            cn.Open();
            SqlCommand com = new SqlCommand("update users set password ='" + user.password + 
                                            "' , Fullname = '" + user.Fullname + 
                                            "' , phoneNumber ='" + user.phoneNumber + 
                                            "' where username = '" + user.username + "';", cn);
            try
            {
                com.ExecuteNonQuery();
                return new JsonResult("Updated Successefuly");
            }
            catch (Exception) { return new JsonResult("Failed !!"); }
            cn.Close();
        }

        [HttpDelete("{user}")]
        public JsonResult Delete(string user)
        {
            SqlConnection cn = new SqlConnection(@"Data source = .\sqlexpress;initial catalog = react_crud;integrated security = true;");
            cn.Open();
            SqlCommand com = new SqlCommand("delete users where username like '"+user+"';", cn);
            try
            {
                com.ExecuteNonQuery();
                return new JsonResult("Delete Successefuly");
            }
            catch (Exception) { return new JsonResult("Failed !!"); }
            cn.Close();
        }

    }
}
