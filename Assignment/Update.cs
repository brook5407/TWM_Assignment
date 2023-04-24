using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace Assignment
{
    public partial class Update : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\chuny\OneDrive\Documents\MyTest.mdf;Integrated Security=True;Connect Timeout=30");
        SqlCommand cmd = new SqlCommand();
        public Update()
        {
            InitializeComponent();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        public void Clear()
        {
            txtItemCode.Clear();
            txtItemName.Clear();
            txtPrice.Clear();
            checkBox1.Checked = false;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Are you sure you want to update this record?", "Updating Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cmd = new SqlCommand("UPDATE Table1 SET ITEM_CODE=@ITEM_CODE,ITEM_NAME=@ITEM_NAME,PRICE=@PRICE,LOCKED=@LOCKED WHERE ID = @ID", con);
                    int id = Convert.ToInt32(idNumber.Text);
                    cmd.Parameters.AddWithValue("@ITEM_CODE", txtItemCode.Text);
                    cmd.Parameters.AddWithValue("@ITEM_NAME", txtItemName.Text);
                    cmd.Parameters.AddWithValue("@PRICE", txtPrice.Text);
                    if (checkBox1.Checked)
                        cmd.Parameters.AddWithValue("@LOCKED", "Y");
                    else
                        cmd.Parameters.AddWithValue("@LOCKED", "N");
                    cmd.Parameters.AddWithValue("@ID", id);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Record has been successfully updated.");
                    this.Dispose();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void txtPrice_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}


