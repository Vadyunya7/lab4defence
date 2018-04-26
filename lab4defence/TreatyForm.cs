using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab4defence
{
    public partial class TreatyForm : Form
    {

        WorldContext db;
        public TreatyForm()
        {
            InitializeComponent();
            try
            {
                db = new WorldContext();
            }catch(Exception error)
            {
                MessageBox.Show(error.ToString());
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            List<Country> country = db.Countries.ToList();
            Treaty treaty = new Treaty();
            treaty.Description = textBox1.Text;
            treaty.Name = textBox2.Text;
            country.Clear(); 
            foreach (var item in form1.dataGridView1.SelectedRows)
            {
                country.Add((Country)item);
            }
            treaty.Countries = country;
            db.Treaties.Add(treaty);
            db.SaveChanges();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
