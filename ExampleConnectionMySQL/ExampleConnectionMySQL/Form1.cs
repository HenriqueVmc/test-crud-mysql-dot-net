using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExampleConnectionMySQL
{
    public partial class Form1 : Form
    {
        DBConnection db;
        private string nome, login, senha;
        private int cod;
        
        public Form1()
        {
            InitializeComponent();
            db = new DBConnection();
            nome = login = senha = string.Empty;            

            updateList();
        }

        private void updateList()
        {
            string usersList = db.selectAllFormated();
            txtList.Text = usersList;
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            nome = txtNome.Text; login = txtLogin.Text; senha = txtSenha.Text;

            db.insert(nome, login, senha);
            updateList();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            cod = Convert.ToInt32(txtCod.Text);
            string usersList = db.selectByCod(cod);

            if (string.IsNullOrEmpty(usersList))
                return;

            var result = usersList.Split(',');
            txtCod.Text = result.Count() > 0 ? result[0] : string.Empty;
            txtNome.Text = result.Count() > 1 ? result[1] : string.Empty;
            txtLogin.Text = result.Count() > 2 ? result[2] : string.Empty;
            txtSenha.Text = result.Count() > 3 ? result[3] : string.Empty;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            cod = Convert.ToInt32(txtCod.Text);
            nome = txtNome.Text;
            login = txtLogin.Text;
            senha = txtSenha.Text;
            
            db.update(cod, nome, login, senha);
            updateList();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            cod = Convert.ToInt32(txtCod.Text);
            db.delete(cod);
            updateList();
        }
    }
}
