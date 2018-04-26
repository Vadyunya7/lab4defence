using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab4defence
{
    public partial class Form1 : Form
    {
        WorldContext db;
        public Form1()
        {
            InitializeComponent();
            try
            {
                db = new WorldContext();
                // вы водим в первую колонку
                db.Countries.Load();
                dataGridView1.DataSource = db.Countries.ToList();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
       
        }
        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TeamForm tmForm = new TeamForm();   
            DialogResult result = tmForm.ShowDialog(this);
            if (result == DialogResult.Cancel)
                return;

            Country country = new Country();
            country.Name = tmForm.textBox1.Text;
            country.Treaties = new List<Treaty>();
            try
            {
                db.Countries.Add(country);
                db.SaveChanges();
                db.Countries.Load();
                dataGridView1.DataSource = db.Countries.Local.ToList();
            }
            catch(Exception error)
            {
                MessageBox.Show(error.ToString());
            }
            
        }

     

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            db = new WorldContext();
            int key = (int)dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex].Cells[0].Value;
            db.Cities.Load();
            dataGridView2.DataSource = db.Cities.Local.Where(p => p.CountryId == key).ToList();
            db.Treaties.Load();
   
            db.Countries.Load();
            dataGridView3.DataSource = db.Treaties.ToList();
        }

        private void addToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            PlayerForm plForm = new PlayerForm();// создаем форму для города
            // из команд в бд формируем список
              
            int key = (int)dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex].Cells[0].Value;
            plForm.comboBox1.DataSource = db.Countries.ToList();
            DialogResult result = plForm.ShowDialog(this);
       
            if (result == DialogResult.Cancel)
                return;

            City city = new City();
            Country c = (Country)plForm.comboBox1.SelectedItem;
            city.CountryId = c.Id;
            city.Name = plForm.textBox1.Text;
            city.Country = c;
            try
            {
                db.Cities.Add(city);
                db.SaveChanges();
                db.Countries.Load();
                dataGridView2.DataSource =   db.Cities.Local.Where(p => p.CountryId == key).ToList();
            }catch(Exception error)
            {
                MessageBox.Show(error.ToString());
            }

        }
        private void delToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int index = dataGridView1.SelectedRows[0].Index;
                int id = 0;
                bool converted = int.TryParse(dataGridView1[0, index].Value.ToString(), out  id);
                if (converted == false)
                    return;
                Country country = db.Countries.Find(id);
                db.Countries.Remove(country);
                db.SaveChanges();
                db.Countries.Load();
                dataGridView1.DataSource = db.Countries.Local.ToBindingList();
            }
        }
        private void delToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (dataGridView2.SelectedRows.Count > 0)
            {
                int index = dataGridView2.SelectedRows[0].Index;
                int id = 0;
                bool converted = int.TryParse(dataGridView2[0, index].Value.ToString(), out id);
                if (converted == false)
                    return;

                City player = db.Cities.Find(id);
                db.Cities.Remove(player);
                db.SaveChanges();
            }
        }


        private void treatyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TreatyForm treatyForm = new TreatyForm();
            treatyForm.Show();
            foreach (DataGridViewRow t in dataGridView1.SelectedRows)
                treatyForm.label1.Text += "  " + t.Cells[1].Value;
        }

        private void updToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int index = dataGridView1.SelectedRows[0].Index;
                int id = 0;
                bool converted = int.TryParse(dataGridView1[0, index].Value.ToString(), out id);
                if (converted == false)
                    return;

                Country country = db.Countries.Find(id);
                TeamForm tmForm = new TeamForm();
                tmForm.textBox1.Text = country.Name;
                DialogResult result = tmForm.ShowDialog(this);
                if (result == DialogResult.Cancel)
                    return;

                country.Name = tmForm.textBox1.Text;
                db.Entry(country).State = EntityState.Modified;
                db.SaveChanges();
                MessageBox.Show("Объект обновлен");
                db.Countries.Load();
                dataGridView1.DataSource = db.Countries.Local.ToBindingList();
            }
        }
        private void updToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (dataGridView2.SelectedRows.Count > 0)
            {
                int index = dataGridView2.SelectedRows[0].Index;
                int id = 0;
                int key = (int)dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex].Cells[0].Value;

                bool converted = int.TryParse(dataGridView2[0, index].Value.ToString(), out id);
                if (converted == false)
                    return;              

                City city = db.Cities.Find(id);
                PlayerForm tmForm = new PlayerForm();
                tmForm.comboBox1.DataSource = db.Countries.ToList();
                tmForm.textBox1.Text = city.Name;

                DialogResult result = tmForm.ShowDialog(this);
                if (result == DialogResult.Cancel)
                    return;

                city.Country = (Country)tmForm.comboBox1.SelectedItem;
                city.Name = tmForm.textBox1.Text;
                db.Entry(city).State = EntityState.Modified;
                db.SaveChanges();
                MessageBox.Show("Объект обновлен");
                db.Countries.Load();
                dataGridView2.DataSource = db.Cities.Local.Where(p => p.CountryId == key).ToList();
            }
        }
    }
}
