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
        private SqlConnection cn;
        public WeatherForecastController(IConfiguration configuration)
        {
            this.configuration = configuration;
            cn = new SqlConnection(configuration.GetConnectionString("con"));
        }
        private void openCon()
        {
            if (cn.State != ConnectionState.Open)
                cn.Open();
        }
        private void closeCon()
        {
            if (cn.State != ConnectionState.Closed)
                cn.Close();
        }


        [HttpGet]
        public JsonResult Get()
        {
            openCon();
            SqlCommand com = new SqlCommand("select * from users",cn);
            SqlDataReader dr = com.ExecuteReader();
            DataTable table = new DataTable("tb");
            table.Load(dr);
            closeCon();
            return new JsonResult(table);
        }

        [HttpPost]
        public JsonResult Post(User user)
        {
            openCon();
            SqlCommand com = new SqlCommand("insert into users values ('"+user.username+"','"+user.password+"','"+user.Fullname+"','"+ user.email + "','"+user.phoneNumber+"')", cn);
            try
            {
                com.ExecuteNonQuery();
                closeCon();
                return new JsonResult("Added Successefuly");
            }
            catch (Exception) { closeCon(); return new JsonResult("Failed !!"); }
        }

        [HttpPut]
        public JsonResult Put(User user)
        {
            openCon();
            SqlCommand com = new SqlCommand("update users set password ='" + user.password + 
                                            "' , Fullname = '" + user.Fullname + 
                                            "' , phoneNumber ='" + user.phoneNumber + 
                                            "' where username = '" + user.username + "';", cn);
            try
            {
                com.ExecuteNonQuery();
                closeCon();
                return new JsonResult("Updated Successefuly");
            }
            catch (Exception) { closeCon(); return new JsonResult("Failed !!"); }
        }

        [HttpDelete("{user}")]
        public JsonResult Delete(string user)
        {
            openCon();
            SqlCommand com = new SqlCommand("delete users where username like '"+user+"';", cn);
            try
            {
                com.ExecuteNonQuery();
                closeCon();
                return new JsonResult("Delete Successefuly");
            }
            catch (Exception) { closeCon(); return new JsonResult("Failed !!"); }
        }

    }
}
