using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TechAssemblyManager.Models;

namespace TechAssemblyManager.UI
{
    public partial class CheckoutForm : Form
    {
        private List<Product> cos;
        private object cosCumparaturi;
        private MainForm mainForm;
        public CheckoutForm(List<Product> cosCumparaturi,MainForm mainForm)
        {
            this.mainForm = mainForm;
            InitializeComponent();
            cos = cosCumparaturi;
            this.FormClosing += CheckoutForm_FormClosing;
            AfiseazaProduse();
            AfiseazaTotal();
        }
        private void CheckoutForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(mainForm!=null && !mainForm.IsDisposed)
            {
                mainForm.Show();
            }
        }
        public CheckoutForm(object cosCumparaturi)
        {
            this.cosCumparaturi = cosCumparaturi;
        }

        private void AfiseazaProduse()
        {
            foreach (var produs in cos)
            {
                ListViewItem item = new ListViewItem(produs.name);
                item.SubItems.Add($"{produs.price} RON");
                listView1.Items.Add(item);
            }
        }

        private void AfiseazaTotal()
        {
            float total = cos.Sum(p => p.price);
            lblTotal.Text = $"Total: {total} RON";
        }

        private void btnFinalizare_Click(object sender, EventArgs e)
        {
            var confirmare = MessageBox.Show("Ești sigur că vrei să finalizezi comanda?", "Confirmare", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirmare == DialogResult.Yes)
            {
                MessageBox.Show("Comanda a fost plasată cu succes!", "Finalizare", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cos.Clear(); // Golește coșul
                this.Close(); // Închide fereastra

            }
        }
    }
}
