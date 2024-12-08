using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;


namespace WebPTCOM.Models
{
    public class ProductDAL
    {
        private SqlConnection con;

        private void Connect()
        {
            string constr = "Data Source=ADMIN-PC\\SQLEXPRESS;Initial Catalog=WebPTCOM;Integrated Security=True;";
            con = new SqlConnection(constr);
        }

        public List<Product> GetAll()
        {
            Connect();
            List<Product> List = new List<Product>();

            string sql = "Select * From tblProduct";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.CommandType = CommandType.Text;

            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                var obj = new Product()
                {
                    Id = Convert.ToInt32(dr["Id"]),
                    Title = Convert.ToString(dr["Title"]),
                    Description = Convert.ToString(dr["Description"]),
                    Photo = Convert.ToString(dr["Photo"]),
                    Price = Convert.ToDecimal(dr["Price"]),
                };
                List.Add(obj);
            }
            return (List);
        }

        public bool Insert(Product obj)
        {
            Connect();
            string sql = "INSERT INTO tblProduct (Title,Description,Price,Photo) values (@Title,@Description,@Price,@Photo)";
            SqlCommand cmd = new SqlCommand(sql, con);

            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@Title", obj.Title);
            cmd.Parameters.AddWithValue("@Photo", obj.Photo);
            cmd.Parameters.AddWithValue("@Description", obj.Description);
            cmd.Parameters.AddWithValue("@Price", obj.Price);

            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();

            if (i >= 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Update(Product obj)
        {
            Connect();
            string sql = "UPDATE tblProduct SET Title=@Title,Description=@Description,Price=@Price where Id=@Id";
            SqlCommand cmd = new SqlCommand(sql, con);

            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@Id", obj.Id);
            cmd.Parameters.AddWithValue("@Title", obj.Title);  
            cmd.Parameters.AddWithValue("@Description", obj.Description);
            cmd.Parameters.AddWithValue("@Price", obj.Price);

            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();

            if (i >= 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public Product GetById(int? Id)
        {
            Product obj = new Product();

            Connect();
            string sql = "SELECT * FROM tblProduct WHERE Id=@Id";

            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("Id", Id);

            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                obj.Id = Convert.ToInt32(dr["Id"]);
                obj.Title = Convert.ToString(dr["Title"]);
                obj.Photo = Convert.ToString(dr["Photo"]);
                obj.Description = Convert.ToString(dr["Description"]);
                obj.Price = Convert.ToDecimal(dr["Price"]);
            }
            return obj;
        }

        public bool Delete(int? Id)
        {
            Connect();
            string sql = "DELETE tblProduct WHERE Id=@Id";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@Id", Id);

            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();

            if (i >= 1)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

    }
}