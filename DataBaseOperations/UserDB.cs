using MovieTicketBooking.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace MovieTicketBooking.DataBaseOperations
{
    public class UserDB
    {
        public string path = @"Data Source=NEUDESI-SSN74TE\SQLEXPRESS;Initial Catalog=MovieTicketBooking;Integrated Security=SSPI";

        public string Login(string Name, string password)
        {
            string status;
            using (SqlConnection con = new SqlConnection(path))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand($"sp_LoginCheck", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@name", Name);
                cmd.Parameters.AddWithValue("@password", password);
                status = Convert.ToString(cmd.ExecuteScalar());
                con.Close();
            }
            return status;
        }

        public UserInfo SignUp(UserInfo user)
        {
            UserInfo User = new UserInfo();
            using (SqlConnection con = new SqlConnection(path))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand($"sp_SignUp", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@name", user.Name);
                cmd.Parameters.AddWithValue("@age", user.age);
                cmd.Parameters.AddWithValue("@mobile", user.mobile);
                cmd.Parameters.AddWithValue("@password", user.Password);
                int RowCount = cmd.ExecuteNonQuery();
                con.Close();
                User.Name = user.Name;
                User.Password = user.Password;
            }
            return User;
        }
        public UserInfo Search(UserInfo user)
        {
            UserInfo User = new UserInfo();
            using (SqlConnection con = new SqlConnection(path))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand($"sp_Search", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@name", user.Name);
                cmd.Parameters.AddWithValue("@password", user.Password);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {
                    User.UserID = Convert.ToInt32(dr["UserID"]);
                }
            }
            return User;
        }
    }
}
