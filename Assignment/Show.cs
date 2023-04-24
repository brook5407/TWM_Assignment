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

namespace Assignment
{
    public partial class List : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\chuny\OneDrive\Documents\MyTest.mdf;Integrated Security=True;Connect Timeout=30");
        SqlCommand cmd = new SqlCommand();
        SqlDataReader reader;
        public List()
        {
            InitializeComponent();
            LoadDB();
        }

        private void itemPriceList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colname = itemPriceList.Columns[e.ColumnIndex].Name;
            if (colname == "Edit" && itemPriceList.Rows[e.RowIndex].Cells[0].Value != null)
            {
                Update update = new Update();
                update.idNumber.Text = itemPriceList.Rows[e.RowIndex].Cells[0].Value.ToString();
                update.txtItemCode.Text = itemPriceList.Rows[e.RowIndex].Cells[1].Value.ToString();
                update.txtItemName.Text = itemPriceList.Rows[e.RowIndex].Cells[2].Value.ToString();
                update.txtPrice.Text = itemPriceList.Rows[e.RowIndex].Cells[3].Value.ToString();
                var isLocked = (bool)itemPriceList.Rows[e.RowIndex].Cells[4].Value;
                if (isLocked)
                    update.checkBox1.Checked = true;
                update.btnUpdate.Enabled = true;
                update.btnCancel.Enabled = true;
                update.ShowDialog();
                LoadDB();
            }
            else if (colname == "Delete" && itemPriceList.Rows[e.RowIndex].Cells[0].Value != null)
            {
                var isLocked = (bool)itemPriceList.Rows[e.RowIndex].Cells[4].Value;
                if (isLocked)
                    MessageBox.Show("Record has been locked!");
                else if (MessageBox.Show("Are you sure you want to delete this record?", "Delete Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    con.Open();
                    int id = Convert.ToInt32(itemPriceList.Rows[e.RowIndex].Cells[0].Value);
                    cmd = new SqlCommand("DELETE FROM Table1 WHERE ID = @ID", con);
                    cmd.Parameters.AddWithValue("@ID", id);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Record has been successfully deleted!");
                }
                LoadDB();
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Add add = new Add();
            add.ShowDialog();
            LoadDB();
        }
        public void LoadDB()
        {
            itemPriceList.Rows.Clear();
            cmd = new SqlCommand("SELECT * FROM Table1", con);
            con.Open();
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                bool isChecked = reader[4].ToString() == "Y";
                itemPriceList.Rows.Add(reader[0].ToString(), reader[1].ToString(), reader[2].ToString(), reader[3].ToString(), isChecked);
            }
            reader.Close();
            con.Close();
        }
    }
}
