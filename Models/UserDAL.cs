using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace WebPTCOM.Models
{
    public class UserDAL
    {
        private SqlConnection con;

        private void Connect()
        {
            string constr = "Data Source=ADMIN-PC\\SQLEXPRESS;Initial Catalog=WebPTCOM;Integrated Security=True;";
            con = new SqlConnection(constr);
        }

        public User GetUser(string Username, string Password)
        {
            User user = new User();

            Connect();
            string sql = "SELECT * FROM tblUser WHERE Username=@Username AND Password=@Password";

            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@Username", Username);
            cmd.Parameters.AddWithValue("@Password", Password);

            con.Open();
            SqlDataReader sdr = cmd.ExecuteReader();
            while (sdr.Read())
            {
                user.Username = Convert.ToString(sdr["Username"]);
                user.Password = Convert.ToString(sdr["Password"]);
                user.Role = Convert.ToString(sdr["Role"]);
            }
            return user;
        }
    }
}