using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class AdminUserManagement : System.Web.UI.Page
    {
        // connect to the database
        string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            GridView1.DataBind();
        }

        // Go Btn
        protected void LinkButton4_Click(object sender, EventArgs e)
        {
            GetUserByID();
        }


        // Active user btn
        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            UpdateUserStatusByID("active");
        }
        // pending user btn
        protected void LinkButton2_Click(object sender, EventArgs e)
        {
            UpdateUserStatusByID("pending");
        }
        // deactive user btn
        protected void LinkButton3_Click(object sender, EventArgs e)
        {
            UpdateUserStatusByID("deactive");
        }
        // Delete User btn
        protected void Button1_Click(object sender, EventArgs e)
        {
            DeleteUserByID();
        }


        // User define function

        void GetUserByID()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand("SELECT * FROM user_tbl where user_id='" + TextBox8.Text.Trim() +  "'", con);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        TextBox4.Text = dr.GetValue(0).ToString();
                        TextBox7.Text = dr.GetValue(6).ToString();
                        TextBox1.Text = dr.GetValue(2).ToString();
                        TextBox2.Text = dr.GetValue(3).ToString();

                        

                    }
                   
                }
                else
                {
                    Response.Write("<script>alert('Invalid User try again.');</script>");
                }

            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }

        void UpdateUserStatusByID(string status)
        {
            if (CheckUserExists())
            {
                try
                {
                    SqlConnection con = new SqlConnection(strcon);
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    SqlCommand cmd = new SqlCommand("UPDATE user_tbl SET account_status='" + status + "' WHERE user_id='" + TextBox8.Text.Trim() + "'", con);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    Response.Write("<script>alert('Status User Updatedd.');</script>");
                    ClearForm();
                    GridView1.DataBind();
                }
                catch (Exception ex)
                {
                    Response.Write("<script>alert('" + ex.Message + "');</script>");
                }
            }
            else
            {
                Response.Write("<script>alert('Invalid User ID.');</script>");
            }
            
        }

        void DeleteUserByID()
        {
            if (CheckUserExists())
            {
                try
                {
                    SqlConnection con = new SqlConnection(strcon);
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }

                    SqlCommand cmd = new SqlCommand("DELETE from user_tbl WHERE user_id='" + TextBox8.Text.Trim() + "'", con);

                    cmd.ExecuteNonQuery();
                    con.Close();
                    Response.Write("<script>alert('User Deleted Successfully.');</script>");
                    ClearForm();
                    GridView1.DataBind();

                }
                catch (Exception ex)
                {
                    Response.Write("<script>alert('" + ex.Message + "');</script>");
                }
            }
            else
            {
                Response.Write("<script>alert('Invalid User ID.');</script>");
            }
            
        }

       void ClearForm()
        {
            TextBox1.Text =  " ";
            TextBox2.Text =  " ";
            TextBox4.Text =  " ";
            TextBox7.Text =  " ";
            TextBox8.Text =  " ";
        }

        bool CheckUserExists()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand("SELECT * from user_tbl where user_id='" + TextBox8.Text.Trim() + "';", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count >= 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");

                return false;
            }
        }
    }
}