using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TechAssemblyManager.UI
{
    public partial class CheckoutForm : Form
    {
        private List<Produs> cos;
        private object cosCumparaturi;

        public CheckoutForm(List<Produs> cosCumparaturi)
        {
            InitializeComponent();
            cos = cosCumparaturi;
            AfiseazaProduse();
            AfiseazaTotal();
        }

        public CheckoutForm(object cosCumparaturi)
        {
            this.cosCumparaturi = cosCumparaturi;
        }

        private void AfiseazaProduse()
        {
            foreach (var produs in cos)
            {
                ListViewItem item = new ListViewItem(produs.Nume);
                item.SubItems.Add($"{produs.Pret} RON");
                listView1.Items.Add(item);
            }
        }

        private void AfiseazaTotal()
        {
            decimal total = cos.Sum(p => p.Pret);
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
